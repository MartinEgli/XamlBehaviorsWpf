// -----------------------------------------------------------------------
// <copyright file="AttachedBindingGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public abstract class AttachedBindingGetterExtension<T, TOwner> : UpdateableMarkupExtension
        where TOwner : AttachedFork<T, TOwner>

    {
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public PropertyPath Path { get; set; }

        /// <summary>
        /// Gets or sets the update source trigger.
        /// </summary>
        /// <value>
        /// The update source trigger.
        /// </value>
        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get;
            set;
        }

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValueInternal(IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValueTarget =
                (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (provideValueTarget.TargetObject is DependencyObject dependencyObject)

            {
                return new Setter(dependencyObject, this.UpdateSourceTrigger, this.Path).ProvideValue(serviceProvider);
            }

            if (provideValueTarget.TargetObject is System.Windows.Setter setter)

            {
                //                setter.
            }
            //var value = new Setter();
            //var binding = new CreateBinding(nameof(Setter.Value))
            //{
            //    Mode = BindingMode.TwoWay,
            //    Source = value,
            //    UpdateSourceTrigger = this.UpdateSourceTrigger
            //};
            //if (Path != null)
            //{
            //    binding.Path = Path;
            //}

            //var rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            //if (rootObjectProvider.RootObject is FrameworkElement f)
            //{
            //   var p= provideValueTarget.TargetProperty as DependencyProperty;
            //   var o = provideValueTarget.TargetObject as DependencyObject;

            //    f.Loaded += (sender, args) => UpdateValue(BindingOperations.SetBinding(o, p, binding));
            //}

            return null;
        }

        /// <summary>
        /// Setter
        /// </summary>
        /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
        internal class Setter : INotifyPropertyChanged
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
            [CanBeNull] private DependencyObject hostObject;

            /// <summary>
            ///     The value
            /// </summary>
            [CanBeNull] private T value;


            /// <summary>
            /// Initializes a new instance of the <see cref="Setter"/> class.
            /// </summary>
            /// <param name="dependencyObject">The dependency object.</param>
            /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
            public Setter([NotNull] DependencyObject dependencyObject) => this.DependencyObject = dependencyObject ?? throw new ArgumentNullException(nameof(dependencyObject));

            /// <summary>
            /// Initializes a new instance of the <see cref="Setter"/> class.
            /// </summary>
            /// <param name="dependencyObject">The dependency object.</param>
            /// <param name="updateSourceTrigger">The update source trigger.</param>
            /// <param name="path">The path.</param>
            /// <exception cref="System.ArgumentNullException">dependencyObject</exception>
            public Setter([NotNull] DependencyObject dependencyObject, UpdateSourceTrigger updateSourceTrigger, [NotNull] PropertyPath path)
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
            public Setter([NotNull] DependencyObject dependencyObject, UpdateSourceTrigger updateSourceTrigger)
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
            public event Action<T> ValueChanged;

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
            public DependencyObject HostObject
            {
                get
                {
                    return this.hostObject;
                }
                set
                {
                    if (Equals(value, this.hostObject))
                    {
                        return;
                    }
                    this.hostObject = value;
                    this.OnHostChanged(hostObject);
                    this.OnPropertyChanged();
                }
            }
            
            /// <summary>
            ///     Gets or sets the value.
            /// </summary>
            /// <value>
            ///     The value.
            /// </value>
            public T Value
            {
                get => this.value;
                set
                {
                    if (EqualityComparer<T>.Default.Equals(value, this.value))
                    {
                        return;
                    }

                    Debug.WriteLine("Set Value to new [{0}] old [{1}] DependencyObject {2}", value, this.value, ((FrameworkElement)DependencyObject)?.Name);
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
                Debug.WriteLine("Provide value DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

                if (serviceProvider == null)
                {
                    throw new ArgumentNullException(nameof(serviceProvider));
                }

                

                if (DependencyObject is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.IsLoaded)
                    {
                        if (this.UpdateHost())
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

                    frameworkElement.Loaded += OnLoaded;
                    frameworkElement.Unloaded += OnUnloaded;

                } else
                {
                    this.UpdateHost();


                    if (!(this.CreateBinding().ProvideValue(serviceProvider) is BindingExpression expression))
                    {
                        throw new ArgumentNullException(nameof(expression));
                    }

                    this.bindingExpression = expression;

                }

                //SourceChanged += UpdateTarget;

                return bindingExpression;
            }

            private void OnUnloaded(object sender, RoutedEventArgs e)
            {
                Debug.WriteLine("On Unloaded DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                var host = AttachedFork<T, TOwner>.GetOrCreateHost(this.HostObject);
                host.HostChanged -= OnHostChanged;
                host.Unsubscribe -= HostOnUnsubscribe;
                AttachedFork<T, TOwner>.RemoveValueChangedHandler(this.HostObject, ValueChangedHandler);

            }

            /// <summary>
            /// Called when [loaded].
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
            private void OnLoaded(object sender, RoutedEventArgs e)
            {
                Debug.WriteLine("On Loaded DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                if (!this.UpdateHost())
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
            public bool SetSource(T value)
            {
                if (EqualityComparer<T>.Default.Equals(value, this.value))
                {
                    return false;
                }
                Debug.WriteLine("Set Source new [{0}] old [{1}] DependencyObject {2}", value, this.value , (object)((FrameworkElement)DependencyObject)?.Name);
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
                Debug.WriteLine("On Host Changed DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

                this.HostChanged?.Invoke(host);
            }

            /// <summary>
            ///     Called when [property changed].
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                Debug.WriteLine("On Property Changed DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            /// <summary>
            /// Called when [value changed].
            /// </summary>
            /// <param name="value">The value.</param>
            protected virtual void OnValueChanged(T value)
            {
                Debug.WriteLine("On Value Changed DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

                AttachedFork<T, TOwner>.SetSetter(this.hostObject,value);
                this.ValueChanged?.Invoke(value);
            }

            /// <summary>
            /// Updates the host.
            /// </summary>
            protected virtual bool UpdateHost()
            {
                Debug.WriteLine("Update Host DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

                var hostObject = AttachedFork<T, TOwner>.GetAttachedHostObject(this.DependencyObject ,out _);
                if (hostObject == this.HostObject)
                {
                    if (this.HostObject != null)
                    {
                        AttachedFork<T, TOwner>.GetHost(this.HostObject).HostChanged += OnHostChanged;
                    }
                    Debug.WriteLine("Host not changed {0}", (object)((FrameworkElement)HostObject)?.Name);
                    return false;
                }
                if (this.HostObject != null)
                {
                    Debug.WriteLine("Removing HostObject {0}", (object)((FrameworkElement)HostObject)?.Name);
                    AttachedFork<T, TOwner>.RemoveValueChangedHandler(this.HostObject, ValueChangedHandler);
 //                   AttachedFork<T, TOwner>.GetHost(this.HostObject).HostChanged -= OnHostChanged;
                } else
                {
                    Debug.WriteLine("Not Removed HostObject is null for {0}", this.GetType());
                }

                this.HostObject = hostObject;

                if (this.HostObject != null)
                {
                    Debug.WriteLine("Adding HostObject {0}", (object)((FrameworkElement)HostObject)?.Name);
                    AttachedFork<T, TOwner>.AddValueChangedHandler(this.HostObject, ValueChangedHandler);
                    var host = AttachedFork<T, TOwner>.GetOrCreateHost(this.HostObject);
                    host.HostChanged += OnHostChanged;
                    host.Unsubscribe += HostOnUnsubscribe;
                } else
                {
                    Debug.WriteLine("Not Added HostObject is null for {0}", this.GetType());
                }
                this.UpdateValue();
                return true;

            }

            private void HostOnUnsubscribe(object sender, EventArgs e)
            {
                ((Host<T>)sender).Unsubscribe -= HostOnUnsubscribe;
                AttachedFork<T, TOwner>.RemoveValueChangedHandler(this.HostObject, ValueChangedHandler);
            }

            /// <summary>
            /// Called when [host changed].
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            private void OnHostChanged(object sender, EventArgs e)
            {
                Debug.WriteLine("On Host Changed {0}", (object)((FrameworkElement)((Host<T>)sender).Owner)?.Name);
                ((Host<T>)sender).HostChanged -= OnHostChanged;
                this.UpdateHost();
            }

            /// <summary>
            /// Values the changed handler.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            private void ValueChangedHandler(object sender, EventArgs e)
            {
                Debug.WriteLine("Value Changed Handler DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                var dependencyObject = ((DependencyObject)sender);
                var v = dependencyObject.GetValueSync<T>(AttachedFork<T, TOwner>.SetterProperty);
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
                Debug.WriteLine("Update target {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                BindingExpression expression = this.bindingExpression ?? throw new ArgumentNullException("bindingExpression");
                expression.UpdateTarget();
            }

            /// <summary>
            /// Updates the value.
            /// </summary>
            protected virtual void UpdateValue()
            {
                Debug.WriteLine("Update value DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                this.Value = AttachedFork<T, TOwner>.GetSetter(this.HostObject);
                Debug.WriteLine("New Value {0} DependencyObject {1}", this.Value, ((FrameworkElement)this.DependencyObject)?.Name);
            }

            /// <summary>
            /// Bindings this instance.
            /// </summary>
            /// <returns></returns>
            [NotNull]
            private Binding CreateBinding()
            {
                Debug.WriteLine("Create Binding DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);

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
                Debug.WriteLine("On Source Changed DependencyObject {0}", (object)((FrameworkElement)DependencyObject)?.Name);
                this.UpdateTarget();
                this.SourceChanged?.Invoke();
            }
        }
    }
}

