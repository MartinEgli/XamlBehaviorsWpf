// -----------------------------------------------------------------------
// <copyright file="ICommandParameterConverter.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Converters
{
    using System;
    using System.Globalization;

    /// <summary>
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
        /// <returns></returns>
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
