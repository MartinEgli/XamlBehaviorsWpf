using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// AttachedAncestorPropertyBindableSetter
    /// </summary>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    internal class AttachedAncestorPropertyBindableSetter<TOwner, TValue> : INotifyPropertyChanged
        where TOwner : AttachedAncestorProperty<TOwner, TValue>
    {
        /// <summary>
        /// The update source trigger
        /// </summary>
        private readonly UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        /// <summary>
        /// The path
        /// </summary>
        [CanBeNull] private readonly PropertyPath path;

        /// <summary>
        /// The binding expression
        /// </summary>
        [CanBeNull] private BindingExpression bindingExpression;

        /// <summary>
        /// The host object
        /// </summary>
        [CanBeNull] private DependencyObject ancestor;

        /// <summary>
        ///     The value
        /// </summary>
        [CanBeNull] private TValue value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
        public AttachedAncestorPropertyBindableSetter([NotNull] DependencyObject dependencyObject) => this.DependencyObject = dependencyObject ?? throw new ArgumentNullException(nameof(dependencyObject));

        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="updateSourceTrigger">The update source trigger.</param>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
        public AttachedAncestorPropertyBindableSetter([NotNull] DependencyObject dependencyObject, UpdateSourceTrigger updateSourceTrigger, [NotNull] PropertyPath path)
        {
            this.updateSourceTrigger = updateSourceTrigger;
            this.path = path;
            this.DependencyObject = dependencyObject ?? throw new ArgumentNullException(nameof(dependencyObject));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="updateSourceTrigger">The update source trigger.</param>
        /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
        public AttachedAncestorPropertyBindableSetter([NotNull] DependencyObject dependencyObject, UpdateSourceTrigger updateSourceTrigger)
        {
            this.updateSourceTrigger = updateSourceTrigger;
            this.DependencyObject = dependencyObject ?? throw new ArgumentNullException(nameof(dependencyObject));
        }

        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        public event Action<DependencyObject> HostChanged;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Occurs when [source changed].
        /// </summary>
        public event Action SourceChanged;

        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        public event Action<TValue> ValueChanged;

        /// <summary>
        /// Gets the dependency object.
        /// </summary>
        /// <value>
        /// The dependency object.
        /// </value>
        public DependencyObject DependencyObject { get; }

        /// <summary>
        /// Gets or sets the host object.
        /// </summary>
        /// <value>
        /// The host object.
        /// </value>
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
                this.OnHostChanged(this.ancestor);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public TValue Value
        {
            get => this.value;
            set
            {
                if (EqualityComparer<TValue>.Default.Equals(value, this.value))
                {
                    return;
                }

                Debug.WriteLine("Set Value to new [{0}] old [{1}] DependencyObject {2}", value, this.value, ((FrameworkElement)this.DependencyObject)?.Name);
                this.value = value;
                this.OnValueChanged(value);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Provides the value.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serviceProvider</exception>
        [CanBeNull]
        public BindingExpression ProvideValue([NotNull] IServiceProvider serviceProvider)
        {
            Debug.WriteLine("Provide value DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (this.DependencyObject is FrameworkElement frameworkElement)
            {
                if (frameworkElement.IsLoaded)
                {
                    if (this.UpdateAttachedAncestorProperty())
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
            } else
            {
                this.UpdateAttachedAncestorProperty();

                if (!(this.CreateBinding().ProvideValue(serviceProvider) is BindingExpression expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;
            }

            return this.bindingExpression;
        }

        /// <summary>
        /// Creates the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyProperty">The dependency property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// expression
        /// or
        /// expression
        /// </exception>
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
                    if (this.UpdateAttachedAncestorProperty())
                    {
                        this.UpdateTarget();
                    }
                    Debug.WriteLine("Framework element {0} is loaded", (object)frameworkElement.Name);
                }

                var binding = this.CreateBinding();
                if (!(BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding) is BindingExpression expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;

                frameworkElement.Loaded += this.OnLoaded;
                frameworkElement.Unloaded += this.OnUnloaded;
            } else
            {
                this.UpdateAttachedAncestorProperty();

                var binding = this.CreateBinding();
                if (!(BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding) is BindingExpression expression))
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                this.bindingExpression = expression;
            }
            return this.bindingExpression;
        }

        /// <summary>
        /// Called when [unloaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Unloaded DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            var attachedAncestorProperty = AttachedAncestorProperty<TOwner, TValue>.GetOrCreateShadowAttachedAncestorProperty(this.Ancestor);
            attachedAncestorProperty.AncestorChanged -= this.OnAttachedAncestorChanged;
            attachedAncestorProperty.Unsubscribe -= this.OnUnsubscribe;
            AttachedAncestorProperty<TOwner, TValue>.RemoveValueChangedHandler(this.Ancestor, this.ValueChangedHandler);
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Loaded DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            if (!this.UpdateAttachedAncestorProperty())
            {
                return;
            }

            this.UpdateTarget();
        }

        /// <summary>
        /// Sets the source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool SetSource(TValue value)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, this.value))
            {
                return false;
            }
            Debug.WriteLine("Set Source new [{0}] old [{1}] DependencyObject {2}", value, this.value, (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.value = value;
            this.OnSourceChanged();
            return true;
        }

        /// <summary>
        /// Called when [host changed].
        /// </summary>
        /// <param name="host">The host.</param>
        protected virtual void OnHostChanged(DependencyObject host)
        {
            Debug.WriteLine("On ShadowAttachedAncestorProperty Changed DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            this.HostChanged?.Invoke(host);
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.WriteLine("On Property Changed DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when [value changed].
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void OnValueChanged(TValue value)
        {
            Debug.WriteLine("On Value Changed DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            DependencyObject dependencyObject = this.ancestor;
            if (this.ancestor != null)
            {
                AttachedAncestorProperty<TOwner, TValue>.SetSetter(dependencyObject, value);
            }

            this.ValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Updates the host.
        /// </summary>
        protected virtual bool UpdateAttachedAncestorProperty()
        {
            Debug.WriteLine("Update ShadowAttachedAncestorProperty DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            var ancestorObject = AttachedAncestorProperty<TOwner, TValue>.GetAncestor(this.DependencyObject, out _);
            if (ancestorObject == this.Ancestor)
            {
                if (this.Ancestor != null)
                {
                    AttachedAncestorProperty<TOwner, TValue>.GetShadowAttachedAncestorProperty(this.Ancestor).AncestorChanged += this.OnAttachedAncestorChanged;
                }
                Debug.WriteLine("ShadowAttachedAncestorProperty not changed {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                return false;
            }
            if (this.Ancestor != null)
            {
                Debug.WriteLine("Removing Ancestor {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                AttachedAncestorProperty<TOwner, TValue>.RemoveValueChangedHandler(this.Ancestor, this.ValueChangedHandler);
            }
            else
            {
                Debug.WriteLine("Not Removed Ancestor is null for {0}", this.GetType());
            }

            this.Ancestor = ancestorObject;

            if (this.Ancestor != null)
            {
                Debug.WriteLine("Adding Ancestor {0}", (object)((FrameworkElement)this.Ancestor)?.Name);
                AttachedAncestorProperty<TOwner, TValue>.AddValueChangedHandler(this.Ancestor, this.ValueChangedHandler);
                var shadowAttachedAncestorProperty = AttachedAncestorProperty<TOwner, TValue>.GetOrCreateShadowAttachedAncestorProperty(this.Ancestor);
                shadowAttachedAncestorProperty.AncestorChanged += this.OnAttachedAncestorChanged;
                shadowAttachedAncestorProperty.Unsubscribe += this.OnUnsubscribe;
            } else
            {
                Debug.WriteLine("Not Added Ancestor is null for {0}", this.GetType());
            }
            this.UpdateValue();
            return true;
        }

        /// <summary>
        /// Hosts the on unsubscribe.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnUnsubscribe(object sender, EventArgs e)
        {
            ((ShadowAttachedAncestorProperty)sender).Unsubscribe -= this.OnUnsubscribe;
            AttachedAncestorProperty<TOwner, TValue>.RemoveValueChangedHandler(this.Ancestor, this.ValueChangedHandler);
        }

        /// <summary>
        /// Called when [host changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAttachedAncestorChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("On ShadowAttachedAncestorProperty Changed {0}", (object)((FrameworkElement)((ShadowAttachedAncestorProperty)sender).Ancestor)?.Name);
            ((ShadowAttachedAncestorProperty)sender).AncestorChanged -= this.OnAttachedAncestorChanged;
            this.UpdateAttachedAncestorProperty();
        }

        /// <summary>
        /// Values the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ValueChangedHandler(object sender, EventArgs e)
        {
            Debug.WriteLine("Value Changed Handler DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            var dependencyObject = ((DependencyObject)sender);
            var v = dependencyObject.GetValueSync<TValue>(AttachedAncestorProperty<TOwner, TValue>.SetterProperty);
            if (this.SetSource(v))
            {
                Debug.WriteLine("Value changed handler value {0} to {1}", v, ((FrameworkElement)dependencyObject)?.Name);
            }
        }

        /// <summary>
        /// Updates the target.
        /// </summary>
        protected virtual void UpdateTarget()
        {
            Debug.WriteLine("Update target {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            BindingExpression expression = this.bindingExpression ?? throw new ArgumentNullException("bindingExpression");
            expression.UpdateTarget();
        }

        /// <summary>
        /// Updates the value.
        /// </summary>
        protected virtual void UpdateValue()
        {
            Debug.WriteLine("Update value DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.Value = AttachedAncestorProperty<TOwner, TValue>.GetSetter(this.Ancestor);
            Debug.WriteLine("New Value {0} DependencyObject {1}", this.Value, ((FrameworkElement)this.DependencyObject)?.Name);
        }

        /// <summary>
        /// Bindings this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private Binding CreateBinding()
        {
            Debug.WriteLine("Create Binding DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);

            var binding = new Binding(nameof(Setter.Value))
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
            Debug.WriteLine("On Source Changed DependencyObject {0}", (object)((FrameworkElement)this.DependencyObject)?.Name);
            this.UpdateTarget();
            this.SourceChanged?.Invoke();
        }
    }
}
