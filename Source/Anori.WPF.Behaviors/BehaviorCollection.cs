﻿// -----------------------------------------------------------------------
// <copyright file="BehaviorCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Windows;

    /// <summary>
    ///     Represents a collection of behaviors with a shared AssociatedObject and provides change notifications to its
    ///     contents when that AssociatedObject changes.
    /// </summary>
    public sealed class BehaviorCollection : AttachableCollection<Behavior>
    {
        /// <summary>
        ///     Called immediately after the collection is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            foreach (Behavior behavior in this)
            {
                behavior.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        ///     Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            foreach (Behavior behavior in this)
            {
                behavior.Detach();
            }
        }

        /// <summary>
        ///     Called when a new item is added to the collection.
        /// </summary>
        /// <param name="item">The new item.</param>
        internal override void ItemAdded(Behavior item)
        {
            if (this.AssociatedObject != null)
            {
                item.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        ///     Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The removed item.</param>
        internal override void ItemRemoved(Behavior item)
        {
            if (((IAttachedObject)item).AssociatedObject != null)
            {
                item.Detach();
            }
        }

        /// <summary>
        ///     Creates a new instance of the BehaviorCollection.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BehaviorCollection();
        }
    }
}
