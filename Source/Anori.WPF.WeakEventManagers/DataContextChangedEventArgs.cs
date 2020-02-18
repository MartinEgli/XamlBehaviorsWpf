// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventArgsEx.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.WeakEventManagers
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DataContextChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public DataContextChangedEventArgs(object oldValue, object newValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        /// <summary>
        /// Creates new value.
        /// </summary>
        /// <value>
        /// The new value.
        /// </value>
        public object NewValue { get; }

        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>
        /// The old value.
        /// </value>
        public object OldValue { get; }
    }
}
