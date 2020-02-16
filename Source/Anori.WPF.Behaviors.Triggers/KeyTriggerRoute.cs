// -----------------------------------------------------------------------
// <copyright file="KeyTriggerRoute.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Triggers
{
    /// <summary>
    ///     Enum for key trigger route of visual tree
    /// </summary>
    public enum KeyTriggerRoute
    {
        /// <summary>
        ///     The tunneling route
        /// </summary>
        Tunneling = 0,

        /// <summary>
        ///     The bubbling route
        /// </summary>
        Bubbling = 1
    }
}
