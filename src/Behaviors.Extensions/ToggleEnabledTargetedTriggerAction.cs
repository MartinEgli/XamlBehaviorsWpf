// -----------------------------------------------------------------------
// <copyright file="ToggleEnabledTargetedTriggerAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Behaviors.Extensions
{
    using System;
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Xaml.Behaviors;

    public class ToggleEnabledTargetedTriggerAction : TargetedTriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            if (this.Target != null && this.Target is UIElement c)
            {
                c.IsEnabled = !c.IsEnabled;
            }
        }
    }

    public class SetTimeTargetedTriggerAction : TargetedTriggerAction<TextBox>
    {
        protected override void Invoke(object parameter)
        {
            if (this.Target != null)
            {
                Target.Text = DateTime.Now.ToLongTimeString();
            }
        }
    }
}
