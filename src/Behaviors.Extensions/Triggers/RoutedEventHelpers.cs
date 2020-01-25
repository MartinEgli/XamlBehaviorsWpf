// -----------------------------------------------------------------------
// <copyright file="RoutedEventHelpers.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Triggers
{
    using System;
    using System.Windows;

    using Anori.WPF.Behaviors;

    /// <summary>
    ///     RoutedEventHelpers class.
    /// </summary>
    public static class RoutedEventHelpers
    {
        /// <summary>
        ///     Solves the event arguments.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="commandParameterType">Type of the command parameter.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        /// <param name="associatedObject">The associated object.</param>
        /// <param name="property">The property.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="converterParameter">The converter parameter.</param>
        /// <returns>the Argument.</returns>
        public static object SolveEventArguments(
            object parameter,
            ParameterPattern pattern,
            Type commandParameterType,
            RoutedEventArgs e,
            FrameworkElement associatedObject,
            string property,
            IRoutedEventArgsConverter converter,
            object converterParameter)
        {
            object args;
            switch (pattern)
            {
                case Triggers.ParameterPattern.None:
                    args = parameter;
                    break;

                case Triggers.ParameterPattern.EventArgs when property != null && converter == null:

                    args = e?.GetType().GetProperty(property)?.GetValue(e, null);
                    break;

                case Triggers.ParameterPattern.EventArgs when converter == null:
                    args = e;
                    break;

                case Triggers.ParameterPattern.EventArgs:
                    args = converter.Convert(e, commandParameterType, converterParameter, associatedObject);
                    break;

                case Triggers.ParameterPattern.AssociatedObject when property != null:

                    args = associatedObject?.GetType().GetProperty(property)?.GetValue(associatedObject, null);
                    break;

                case Triggers.ParameterPattern.AssociatedObject:
                    args = associatedObject;
                    break;

                case Triggers.ParameterPattern.Source when property != null:

                    args = e.Source?.GetType().GetProperty(property)?.GetValue(e.Source, null);
                    break;

                case Triggers.ParameterPattern.Source:
                    args = e.Source;
                    break;

                case Triggers.ParameterPattern.OriginalSource when property != null:

                    args = e.OriginalSource?.GetType().GetProperty(property)?.GetValue(e.OriginalSource, null);
                    break;

                case Triggers.ParameterPattern.OriginalSource:
                    args = e.OriginalSource;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(pattern.ToString());
            }

            return args;
        }
    }
}