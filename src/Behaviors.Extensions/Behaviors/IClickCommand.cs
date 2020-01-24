// -----------------------------------------------------------------------
// <copyright file="IClickCommand.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows.Input;

    /// <summary>
    ///     IClickCommand interface.
    /// </summary>
    public interface IClickCommand
    {
        /// <summary>
        ///     Gets the click command.
        /// </summary>
        /// <value>
        ///     The click command.
        /// </value>
        ICommand ClickCommand { get; }
    }
}