// -----------------------------------------------------------------------
// <copyright file="HashCode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    public readonly struct HashCode
    {
        /// <summary>
        ///     The value
        /// </summary>
        private readonly int value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HashCode" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public HashCode(int value) => this.value = value;

        /// <summary>
        ///     Gets the start.
        /// </summary>
        /// <value>
        ///     The start.
        /// </value>
        public static HashCode Start { get; } = new HashCode(17);

        /// <summary>
        ///     Performs an implicit conversion from <see cref="HashCode" /> to <see cref="System.Int32" />.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator int(HashCode hash) => hash.value;

        /// <summary>
        ///     Hashes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public HashCode Hash<T>(T obj)
        {
            int h = EqualityComparer<T>.Default.GetHashCode(obj);
            return unchecked(new HashCode((this.value * 31) + h));
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => this.value;

        /// <summary>
        ///     Hashes the fullname of from type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public HashCode HashFromTypeName(Type type)
        {
            if (type.FullName != null)
            {
                return new HashCode(type.FullName.GetHashCode());
            }

            return new HashCode(type.GetHashCode());
        }
    }
}
