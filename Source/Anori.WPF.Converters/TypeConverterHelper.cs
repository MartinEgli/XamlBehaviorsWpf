// -----------------------------------------------------------------------
// <copyright file="TypeConverterHelper.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Converters
{
    #region

    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using JetBrains.Annotations;

    #endregion

    public static class TypeConverterHelper
    {
        /// <summary>
        ///     Does the conversion from.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">converter</exception>
        public static object DoConversionFrom([NotNull] this TypeConverter converter, object value)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            object returnValue = value;

            try
            {
                if (value != null && converter.CanConvertFrom(value.GetType()))
                {
                    // This utility class is used to convert value that come from XAML, so we should use the invariant culture.
                    returnValue = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
                }
            } catch (Exception e)
            {
                if (!ShouldEatException(e))
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        ///     Shoulds the eat exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        private static bool ShouldEatException(Exception e)
        {
            bool shouldEat = false;

            if (e.InnerException != null)
            {
                shouldEat |= ShouldEatException(e.InnerException);
            }

            shouldEat |= e is FormatException;
            return shouldEat;
        }

        /// <summary>
        ///     Gets the type converter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">type</exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification =
                "Activator.CreateInstance could be calling user code which we don't want to bring us down.")]
        public static TypeConverter GetTypeConverter([NotNull] this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return TypeDescriptor.GetConverter(type);
        }
    }
}
