// -----------------------------------------------------------------------
// <copyright file="TriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System;
    using System.Windows;

    using Anori.WPF.WeakEventManagers;

    using JetBrains.Annotations;

    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;

    /// <summary>
    /// </summary>
    /// <seealso cref="ITriggerActionCreator" />
    public abstract class TriggerActionCreator<TTriggerAction> : ITriggerActionCreator
        where TTriggerAction : TriggerAction
    {
        /// <summary>
        ///     The unregister triggerAction
        /// </summary>
        //private List<Action> unregisterActions;
        public TriggerAction Create([NotNull] DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            TTriggerAction action = this.CreateTriggerAction();
            this.Register(action, dependencyObject);
            return action;
        }

        /// <summary>
        ///     Attaches the specified associated object.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="associatedObject">The associated object.</param>
        /// <exception cref="ArgumentNullException">associatedObject</exception>
        public void Register([NotNull] TTriggerAction action, [NotNull] DependencyObject associatedObject)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (associatedObject == null)
            {
                throw new ArgumentNullException(nameof(associatedObject));
            }

            if (associatedObject is FrameworkElement frameworkElement)
            {
                DependencyPropertyChangedEventHandler OnDataContextChanged =
                    (sender, args) => this.DataContextChanged(action, args.NewValue);

                frameworkElement.DataContextChanged += OnDataContextChanged;
            }
        }

        ///// <summary>
        /////     Detaches the specified associated object.
        ///// </summary>
        ///// <param name="associatedObject">The associated object.</param>
        ///// <exception cref="NotImplementedException"></exception>
        //public void Detach([NotNull] DependencyObject associatedObject)
        //{
        //    this.Unregister();
        //}

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        abstract public TTriggerAction CreateTriggerAction();

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="triggerAction">The trigger action.</param>
        /// <param name="dataContext">The data context.</param>
        abstract protected void DataContextChanged(
            [NotNull] TTriggerAction triggerAction,
            [NotNull] object dataContext);

        ///// <summary>
        /////     Unregisters this instance.
        ///// </summary>
        //public void UnregisterAll()
        //{
        //    foreach (Action unregisterAction in this.unregisterActions)
        //    {
        //        unregisterAction();
        //    }
        //}
    }
}
