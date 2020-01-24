// -----------------------------------------------------------------------
// <copyright file="SetFocusTriggerAction.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.TriggerActions
{
    using System;
    using System.Windows;

    using Bfa.Common.WPF.Extensions;

    using JetBrains.Annotations;

    using Microsoft.Xaml.Behaviors;

    /// <inheritdoc />
    /// <summary>
    ///     IsDisposed UIElement to focus Trigger Action
    /// </summary>
    /// <seealso cref="!:System.Windows.Interactivity.TargetedTriggerAction{System.Windows.UIElement}" />
    public class SetFocusTriggerAction : TargetedTriggerAction<UIElement>
    {
        /// <summary>
        ///     Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke([NotNull] object parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var target = this.Target;
            if (target == null)
            {
                return;
            }

            if (parameter is RoutedEventArgs ev)
            {
                ev.Handled = true;
            }

            if (target.IsFocused)
            {
                return;
            }

            if (!target.IsEnabled)
            {
                target.IsEnabledChanged += this.TargetOnIsEnabledChanged;
                if (!target.IsEnabled)
                {
                    return;
                }

                target.Dispatch(element => element.Focus());
                target.IsEnabledChanged -= this.TargetOnIsEnabledChanged;
            }
            else
            {
                target.Dispatch(element => element.Focus());
            }
        }

        /// <summary>
        ///     Targets the on is enabled changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private void TargetOnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var target = this.Target;
            if (target == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                target.Dispatch(element => element.Focus());
            }

            target.IsEnabledChanged -= this.TargetOnIsEnabledChanged;
        }
    }
}