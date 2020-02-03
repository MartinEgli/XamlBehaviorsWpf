// -----------------------------------------------------------------------
// <copyright file="SelectAllTextTargetedTriggerAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.TriggerActions
{
    using System.Windows.Controls;

    using Anori.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    ///     SelectAllTextTargetedTriggerAction Class
    /// </summary>
    /// <seealso cref="TargetedTriggerAction{T}" />
    public class SelectAllTextTargetedTriggerAction : TargetedTriggerAction<TextBox>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            this.Target.Dispatch(
                t =>
                    {
                        t?.Focus();
                        t?.SelectAll();
                    });
        }
    }
}