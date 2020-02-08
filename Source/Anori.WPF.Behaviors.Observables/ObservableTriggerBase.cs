﻿// -----------------------------------------------------------------------
// <copyright file="ObservableTriggerBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;
    using System.Windows;

    using JetBrains.Annotations;

    using TriggerBase = Anori.WPF.Behaviors.TriggerBase;

    /// <summary>
    ///     Represents a trigger that can listen to an object other than its AssociatedObject.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    /// <seealso cref="Anori.WPF.Behaviors.TriggerBase" />
    /// <seealso cref="System.IObserver{TPayload}" />
    public abstract class ObservableTriggerBase<TPayload> : TriggerBase, IObserver<TPayload>
    {
        /// <summary>
        ///     The source name property
        /// </summary>
        public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register(
            "SourceName",
            typeof(string),
            typeof(ObservableTriggerBase<TPayload>),
            new PropertyMetadata(OnSourceNameChanged));

        /// <summary>
        ///     The source object property
        /// </summary>
        public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register(
            "SourceObject",
            typeof(object),
            typeof(ObservableTriggerBase<TPayload>),
            new PropertyMetadata(OnSourceObjectChanged));

        /// <summary>
        ///     The observable dispose
        /// </summary>
        private IDisposable observerDispose;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableTriggerBase{T}" /> class.
        /// </summary>
        protected ObservableTriggerBase()
            : base(typeof(DependencyObject))
        {
            this.SourceTypeConstraint = typeof(TPayload);
            this.SourceNameResolver = new NameResolver();
            this.RegisterSourceChanged();
        }

        /// <summary>
        ///     Gets the resolved source. If <c ref="SourceName" /> is not set or cannot be resolved, defaults to AssociatedObject.
        /// </summary>
        /// <value>The resolved source object.</value>
        /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
        /// <exception cref="InvalidOperationException">
        ///     The element pointed to by <c cref="Source" /> does not satisify the type
        ///     constraint.
        /// </exception>
        public object Source
        {
            get
            {
                object source = this.AssociatedObject;

                if (this.SourceObject != null)
                {
                    source = this.SourceObject;
                } else if (this.IsSourceNameSet)
                {
                    source = this.SourceNameResolver.Object;
                    if (source != null && !this.SourceTypeConstraint.IsInstanceOfType(source))
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                ExceptionStrings.RetargetedTypeConstraintViolatedExceptionMessage,
                                this.GetType().Name,
                                source.GetType(),
                                this.SourceTypeConstraint,
                                "Source"));
                    }
                }

                return source;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the element this EventTriggerBase listens for as a source. If the name is not set or
        ///     cannot be resolved, the AssociatedObject will be used.  This is a dependency property.
        /// </summary>
        /// <value>The name of the source element.</value>
        public string SourceName
        {
            get => (string)this.GetValue(SourceNameProperty);

            set => this.SetValue(SourceNameProperty, value);
        }

        /// <summary>
        ///     Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by
        ///     TargetName. If an element referred to by TargetName cannot be found, the target will default to the
        ///     AssociatedObject. This is a dependency property.
        /// </summary>
        /// <value>The target object.</value>
        public object SourceObject
        {
            get => this.GetValue(SourceObjectProperty);

            set => this.SetValue(SourceObjectProperty, value);
        }

        /// <summary>
        ///     Gets the type constraint of the associated object.
        /// </summary>
        /// <value>
        ///     The associated object type constraint.
        /// </value>
        /// <remarks>
        ///     Define a TypeConstraintAttribute on a derived type to constrain the types it may be attached to.
        /// </remarks>
        protected sealed override Type AssociatedObjectTypeConstraint
        {
            get
            {
                AttributeCollection attributes = TypeDescriptor.GetAttributes(this.GetType());

                if (attributes[typeof(TypeConstraintAttribute)] is TypeConstraintAttribute typeConstraintAttribute)
                {
                    return typeConstraintAttribute.Constraint;
                }

                return typeof(DependencyObject);
            }
        }

        /// <summary>
        ///     Gets the source type constraint.
        /// </summary>
        /// <value>The source type constraint.</value>
        protected Type SourceTypeConstraint { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is loaded registered.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is loaded registered; otherwise, <c>false</c>.
        /// </value>
        private bool IsLoadedRegistered { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is source changed registered.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is source changed registered; otherwise, <c>false</c>.
        /// </value>
        private bool IsSourceChangedRegistered { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is source name set.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is source name set; otherwise, <c>false</c>.
        /// </value>
        private bool IsSourceNameSet =>
            !string.IsNullOrEmpty(this.SourceName)
            || this.ReadLocalValue(SourceNameProperty) != DependencyProperty.UnsetValue;

        /// <summary>
        ///     Gets the source name resolver.
        /// </summary>
        /// <value>
        ///     The source name resolver.
        /// </value>
        private NameResolver SourceNameResolver { get; }

        /// <summary>
        ///     Called when [event name changed].
        /// </summary>
        /// <param name="oldEventName">Old name of the event.</param>
        /// <param name="newEventName">New name of the event.</param>
        internal void OnObservableNameChanged(string oldEventName, string newEventName)
        {
            if (this.AssociatedObject == null)
            {
                return;
            }

            this.UnregisterObserver();
            this.RegisterObserver(this.Source, newEventName);
        }

        /// <summary>
        ///     Called when the source changes.
        /// </summary>
        /// <param name="oldSource">The old source.</param>
        /// <param name="newSource">The new source.</param>
        /// <remarks>
        ///     This function should be overridden in derived classes to hook functionality to and unhook functionality from
        ///     the changing source objects.
        /// </remarks>
        internal virtual void OnSourceChangedImplementation(object oldSource, object newSource)
        {
            if (string.IsNullOrEmpty(this.GetObservableName()))
            {
                return;
            }

            if (oldSource != null && this.SourceTypeConstraint.IsInstanceOfType(oldSource))
            {
                this.UnregisterObserver();
            }

            if (newSource != null && this.SourceTypeConstraint.IsInstanceOfType(newSource))
            {
                this.RegisterObserver(newSource, this.GetObservableName());
            }
        }

        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage(
            "Anori.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "NikhilKo convinced us this was the right choice.")]
        protected abstract string GetObservableName();

        /// <summary>
        ///     Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            DependencyObject newHost = this.AssociatedObject;
            Behavior newBehavior = newHost as Behavior;
            FrameworkElement newHostElement = newHost as FrameworkElement;

            this.RegisterSourceChanged();
            if (newBehavior != null)
            {
                newBehavior.AssociatedObjectChanged += this.OnBehaviorHostChanged;
            } else if (this.SourceObject != null || newHostElement == null)
            {
                try
                {
                    this.OnSourceChanged(null, this.Source);
                } catch (InvalidOperationException)
                {
                    // If we're hosted on a DependencyObject, we don't have a name scope reference element.
                    // Hence, we'll fire off the source changed manually when we first attach. However, if we've
                    // been attached to something that doesn't meet the target type constraint, accessing Source
                    // will throw.
                }
            } else
            {
                this.SourceNameResolver.NameScopeReferenceElement = newHostElement;
            }
        }

        /// <summary>
        ///     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            Behavior oldBehavior = this.AssociatedObject as Behavior;

            try
            {
                this.OnSourceChanged(this.Source, null);
            } catch (InvalidOperationException)
            {
                // We fire off the source changed manually when we detach. However, if we've been attached to
                // something that doesn't meet the target type constraint, accessing Source will throw.
            }

            this.UnregisterSourceChanged();

            if (oldBehavior != null)
            {
                oldBehavior.AssociatedObjectChanged -= this.OnBehaviorHostChanged;
            }

            this.SourceNameResolver.NameScopeReferenceElement = null;
        }

        /// <summary>
        ///     Determines whether [is valid observable] [the specified property information].
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>
        ///     <c>true</c> if [is valid observable] [the specified property information]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidObservable<TPayload>(PropertyInfo propertyInfo)
        {
            Type observableType = propertyInfo.PropertyType;
            if (!typeof(IObservable<TPayload>).IsAssignableFrom(observableType))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Called when [source name changed].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnSourceNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ObservableTriggerBase<TPayload> trigger = (ObservableTriggerBase<TPayload>)obj;
            trigger.SourceNameResolver.Name = (string)args.NewValue;
        }

        /// <summary>
        ///     Called when [source object changed].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnSourceObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ObservableTriggerBase<TPayload> observableTriggerBase = (ObservableTriggerBase<TPayload>)obj;
            object sourceNameObject = observableTriggerBase.SourceNameResolver.Object;
            if (args.NewValue == null)
            {
                observableTriggerBase.OnSourceChanged(args.OldValue, sourceNameObject);
            } else
            {
                if (args.OldValue == null && sourceNameObject != null)
                {
                    observableTriggerBase.UnregisterObserver();
                }

                observableTriggerBase.OnSourceChanged(args.OldValue, args.NewValue);
            }
        }

        /// <summary>
        ///     Called when [behavior host changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnBehaviorHostChanged(object sender, EventArgs e)
        {
            this.SourceNameResolver.NameScopeReferenceElement =
                ((IAttachedObject)sender).AssociatedObject as FrameworkElement;
        }

        /// <summary>
        ///     Called when [source changed].
        /// </summary>
        /// <param name="oldSource">The old source.</param>
        /// <param name="newSource">The new source.</param>
        private void OnSourceChanged(object oldSource, object newSource)
        {
            if (this.AssociatedObject != null)
            {
                this.OnSourceChangedImplementation(oldSource, newSource);
            }
        }

        /// <summary>
        ///     Called when [source name resolver element changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NameResolvedEventArgs" /> instance containing the event data.</param>
        private void OnSourceNameResolverElementChanged(object sender, NameResolvedEventArgs e)
        {
            if (this.SourceObject == null)
            {
                this.OnSourceChanged(e.OldObject, e.NewObject);
            }
        }

        /// <summary>
        ///     Registers the event.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="observableName">Name of the event.</param>
        /// <exception cref="System.ArgumentException">
        ///     observable
        /// </exception>
        /// <exception cref="ArgumentException">Could not find observableName on the Target.</exception>
        private void RegisterObserver([NotNull] object obj, [NotNull] string observableName)
        {
            Type targetType = obj.GetType();
            PropertyInfo propertyInfo = targetType.GetProperty(observableName);
            if (propertyInfo == null)
            {
                if (this.SourceObject != null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            ExceptionStrings.EventTriggerCannotFindEventNameExceptionMessage,
                            observableName,
                            obj.GetType().Name));
                }

                return;
            }

            if (!IsValidObservable<TPayload>(propertyInfo))
            {
                if (this.SourceObject != null)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            ExceptionStrings.EventTriggerBaseInvalidEventExceptionMessage,
                            observableName,
                            obj.GetType().Name));
                }

                return;
            }

            if (!(propertyInfo.GetValue(obj, null) is IObservable<TPayload> observable))
            {
                throw new ArgumentException(nameof(observable));
            }

            this.observerDispose = observable.Subscribe(this);
        }

        /// <summary>
        ///     Registers the source changed.
        /// </summary>
        private void RegisterSourceChanged()
        {
            if (this.IsSourceChangedRegistered)
            {
                return;
            }

            this.SourceNameResolver.ResolvedElementChanged += this.OnSourceNameResolverElementChanged;
            this.IsSourceChangedRegistered = true;
        }

        /// <summary>
        ///     Unregisters the observer.
        /// </summary>
        private void UnregisterObserver()
        {
            this.observerDispose.Dispose();
            this.observerDispose = null;
        }

        /// <summary>
        ///     Unregisters the source changed.
        /// </summary>
        private void UnregisterSourceChanged()
        {
            if (!this.IsSourceChangedRegistered)
            {
                return;
            }

            this.SourceNameResolver.ResolvedElementChanged -= this.OnSourceNameResolverElementChanged;
            this.IsSourceChangedRegistered = false;
        }

        public void OnNext(TPayload value)
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}