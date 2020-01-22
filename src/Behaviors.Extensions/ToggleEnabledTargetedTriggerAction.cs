﻿namespace Behaviors.Extensions
{
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// ToggleEnabledTargetedTriggerAction class
    /// </summary>
    /// <seealso cref="Microsoft.Xaml.Behaviors.TargetedTriggerAction{System.Windows.Controls.Button}" />
    public class ToggleEnabledTargetedTriggerAction : TargetedTriggerAction<Button>
    {
        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            if (this.Target != null && this.Target is UIElement c)
            {
                c.IsEnabled = !c.IsEnabled;
            }
        }
    }
}