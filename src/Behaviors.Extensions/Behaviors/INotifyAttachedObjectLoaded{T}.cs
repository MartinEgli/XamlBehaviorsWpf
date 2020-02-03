// -----------------------------------------------------------------------
// <copyright file="INotifyAttachedObjectLoaded{T}.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;

    /// <summary>
    ///     Interface to notify an attached object is loaded.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface INotifyAttachedObjectLoaded<out T>
    {
        /// <summary>
        ///     Occurs when [attached object loaded].
        /// </summary>
        event EventHandler AttachedObjectLoaded;

        /// <summary>
        ///     Gets the attached object.
        /// </summary>
        /// <returns>The Attached Object.</returns>
        T GetAttachedObject();
    }
}