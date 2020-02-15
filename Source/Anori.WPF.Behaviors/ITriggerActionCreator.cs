// -----------------------------------------------------------------------
// <copyright file="ITriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.Behaviors
{
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
