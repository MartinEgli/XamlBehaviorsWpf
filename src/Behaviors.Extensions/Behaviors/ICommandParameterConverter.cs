// -----------------------------------------------------------------------
// <copyright file="ICommandParameterConverter.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     ICommandParameterConverter Interface.
    /// </summary>
    public interface ICommandParameterConverter
    {
        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The Value.</returns>
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    }
}