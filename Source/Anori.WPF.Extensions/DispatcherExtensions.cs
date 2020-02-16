// -----------------------------------------------------------------------
// <copyright file="DispatcherExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Extensions
{
    #region

    using System;
    using System.Windows.Threading;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     Dispatcher Extensions Module
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dispatcherObject">The dispatcherObject.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dispatcherObject
        ///     or
        ///     action
        /// </exception>
        public static void Dispatch<T>([NotNull] this T dispatcherObject, [NotNull] Action<T> action)
            where T : DispatcherObject
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!dispatcherObject.CheckAccess())
            {
                action(dispatcherObject);
            }

            dispatcherObject.Dispatcher?.Invoke(() => action(dispatcherObject));
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="dispatcherObject">The dispatcherObject.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dispatcherObject
        ///     or
        ///     action
        /// </exception>
        public static void Dispatch([NotNull] this DispatcherObject dispatcherObject, [NotNull] Action action)

        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!dispatcherObject.CheckAccess())
            {
                action();
            }

            dispatcherObject.Dispatcher?.Invoke(() => action);
        }
    }
}
