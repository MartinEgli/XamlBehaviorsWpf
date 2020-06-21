// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyTwoWaySetter.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using Anori.WPF.Extensions;

    using JetBrains.Annotations;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Data;

    internal class AncestorPropertyTwoWaySetter : AncestorPropertySetterBase
    {
        /// <summary>
        ///     The path
        /// </summary>
        [CanBeNull]
        private readonly PropertyPath path;

        /// <summary>
        ///     The update source trigger
        /// </summary>
        private readonly UpdateSourceTrigger updateSourceTrigger;

        /// <summary>
        ///     The binding expression
        /// </summary>
        [CanBeNull]
        private BindingExpression bindingExpression;

        /// <summary>
        ///     The value
        /// </summary>
        [CanBeNull]
        private object value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Setter" /> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="updateSourceTrigger">The update source trigger.</param>
        /// <param name="setterProperty"></param>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
        public AncestorPropertyTwoWaySetter(
            [NotNull] DependencyObject dependencyObject,
            UpdateSourceTrigger updateSourceTrigger,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] PropertyPath path)
            : base(dependencyObject, setterProperty)
        {
            this.updateSourceTrigger = updateSourceTrigger;
            this.path = path;
        }

        /// <summary>
        ///     Occurs when [source changed].
        /// </summary>
        public event Action SourceChanged;

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get => this.value;
            set
            {
                if (EqualityComparer<object>.Default.Equals(value, this.value))
                {
                    return;
                }

                Debug.WriteLine(
                    "Set Value to new [{0}] old [{1}] DependencyObject {2}",
                    value,
                    this.value,
                    ((FrameworkElement)this.DependencyObject)?.Name);

                this.value = value;
                this.OnValueChanged(value);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Creates the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyProperty">The dependency property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">expression or expression</exception>
        public BindingExpression Create(
            [NotNull] DependencyObject dependencyObject,
            [NotNull] DependencyProperty dependencyProperty)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (dependencyProperty == null)
            {
                throw new ArgumentNullException(nameof(dependencyProperty));
            }

            if (this.DependencyObject is FrameworkElement frameworkElement)
            {
                if (frameworkElement.IsLoaded)
                {
                    if (this.UpdateAncestorProperty())
                    {
                        this.UpdateTarget();
                    }

                    Debug.WriteLine("Framework element {0} is loaded", (object)frameworkElement.Name);
                }

                Binding binding = this.CreateBinding();
                if (!(BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding) is BindingExpression
                          expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;

                frameworkElement.Loaded += this.OnLoaded;
                frameworkElement.Unloaded += this.OnUnloaded;
            } else
            {
                this.UpdateAncestorProperty();

                Binding binding = this.CreateBinding();
                if (!(BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding) is BindingExpression
                          expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;
            }

            return this.bindingExpression;
        }

        /// <summary>
        ///     Provides the value.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serviceProvider</exception>
        [CanBeNull]
        public object ProvideValue([NotNull] IServiceProvider serviceProvider)
        {
            Debug.WriteLine(
                "Provide value DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (this.DependencyObject is FrameworkElement frameworkElement)
            {
                if (frameworkElement.IsLoaded)
                {
                    if (this.UpdateAncestorProperty())
                    {
                        this.UpdateTarget();
                    }

                    Debug.WriteLine("Framework element {0} is loaded", (object)frameworkElement.Name);
                }

                if (!(this.CreateBinding().ProvideValue(serviceProvider) is BindingExpression expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;

                frameworkElement.Loaded += this.OnLoaded;
                frameworkElement.Unloaded += this.OnUnloaded;
                return this.bindingExpression;
            } else
            {
                this.UpdateAncestorProperty();

                if (!(this.CreateBinding().ProvideValue(serviceProvider) is BindingExpression expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;
                return this.bindingExpression;
            }
        }

        /// <summary>
        ///     Sets the source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool SetSource(object value)
        {
            if (EqualityComparer<object>.Default.Equals(value, this.value))
            {
                return false;
            }

            Debug.WriteLine(
                "Set Source new [{0}] old [{1}] DependencyObject {2}",
                value,
                this.value,
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.value = value;
            this.OnSourceChanged();
            return true;
        }

        /// <summary>
        ///     Subscribes the value changed.
        /// </summary>
        protected override void SubscribeValueChanged() =>
            AncestorPropertyHelper.AddValueChangedHandler(this.Ancestor, this.SetterProperty, this.ValueChangedHandler);

        /// <summary>
        ///     Unsubscribes the value changed.
        /// </summary>
        protected override void UnsubscribeValueChanged() =>
            AncestorPropertyHelper.RemoveValueChangedHandler(
                this.Ancestor,
                this.SetterProperty,
                this.ValueChangedHandler);

        /// <summary>
        ///     Updates the target.
        /// </summary>
        /// <param name="sender"></param>
        protected override void UpdateTarget(object sender) => UpdateTarget();

        /// <summary>
        ///     Updates the value.
        /// </summary>
        protected override void UpdateValue()
        {
            Debug.WriteLine(
                "Update value DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.Value = this.Ancestor.GetValue(this.SetterProperty);
            Debug.WriteLine(
                "New Value {0} DependencyObject {1}",
                this.Value,
                ((FrameworkElement)this.DependencyObject)?.Name);
        }

        /// <summary>
        ///     Values the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ValueChangedHandler(object sender, EventArgs e)
        {
            Debug.WriteLine(
                "Value Changed Handler DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            DependencyObject dependencyObject = ((DependencyObject)sender);
            object v = dependencyObject.GetValueSync<object>(this.SetterProperty);
            if (this.SetSource(v))
            {
                Debug.WriteLine(
                    "Value changed handler value {0} to {1}",
                    v,
                    ((FrameworkElement)dependencyObject)?.Name);
            }
        }

        /// <summary>
        ///     Bindings this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private Binding CreateBinding()
        {
            Debug.WriteLine(
                "Create Binding DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);

            Binding binding = new Binding(nameof(Setter.Value))
                              {
                                  Mode = BindingMode.TwoWay,
                                  Source = this,
                                  UpdateSourceTrigger = this.updateSourceTrigger
                              };
            if (this.path != null)
            {
                binding.Path = this.path;
            }

            return binding;
        }

        /// <summary>
        ///     Called when [source changed].
        /// </summary>
        private void OnSourceChanged()
        {
            Debug.WriteLine(
                "On Source Changed DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.UpdateTarget();
            this.SourceChanged?.Invoke();
        }

        /// <summary>
        ///     Updates the target.
        /// </summary>
        /// <exception cref="ArgumentNullException">bindingExpression</exception>
        private void UpdateTarget()
        {
            Debug.WriteLine("Update target {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.bindingExpression?.UpdateTarget();
        }
    }
}
