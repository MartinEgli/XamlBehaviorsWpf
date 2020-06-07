namespace Anori.WPF.Converters
{
    #region

    using Anori.Strings;
    using System;
    using System.Globalization;
    using System.Security;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    #endregion

    /// <inheritdoc cref="MarkupExtension"/>
    /// <inheritdoc cref="IValueConverter"/>
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Markup.MarkupExtension" />
    /// <seealso cref="T:System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(PasswordBox), typeof(SecureString))]
    public class PasswordBoxToSecureStringPasswordConverter : MarkupExtension, IValueConverter

    {
        /// <summary>
        /// The converter
        /// </summary>
        private static readonly IValueConverter Converter = new PasswordBoxToSecureStringPasswordConverter();

        #region IValueConverter Members

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PasswordBox box)
            {
                return box.Password.ToSecureString();
            }

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        #endregion

        /// <inheritdoc />
        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter;
        }
    }
}
