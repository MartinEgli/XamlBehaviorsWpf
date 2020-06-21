// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyOneWaySetter.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using JetBrains.Annotations;

    using System;
    using System.Diagnostics;
    using System.Windows;

    internal class AncestorPropertyOneWaySetter : AncestorPropertySetterBase
    {
        
        /// <summary>
        ///     The update action
        /// </summary>
        private readonly Action<object> updateAction;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="AttachedAncestorPropertyUpdateableSetter{TOwner,TValue}" />
        ///     class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="updateAction">The update action.</param>
        public AncestorPropertyOneWaySetter(
            [NotNull] DependencyObject dependencyObject,
            [NotNull] DependencyProperty setterProperty,
            Action<object> updateAction) : base(dependencyObject, setterProperty) =>
            this.updateAction = updateAction;

        /// <summary>
        ///     Provides the value.
        /// </summary>
        /// <returns></returns>
        [CanBeNull]
        public object ProvideValue([NotNull] IServiceProvider serviceProvider) => Create();

        [CanBeNull]
        public object Create()
        {
            if (!(this.DependencyObject is FrameworkElement frameworkElement))
            {
                return AncestorPropertyHelper.GetValueOrRegisterParentChanged(
                    this.DependencyObject,
                    this.SetterProperty,
                    this.OnSourceChanged);
            }

            if (frameworkElement.IsLoaded)
            {
                if (this.UpdateAncestorProperty())
                {
                    // this.UpdateTarget();
                }

                Debug.WriteLine("Framework element {0} is loaded", (object)frameworkElement.Name);
            }

            object value = AncestorPropertyHelper.GetValueOrRegisterParentChanged(
                this.DependencyObject,
                this.SetterProperty,
                this.OnSourceChanged);
            frameworkElement.Loaded += this.OnLoaded;
            frameworkElement.Unloaded += this.OnUnloaded;
            return value;

        }

        protected override void UpdateValue()
        {
            Debug.WriteLine(
                "Update value DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            object value = this.Ancestor.GetValue(this.SetterProperty);
            this.updateAction(value);
            Debug.WriteLine(
                "New Value {0} DependencyObject {1}",
                value,
                ((FrameworkElement)this.DependencyObject)?.Name);
        }

        
       

        /// <summary>
        ///     Updates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void Update(object value) => this.updateAction(value);

        protected override void SubscribeValueChanged()
        {
        }
        protected override void UnsubscribeValueChanged()
        {
        }
        /// <summary>
        ///     Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        protected override void UpdateTarget(object sender)
        {
            if (sender is DependencyObject dependencyObj)
            {
                this.Update(
                    AncestorPropertyHelper.GetValueOrRegisterParentChanged(
                        dependencyObj,
                        this.SetterProperty,
                        this.OnSourceChanged));
            }
        }

        /// <summary>
        ///     Called when [source changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnSourceChanged(object sender, EventArgs e) =>
            this.Update(
                AncestorPropertyHelper.GetValueOrRegisterParentChanged(
                    this.DependencyObject,
                    this.SetterProperty,
                    this.OnSourceChanged));
    }
}
