// -----------------------------------------------------------------------
// <copyright file="DependencyObjectExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Extensions
{
    #region

    using System;
    using System.Windows;
    using System.Windows.Media;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     Dependency Object Extensions
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        ///     Gets the root.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        public static UIElement GetRoot([NotNull] this DependencyObject current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            UIElement uiElement = null;
            for (; current != null; current = VisualTreeHelper.GetParent(current))
            {
                uiElement = current as UIElement;
            }

            return uiElement;
        }

        /// <summary>
        ///     Gets the window.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">current</exception>
        public static Window GetWindow([NotNull] this DependencyObject current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            return Window.GetWindow(current);
        }
    }
}
