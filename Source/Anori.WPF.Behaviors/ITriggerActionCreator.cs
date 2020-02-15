// -----------------------------------------------------------------------
// <copyright file="ITriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public interface ITriggerActionCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        TriggerAction Create();

        /// <summary>
        /// Attaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        void Attach(DependencyObject associatedObject);

        /// <summary>
        /// Detaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        void Detach(DependencyObject associatedObject);
    }
}
