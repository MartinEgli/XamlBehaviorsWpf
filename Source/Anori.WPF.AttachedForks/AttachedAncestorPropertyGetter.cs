using Anori.WPF.Behaviors;
using Anori.WPF.Extensions;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Anori.WPF.AttachedAncestorProperties
{
    [ContentProperty("Binding")]
    public class AttachedAncestorPropertyGetter : Freezable, IAttachedObject
    {
        /// <summary>
        /// The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(AttachedAncestorPropertyGetter),
            new PropertyMetadata(default(object)));

        /// <summary>
        /// The descriptor
        /// </summary>
        private DependencyPropertyDescriptor descriptor;

        /// <summary>
        /// The host object
        /// </summary>
        private DependencyObject ancestor;

        /// <summary>
        /// The setter property
        /// </summary>
        private DependencyProperty setterProperty;

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        /// <value>
        /// The associated object.
        /// </value>
        /// <remarks>
        /// Represents the object the instance is attached to.
        /// </remarks>
        public DependencyObject AssociatedObject { get; }

        /// <summary>
        /// Gets or sets the binding.
        /// </summary>
        /// <value>
        /// The binding.
        /// </value>
        public BindingBase Binding { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Attaches to the specified object.
        /// </summary>
        /// <param name="dependencyObject">The object to attach to.</param>
        /// <exception cref="ArgumentNullException">
        /// dependencyObject
        /// or
        /// GetDependencyProperty(Type, \"SetterProperty\")
        /// or
        /// GetInternalDependencyProperty(Type, \"ShadowAttachedAncestorPropertyProperty\")
        /// </exception>
        public void Attach([NotNull] DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            setterProperty = this.Type.GetDependencyProperty("SetterProperty") ?? throw new ArgumentNullException("GetDependencyProperty(Type, \"SetterProperty\")");
            var shadowProperty = this.Type.GetInternalDependencyProperty("ShadowAttachedAncestorPropertyProperty") ?? throw new ArgumentNullException("GetInternalDependencyProperty(Type, \"ShadowAttachedAncestorPropertyProperty\")");

            this.ancestor = dependencyObject.GetAncestor(setterProperty, shadowProperty);
            if (this.ancestor == null)
            {
                return;
            }

            BindingOperations.SetBinding(this, ValueProperty, this.Binding);
            descriptor = DependencyPropertyDescriptor.FromProperty(this.setterProperty, this.setterProperty.OwnerType);
            descriptor?.AddValueChanged(this.ancestor, this.ValueChangedHandler);
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            BindingOperations.ClearBinding(this, ValueProperty);
            descriptor?.RemoveValueChanged(this.ancestor, this.ValueChangedHandler);
        }

        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" /> derived class.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore() => new AttachedAncestorPropertyGetter();

        /// <summary>
        /// Values the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ValueChangedHandler(object sender, EventArgs e) => Value = this.ancestor.GetValue(this.setterProperty);
    }
}
