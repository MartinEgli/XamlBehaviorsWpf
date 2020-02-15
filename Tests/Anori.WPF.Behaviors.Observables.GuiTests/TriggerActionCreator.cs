// -----------------------------------------------------------------------
// <copyright file="TriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using JetBrains.Annotations;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerActionCreator" />
    public abstract class TriggerActionCreator<TTriggerAction> : ITriggerActionCreator
        where TTriggerAction : TriggerAction
    {
        /// <summary>
        ///     The unregister triggerAction
        /// </summary>
        //private List<Action> unregisterActions;

        public TriggerAction Create(DependencyObject dependencyObject)
        {
            TTriggerAction action = CreateTriggerAction();
            this.Register(action, dependencyObject);
            return action;
        }

        /// <summary>
        ///     Attaches the specified associated object.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="associatedObject">The associated object.</param>
        /// <exception cref="ArgumentNullException">associatedObject</exception>
        public void Register(TTriggerAction action, [NotNull] DependencyObject associatedObject)
        {
            if (associatedObject == null)
            {
                throw new ArgumentNullException(nameof(associatedObject));
            }

            if (associatedObject is FrameworkElement frameworkElement)
            {
                DependencyPropertyChangedEventHandler OnDataContextChanged =
                    (sender, args) => DataContextChanged(action, args.NewValue);

                //void Action()
                //{
                //    frameworkElement.DataContextChanged -= OnDataContextChanged;
                //}

                //this.unregisterActions.Add(Action); //?.Invoke();
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
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        abstract public TTriggerAction CreateTriggerAction();

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        abstract protected void DataContextChanged(TTriggerAction triggerAction, object dataContext);

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
