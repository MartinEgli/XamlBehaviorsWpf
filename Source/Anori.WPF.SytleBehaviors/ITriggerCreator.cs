// -----------------------------------------------------------------------
// <copyright file="ITriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System.Windows;

    using TriggerBase = Anori.WPF.Behaviors.TriggerBase;

    /// <summary>
    /// </summary>
    public interface ITriggerCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        TriggerBase Create(DependencyObject dependencyObject);
    }
}
