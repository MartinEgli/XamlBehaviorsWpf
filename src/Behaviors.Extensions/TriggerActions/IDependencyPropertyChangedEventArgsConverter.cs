// -----------------------------------------------------------------------
// <copyright file="IDependencyPropertyChangedEventArgsConverter.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.TriggerActions
{
    using System;
    using System.Windows;

    /// <summary>
    ///     Dependency Property Changed Event Args Converter Interface
    /// </summary>
    public interface IDependencyPropertyChangedEventArgsConverter
    {
        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="associatedObject">The associated object.</param>
        /// <returns>
        ///     The converted object.
        /// </returns>
        object Convert(
            DependencyPropertyChangedEventArgs eventArgs,
            Type targetType,
            object parameter,
            object associatedObject);
    }
}