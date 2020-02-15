// -----------------------------------------------------------------------
// <copyright file="TargetedTriggerAction.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;

    /// <summary>
    ///     Represents an action that can be targeted to affect an object other than its AssociatedObject.
    /// </summary>
    /// <remarks>
    ///     This is an infrastructure class. Action authors should derive from TargetedTriggerAction&lt;T&gt; instead of
    ///     this class.
    /// </remarks>
    public abstract class TargetedTriggerAction : TriggerAction
    {
        /// <summary>
        ///     The target name property
        /// </summary>
        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
            "TargetName",
            typeof(string),
            typeof(TargetedTriggerAction),
            new FrameworkPropertyMetadata(OnTargetNameChanged));

        /// <summary>
        ///     The target object property
        /// </summary>
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            "TargetObject",
            typeof(object),
            typeof(TargetedTriggerAction),
            new FrameworkPropertyMetadata(OnTargetObjectChanged));

        /// <summary>
        ///     The target type constraint
        /// </summary>
        private readonly Type targetTypeConstraint;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TargetedTriggerAction" /> class.
        /// </summary>
        /// <param name="targetTypeConstraint">The target type constraint.</param>
        internal TargetedTriggerAction(Type targetTypeConstraint)
            : base(typeof(DependencyObject))
        {
            this.targetTypeConstraint = targetTypeConstraint;
            this.TargetResolver = new NameResolver();
            this.RegisterTargetChanged();
        }

        /// <summary>
        ///     Gets or sets the name of the object this action targets. If Target is set, this property is ignored. If Target is
        ///     not set and TargetName is not set or cannot be resolved, the target will default to the AssociatedObject. This is a
        ///     dependency property.
        /// </summary>
        /// <value>The name of the target object.</value>
        public string TargetName
        {
            get { return (string)this.GetValue(TargetNameProperty); }
            set { this.SetValue(TargetNameProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by
        ///     TargetName. If an element referred to by TargetName cannot be found, the target will default to the
        ///     AssociatedObject. This is a dependency property.
        /// </summary>
        /// <value>The target object.</value>
        public object TargetObject
        {
            get { return this.GetValue(TargetObjectProperty); }
            set { this.SetValue(TargetObjectProperty, value); }
        }

        /// <summary>
        ///     Gets the associated object type constraint.
        /// </summary>
        /// <value>The associated object type constraint.</value>
        /// <remarks>Define a TypeConstraintAttribute on a derived type to constrain the types it may be attached to.</remarks>
        protected sealed override Type AssociatedObjectTypeConstraint
        {
            get
            {
                AttributeCollection attributes = TypeDescriptor.GetAttributes(this.GetType());
                TypeConstraintAttribute typeConstraintAttribute =
                    attributes[typeof(TypeConstraintAttribute)] as TypeConstraintAttribute;

                if (typeConstraintAttribute != null)
                {
                    return typeConstraintAttribute.Constraint;
                }

                return typeof(DependencyObject);
            }
        }

        /// <summary>
        ///     Gets the target object. If TargetObject is set, returns TargetObject. Else, if TargetName is not set or cannot be
        ///     resolved, defaults to the AssociatedObject.
        /// </summary>
        /// <value>The target object.</value>
        /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
        /// <exception cref="InvalidOperationException">The Target element does not satisfy the type constraint.</exception>
        protected object Target
        {
            get
            {
                object target = this.AssociatedObject;
                if (this.TargetObject != null)
                {
                    target = this.TargetObject;
                } else if (this.IsTargetNameSet)
                {
                    target = this.TargetResolver.Object;
                }

                if (target != null && !this.TargetTypeConstraint.IsAssignableFrom(target.GetType()))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage,
                            this.GetType().Name,
                            target.GetType(),
                            this.TargetTypeConstraint,
                            "Target"));
                }

                return target;
            }
        }

        /// <summary>
        ///     Gets the target type constraint.
        /// </summary>
        /// <value>The target type constraint.</value>
        protected Type TargetTypeConstraint
        {
            get
            {
                this.ReadPreamble();
                return this.targetTypeConstraint;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is target changed registered.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is target changed registered; otherwise, <c>false</c>.
        /// </value>
        private bool IsTargetChangedRegistered
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is target name set.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is target name set; otherwise, <c>false</c>.
        /// </value>
        private bool IsTargetNameSet
        {
            get
            {
                return !string.IsNullOrEmpty(this.TargetName)
                       || this.ReadLocalValue(TargetNameProperty) != DependencyProperty.UnsetValue;
            }
        }

        /// <summary>
        ///     Gets the target resolver.
        /// </summary>
        /// <value>
        ///     The target resolver.
        /// </value>
        private NameResolver TargetResolver
        {
            get;
        }

        /// <summary>
        ///     Called when the target changes.
        /// </summary>
        /// <param name="oldTarget">The old target.</param>
        /// <param name="newTarget">The new target.</param>
        /// <remarks>
        ///     This function should be overriden in derived classes to hook and unhook functionality from the changing source
        ///     objects.
        /// </remarks>
        internal virtual void OnTargetChangedImpl(object oldTarget, object newTarget)
        {
        }

        /// <summary>
        ///     Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            // We can't resolve element names using a Behavior, as it isn't a FrameworkElement.
            // Hence, if we are Hosted on a Behavior, we need to resolve against the Behavior's
            // Host rather than our own. See comment in EventTriggerBase.
            // TODO jekelly 6/20/08: Ideally we could do a namespace walk, but SL doesn't expose
            //						 a way to do this. This solution only looks one level deep.
            //						 A Behavior with a Behavior attached won't work. This is OK
            //						 for now, but should consider a more general solution if needed.
            DependencyObject hostObject = this.AssociatedObject;
            Behavior newBehavior = hostObject as Behavior;

            this.RegisterTargetChanged();
            if (newBehavior != null)
            {
                hostObject = ((IAttachedObject)newBehavior).AssociatedObject;
                newBehavior.AssociatedObjectChanged += this.OnBehaviorHostChanged;
            }

            this.TargetResolver.NameScopeReferenceElement = hostObject as FrameworkElement;
        }

        /// <summary>
        ///     Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            Behavior oldBehavior = this.AssociatedObject as Behavior;
            base.OnDetaching();
            this.OnTargetChangedImpl(this.TargetResolver.Object, null);
            this.UnregisterTargetChanged();

            if (oldBehavior != null)
            {
                oldBehavior.AssociatedObjectChanged -= this.OnBehaviorHostChanged;
            }

            this.TargetResolver.NameScopeReferenceElement = null;
        }

        /// <summary>
        ///     Called when [target name changed].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnTargetNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TargetedTriggerAction targetedTriggerAction = (TargetedTriggerAction)obj;
            targetedTriggerAction.TargetResolver.Name = (string)args.NewValue;
        }

        /// <summary>
        ///     Called when [target object changed].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnTargetObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TargetedTriggerAction targetedTriggerAction = (TargetedTriggerAction)obj;
            targetedTriggerAction.OnTargetChanged(obj, new NameResolvedEventArgs(args.OldValue, args.NewValue));
        }

        /// <summary>
        ///     Called when [behavior host changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnBehaviorHostChanged(object sender, EventArgs e)
        {
            this.TargetResolver.NameScopeReferenceElement =
                ((IAttachedObject)sender).AssociatedObject as FrameworkElement;
        }

        /// <summary>
        ///     Called when [target changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NameResolvedEventArgs" /> instance containing the event data.</param>
        private void OnTargetChanged(object sender, NameResolvedEventArgs e)
        {
            if (this.AssociatedObject != null)
            {
                this.OnTargetChangedImpl(e.OldObject, e.NewObject);
            }
        }

        /// <summary>
        ///     Registers the target changed.
        /// </summary>
        private void RegisterTargetChanged()
        {
            if (!this.IsTargetChangedRegistered)
            {
                this.TargetResolver.ResolvedElementChanged += this.OnTargetChanged;
                this.IsTargetChangedRegistered = true;
            }
        }

        /// <summary>
        ///     Unregisters the target changed.
        /// </summary>
        private void UnregisterTargetChanged()
        {
            if (this.IsTargetChangedRegistered)
            {
                this.TargetResolver.ResolvedElementChanged -= this.OnTargetChanged;
                this.IsTargetChangedRegistered = false;
            }
        }
    }
}
