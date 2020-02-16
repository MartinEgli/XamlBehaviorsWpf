// -----------------------------------------------------------------------
// <copyright file="IEventArgsConverter.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Converters
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     Event Args Converter Interface
    /// </summary>
    public interface IEventArgsConverter
    {
        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="associatedObject">The associated object.</param>
        /// <returns>The converted object.</returns>
        object Convert(EventArgs eventArgs, Type targetType, object parameter, object associatedObject);
    }
}
