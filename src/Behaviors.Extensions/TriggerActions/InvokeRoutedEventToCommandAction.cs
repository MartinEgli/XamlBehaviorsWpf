// -----------------------------------------------------------------------
// <copyright file="InvokeRoutedEventToCommandAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.TriggerActions
{
    using System.Windows;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    ///     InvokeRoutedEventToCommandAction class.
    /// </summary>
    /// <typeparam name="T">The Type</typeparam>
    /// <seealso cref="TriggerAction{T}" />
    public class InvokeRoutedEventToCommandAction<T> : TriggerAction<T>
        where T : DependencyObject
    {
        /// <summary>
        ///     Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException">Not implemented.</exception>
        protected override void Invoke(object parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}