// -----------------------------------------------------------------------
// <copyright file="IsNotEmptyStringConverter.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Converters
{
    #region

    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    using Anori.Strings;

    #endregion

    /// <summary>
    ///     Not Empty String Converter
    /// </summary>
    /// <seealso cref="System.Windows.Markup.MarkupExtension" />
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(string), typeof(bool))]
    public class IsNotEmptyStringConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        ///     The converter
        /// </summary>
        private static readonly IValueConverter Converter = new IsNotEmptyStringConverter();

        /// <inheritdoc />
        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter;
        }

        #region IValueConverter Members

        /// <inheritdoc />
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return !s.IsNullOrEmpty();
            }

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
