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
        /// <returns></returns>
        TriggerBase Create();
    }

    public interface ITriggerActionCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        TriggerAction Create();

        void Attach(DependencyObject obj);
        void Detach(DependencyObject obj);
    }
}
