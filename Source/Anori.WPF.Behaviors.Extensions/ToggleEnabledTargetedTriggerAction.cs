// -----------------------------------------------------------------------
// <copyright file="ToggleEnabledTargetedTriggerAction.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Extensions
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     ToggleEnabledTargetedTriggerAction class
    /// </summary>
    /// <seealso cref="TargetedTriggerAction{Button}" />
    public class ToggleEnabledTargetedTriggerAction : TargetedTriggerAction<Button>
    {
        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            if (this.Target is UIElement c)
            {
                c.IsEnabled = !c.IsEnabled;
            }
        }
    }
}
