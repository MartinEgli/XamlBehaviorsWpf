// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventHelpers.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Triggers
{
    using System;
    using System.Windows;

    using Anori.WPF.TriggerActions;

    /// <summary>
    ///     RoutedEventHelpers class.
    /// </summary>
    public static class DependencyPropertyChangedEventHelpers
    {
        /// <summary>
        ///     Solves the event arguments.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="commandParameterType">Type of the command parameter.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        /// <param name="associatedObject">The associated object.</param>
        /// <param name="property">The property.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="converterParameter">The converter parameter.</param>
        /// <returns>
        ///     the Argument.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Out of range.</exception>
        public static object SolveEventArguments(
            object parameter,
            DependencyPropertyChangedEventParameterPattern pattern,
            Type commandParameterType,
            DependencyPropertyChangedEventArgs e,
            FrameworkElement associatedObject,
            string property,
            IDependencyPropertyChangedEventArgsConverter converter,
            object converterParameter)
        {
            object args;
            switch (pattern)
            {
                case DependencyPropertyChangedEventParameterPattern.None:
                    args = parameter;
                    break;

                case DependencyPropertyChangedEventParameterPattern.EventArgs
                    when property != null && converter == null:
                    args = e.GetType().GetProperty(property)?.GetValue(e, null);
                    break;

                case DependencyPropertyChangedEventParameterPattern.EventArgs when converter == null:
                    args = e;
                    break;

                case DependencyPropertyChangedEventParameterPattern.EventArgs:
                    args = converter.Convert(e, commandParameterType, converterParameter, associatedObject);
                    break;

                case DependencyPropertyChangedEventParameterPattern.AssociatedObject when property != null:
                    args = associatedObject?.GetType().GetProperty(property)?.GetValue(associatedObject, null);
                    break;

                case DependencyPropertyChangedEventParameterPattern.AssociatedObject:
                    args = associatedObject;
                    break;

                case DependencyPropertyChangedEventParameterPattern.NewValue when property != null:
                    args = e.NewValue?.GetType().GetProperty(property)?.GetValue(e.NewValue, null);
                    break;

                case DependencyPropertyChangedEventParameterPattern.NewValue:
                    args = e.NewValue;
                    break;

                case DependencyPropertyChangedEventParameterPattern.OldValue when property != null:
                    args = e.OldValue?.GetType().GetProperty(property)?.GetValue(e.OldValue, null);
                    break;

                case DependencyPropertyChangedEventParameterPattern.OldValue:
                    args = e.OldValue;
                    break;

                case DependencyPropertyChangedEventParameterPattern.Property when property != null:
                    args = e.Property?.GetType().GetProperty(property)?.GetValue(e.OldValue, null);
                    break;

                case DependencyPropertyChangedEventParameterPattern.Property:
                    args = e.Property;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(pattern.ToString());
            }

            return args;
        }
    }
}