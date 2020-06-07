// -----------------------------------------------------------------------
// <copyright file="BehaviorCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.Behaviors.Extensions
{
    /// <summary>
    ///     Represents a collection of behaviors with a shared AssociatedObject and provides change notifications to its
    ///     contents when that AssociatedObject changes.
    /// </summary>
    public sealed class BehaviorContent : AttachableContent<Behavior>
    {
        /// <summary>
        /// Creates a new instance of the BehaviorCollection.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore() => new BehaviorContent();
    }
}
