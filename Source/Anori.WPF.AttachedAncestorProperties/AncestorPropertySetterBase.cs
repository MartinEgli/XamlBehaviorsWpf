// -----------------------------------------------------------------------
// <copyright file="AncestorPropertySetterBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using JetBrains.Annotations;

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal abstract class AncestorPropertySetterBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The ancestor object
        /// </summary>
        [CanBeNull]
        private DependencyObject ancestor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AncestorPropertySetterBase"/> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <exception cref="ArgumentNullException">dependencyObject or setterProperty</exception>
        protected AncestorPropertySetterBase(
            [NotNull] DependencyObject dependencyObject,
            [NotNull] DependencyProperty setterProperty)
        {
            this.DependencyObject = dependencyObject ?? throw new ArgumentNullException(nameof(dependencyObject));
            this.SetterProperty = setterProperty ?? throw new ArgumentNullException(nameof(setterProperty));
        }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event Action<DependencyObject> AncestorChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event Action<object> ValueChanged;

        /// <summary>
        /// Gets or sets the ancestor object.
        /// </summary>
        /// <value>The ancestor object.</value>
        public DependencyObject Ancestor
        {
            get => this.ancestor;
            set
            {
                if (Equals(value, this.ancestor))
                {
                    return;
                }

                this.ancestor = value;
                this.OnAncestorChanged(this.ancestor);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the dependency object.
        /// </summary>
        /// <value>The dependency object.</value>
        public DependencyObject DependencyObject { get; }

        /// <summary>
        /// The setter property
        /// </summary>
        [NotNull]
        protected DependencyProperty SetterProperty { get; }

        /// <summary>
        /// Called when [ancestor changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void OnAncestorChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(
                "On EndPointUpdater Changed {0}",
                (object)((FrameworkElement)((EndPointUpdater)sender).Ancestor)?.Name);
            ((EndPointUpdater)sender).AncestorChanged -= this.OnAncestorChanged;
            this.UpdateAncestorProperty();
        }

        /// <summary>
        /// Called when [ancestor changed].
        /// </summary>
        /// <param name="ancestor">The ancestor.</param>
        protected virtual void OnAncestorChanged(DependencyObject ancestor)
        {
            Debug.WriteLine(
                "On Ancestor Changed DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);

            this.AncestorChanged?.Invoke(ancestor);
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        protected void OnLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Loaded DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            if (!this.UpdateAncestorProperty())
            {
                return;
            }

            this.UpdateTarget(sender);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when [unloaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        protected void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(
                "On Unloaded DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);
            EndPointUpdater endPointUpdater =
                AncestorPropertyHelper.GetOrCreateEndPointUpdater(this.Ancestor, this.SetterProperty);
            endPointUpdater.AncestorChanged -= this.OnAncestorChanged;
            endPointUpdater.Unsubscribe -= this.OnUnsubscribe;
            this.UnsubscribeValueChanged();
            //AncestorPropertyHelper.RemoveValueChangedHandler(this.Ancestor, this.SetterProperty, this.ValueChangedHandler);
        }

        /// <summary>
        /// Hosts the on unsubscribe.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void OnUnsubscribe(object sender, EventArgs e)
        {
            ((EndPointUpdater)sender).Unsubscribe -= this.OnUnsubscribe;
            this.UnsubscribeValueChanged();
            //AncestorPropertyHelper.RemoveValueChangedHandler(this.Ancestor, this.SetterProperty, this.ValueChangedHandler);
        }

        /// <summary>
        /// Called when [value changed].
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void OnValueChanged(object value)
        {
            Debug.WriteLine(
                "On Value Changed DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);

            this.Ancestor?.SetValue(this.SetterProperty, value);
            this.ValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Subscribes the value changed.
        /// </summary>
        protected abstract void SubscribeValueChanged();

        /// <summary>
        /// Unsubscribes the value changed.
        /// </summary>
        protected abstract void UnsubscribeValueChanged();

        /// <summary>
        /// Updates the ancestor.
        /// </summary>
        protected virtual bool UpdateAncestorProperty()
        {
            Debug.WriteLine(
                "Update EndPointUpdater DependencyObject {0}",
                (object)((FrameworkElement)this.DependencyObject)?.Name);

            DependencyObject ancestorObject = AncestorPropertyHelper.GetAncestor(
                this.DependencyObject ?? throw new InvalidOperationException(),
                this.SetterProperty,
                out _);
            if (ancestorObject == this.Ancestor)
            {
                if (this.Ancestor != null)
                {
                    AncestorPropertyHelper.GetUpdater(this.Ancestor, this.SetterProperty).AncestorChanged +=
                        this.OnAncestorChanged;
                }

                Debug.WriteLine("EndPointUpdater not changed {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                return false;
            }

            if (this.Ancestor != null)
            {
                Debug.WriteLine("Removing Ancestor {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                this.UnsubscribeValueChanged();
            } else
            {
                Debug.WriteLine("Not Removed Ancestor is null for {0}", this.GetType());
            }

            this.Ancestor = ancestorObject;

            if (this.Ancestor != null)
            {
                Debug.WriteLine("Adding Ancestor {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                this.SubscribeValueChanged();
                EndPointUpdater endPointUpdater =
                    AncestorPropertyHelper.GetOrCreateEndPointUpdater(this.Ancestor, this.SetterProperty);
                endPointUpdater.AncestorChanged += this.OnAncestorChanged;
                endPointUpdater.Unsubscribe += this.OnUnsubscribe;
            } else
            {
                Debug.WriteLine("Not Added Ancestor is null for {0}", this.GetType());
            }

            this.UpdateValue();
            return true;
        }

        /// <summary>
        /// Updates the target.
        /// </summary>
        protected abstract void UpdateTarget(object sender);

        /// <summary>
        /// Updates the value.
        /// </summary>
        protected abstract void UpdateValue();
    }
}
