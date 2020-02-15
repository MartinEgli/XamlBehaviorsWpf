// -----------------------------------------------------------------------
// <copyright file="ITriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.Behaviors
{
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
