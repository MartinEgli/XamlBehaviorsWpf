// -----------------------------------------------------------------------
// <copyright file="SelectAllTextTriggerAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.TriggerActions
{
    using System.Windows.Controls;

    using Bfa.Common.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    ///     SelectAllTextTriggerAction Class
    /// </summary>
    /// <seealso cref="TriggerAction{T}" />
    public class SelectAllTextTriggerAction : TriggerAction<TextBox>
    {
        /// <summary>
        ///     Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            this.AssociatedObject.Dispatch(
                ao =>
                    {
                        ao?.Focus();
                        ao?.SelectAll();
                    });
        }
    }
}