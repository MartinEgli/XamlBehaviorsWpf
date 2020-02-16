// -----------------------------------------------------------------------
// <copyright file="IRoutedEventArgsConverter.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Converters
{
    using System;
    using System.Windows;

    /// <summary>
    ///     Routed Event Args Converter Interface
    /// </summary>
    public interface IRoutedEventArgsConverter
    {
        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="eventArgs">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="associatedObject">The associated object.</param>
        /// <returns>The Converter</returns>
        object Convert(RoutedEventArgs eventArgs, Type targetType, object parameter, object associatedObject);
    }
}
