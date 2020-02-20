﻿// -----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media.Animation;

    /// <summary>
    ///     Represents an attachable object that encapsulates a unit of functionality.
    /// </summary>
    /// <remarks>
    ///     This is an infrastructure class. Action authors should derive from TriggerAction&lt;T&gt; instead of this
    ///     class.
    /// </remarks>
    [DefaultTrigger(typeof(UIElement), typeof(EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
    public abstract class TriggerAction : Animatable, IAttachedObject
    {
        /// <summary>
        ///     The identifier source
        /// </summary>
        private static int idSource;

        /// <summary>
        ///     The is enabled property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(
            "IsEnabled",
            typeof(bool),
            typeof(TriggerAction),
            new FrameworkPropertyMetadata(true));

        /// <summary>
        ///     The associated object
        /// </summary>
        private DependencyObject associatedObject;

        /// <summary>
        ///     The associated object type constraint
        /// </summary>
        private readonly Type associatedObjectTypeConstraint;

        /// <summary>
        ///     The is hosted
        /// </summary>
        private bool isHosted;

        internal TriggerAction(Type associatedObjectTypeConstraint)
        {
            this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; } = Interlocked.Increment(ref idSource);

        /// <summary>
        ///     Gets or sets a value indicating whether this action will run when invoked. This is a dependency property.
        /// </summary>
        /// <value>
        ///     <c>True</c> if this action will be run when invoked; otherwise, <c>False</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return (bool)this.GetValue(IsEnabledProperty); }
            set
            {
                this.SetValue(IsEnabledProperty, value);
            }
        }

        /// <summary>
        ///     Gets the object to which this action is attached.
        /// </summary>
        /// <value>The associated object.</value>
        protected DependencyObject AssociatedObject
        {
            get
            {
                this.ReadPreamble();
                return this.associatedObject;
            }
        }

        /// <summary>
        ///     Gets the associated object type constraint.
        /// </summary>
        /// <value>The associated object type constraint.</value>
        protected virtual Type AssociatedObjectTypeConstraint
        {
            get
            {
                this.ReadPreamble();
                return this.associatedObjectTypeConstraint;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is attached.
        /// </summary>
        /// <value><c>True</c> if this instance is attached; otherwise, <c>False</c>.</value>
        internal bool IsHosted
        {
            get
            {
                this.ReadPreamble();
                return this.isHosted;
            }
            set
            {
                this.WritePreamble();
                this.isHosted = value;
                this.WritePostscript();
            }
        }

        /// <summary>
        ///     Attempts to invoke the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        public void CallInvoke(object parameter)
        {
            if (this.IsEnabled)
            {
                this.Invoke(parameter);
            }
        }

        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected abstract void Invoke(object parameter);

        /// <summary>
        ///     Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        ///     Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected virtual void OnDetaching()
        {
        }

        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
        {
            Type classType = this.GetType();
            return (Freezable)Activator.CreateInstance(classType);
        }

        #region IAttachedObject Members

        /// <summary>
        ///     Gets the associated object.
        /// </summary>
        /// <value>The associated object.</value>
        DependencyObject IAttachedObject.AssociatedObject
        {
            get
            {
                return this.AssociatedObject;
            }
        }

        /// <summary>
        ///     Attaches to the specified object.
        /// </summary>
        /// <param name="dependencyObject">The object to attach to.</param>
        /// <exception cref="InvalidOperationException">Cannot host the same TriggerAction on more than one object at a time.</exception>
        /// <exception cref="InvalidOperationException">dependencyObject does not satisfy the TriggerAction type constraint.</exception>
        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException(
                        ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
                }

                // Ensure the type constraint is met
                if (dependencyObject != null
                    && !this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            ExceptionStringTable.TypeConstraintViolatedExceptionMessage,
                            this.GetType().Name,
                            dependencyObject.GetType().Name,
                            this.AssociatedObjectTypeConstraint.Name));
                }

                this.WritePreamble();
                this.associatedObject = dependencyObject;
                this.WritePostscript();

                this.OnAttached();
            }
        }

        /// <summary>
        ///     Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            this.OnDetaching();
            this.WritePreamble();
            this.associatedObject = null;
            this.WritePostscript();
        }

        #endregion IAttachedObject Members
    }
}
