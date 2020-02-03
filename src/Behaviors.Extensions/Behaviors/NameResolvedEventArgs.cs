// -----------------------------------------------------------------------
// <copyright file="NameResolvedEventArgs.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;

    /// <summary>
    ///     Provides data about which objects were affected when resolving a name change.
    /// </summary>
    public sealed class NameResolvedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NameResolvedEventArgs" /> class.
        /// </summary>
        /// <param name="oldObject">The old object.</param>
        /// <param name="newObject">The new object.</param>
        public NameResolvedEventArgs(object oldObject, object newObject)
        {
            this.OldObject = oldObject;
            this.NewObject = newObject;
        }

        /// <summary>
        ///     Gets the old object.
        /// </summary>
        /// <value>
        ///     The old object.
        /// </value>
        public object OldObject { get; }

        /// <summary>
        ///     Gets the new object.
        /// </summary>
        /// <value>
        ///     The new object.
        /// </value>
        public object NewObject { get; }
    }
}