// -----------------------------------------------------------------------
// <copyright file="ITriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System.Windows;

    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;

    /// <summary>
    /// </summary>
    public interface ITriggerActionCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        TriggerAction Create(DependencyObject dependencyObject);
    }
}
