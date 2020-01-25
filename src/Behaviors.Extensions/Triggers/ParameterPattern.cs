// -----------------------------------------------------------------------
// <copyright file="ParameterPattern.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Triggers
{
    /// <summary>
    ///     Parameter Pattern
    /// </summary>
    public enum ParameterPattern
    {
        /// <summary>
        ///     The none
        /// </summary>
        None,

        /// <summary>
        ///     The event arguments
        /// </summary>
        EventArgs,

        /// <summary>
        ///     The associated object
        /// </summary>
        AssociatedObject,

        /// <summary>
        ///     The source
        /// </summary>
        Source,

        /// <summary>
        ///     The original source
        /// </summary>
        OriginalSource
    }
}