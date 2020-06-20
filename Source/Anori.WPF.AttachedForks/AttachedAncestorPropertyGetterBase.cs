// -----------------------------------------------------------------------
// <copyright file="AttachedAncestorPropertyGetterBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    using Anori.WPF.Behaviors;
    using Anori.WPF.Extensions;

    using JetBrains.Annotations;

    [ContentProperty("Binding")]
    public abstract class AttachedAncestorPropertyGetterBase : Freezable, IAttachedObject
    {

        /// <summary>
        ///     The value property
        /// </summary>
        internal static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(AttachedAncestorPropertyGetterBase),
            new PropertyMetadata(default(object), ValueChanged));

        /// <summary>
        /// Properties the changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
            => ((AttachedAncestorPropertyGetterBase)dependencyObject).UpdateValue(e.NewValue);

        /// <summary>
        /// Updates the value.
        /// </summary>
        private void UpdateValue(object value) => this.descriptor?.SetValue(this.ancestor, value);

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get => this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     The host object
        /// </summary>
        private DependencyObject ancestor;

        /// <summary>
        ///     The descriptor
        /// </summary>
        private DependencyPropertyDescriptor descriptor;

        /// <summary>
        ///     The setter property
        /// </summary>
        private DependencyProperty setterProperty;

        /// <summary>
        ///     Gets the associated object.
        /// </summary>
        /// <value>The associated object.</value>
        /// <remarks>Represents the object the instance is attached to.</remarks>
        public DependencyObject AssociatedObject { get; }

        /// <summary>
        ///     Gets or sets the binding.
        /// </summary>
        /// <value>The binding.</value>
        public BindingBase Binding { get; set; }

        /// <summary>
        ///     Attaches to the specified object.
        /// </summary>
        /// <param name="dependencyObject">The object to attach to.</param>
        /// <exception cref="ArgumentNullException">
        ///     dependencyObject or GetDependencyProperty(Type, \"SetterProperty\") or
        ///     GetInternalDependencyProperty(Type, \"ShadowEndPointUpdaterProperty\")
        /// </exception>
        public void Attach([NotNull] DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            this.setterProperty = this.Type.GetDependencyProperty("SetterProperty")
                                  ?? throw new ArgumentNullException("GetDependencyProperty(Type, \"SetterProperty\")");
            var shadowProperty = this.Type.GetInternalDependencyProperty( "ShadowEndPointUpdaterProperty")
                                 ?? throw new ArgumentNullException(
                                     "GetInternalDependencyProperty(Type, \"ShadowEndPointUpdaterProperty\")");

            this.ancestor = dependencyObject.GetAncestor(this.setterProperty, shadowProperty);
            if (this.ancestor == null)
            {
                return;
            }

            BindingOperations.SetBinding(this, ValueProperty, this.Binding);
            this.descriptor = DependencyPropertyDescriptor.FromProperty(
                this.setterProperty,
                this.setterProperty.OwnerType);

            this.descriptor?.AddValueChanged(this.ancestor, this.ValueChangedHandler);

        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; set; }


        /// <summary>
        ///     Values the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ValueChangedHandler(object sender, EventArgs e) =>
            this.Value = this.ancestor.GetValue(this.setterProperty);

        /// <summary>
        ///     Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            BindingOperations.ClearBinding(this, ValueProperty);
            this.descriptor?.RemoveValueChanged(this.ancestor, this.ValueChangedHandler);
        }
    }
}
