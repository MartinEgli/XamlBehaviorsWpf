// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventParameterPattern.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Anori.WPF.Triggers
{
    /// <summary>
    /// Dependency Property Changed Event Parameter Pattern
    /// </summary>
    public enum DependencyPropertyChangedEventParameterPattern
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
        ///     The new value
        /// </summary>
        NewValue,

        /// <summary>
        ///     The old value
        /// </summary>
        OldValue,

        /// <summary>
        ///     The property
        /// </summary>
        Property,

        /// <summary>
        ///     The associated object
        /// </summary>
        AssociatedObject
    }
}