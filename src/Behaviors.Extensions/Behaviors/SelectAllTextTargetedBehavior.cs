// -----------------------------------------------------------------------
// <copyright file="SelectAllTextTargetedBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    using Bfa.Common.WPF.Extensions;

    /// <summary>
    ///     SelectAllTextTargetedTriggerAction Class
    /// </summary>
    /// <seealso cref="DependencyPropertyChangedEventToTargetBehavior{TextBox}" />
    public class SelectAllTextTargetedBehavior : DependencyPropertyChangedEventToTargetBehavior<TextBox>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Invokes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(DependencyPropertyChangedEventArgs parameter)
        {
            if (!(bool)parameter.NewValue)
            {
                return;
            }

            var target = this.Target;
            Task.Factory.StartNew(() => Thread.Sleep(100))
                .ContinueWith(
                    r =>
                        {
                            target?.Dispatch(
                                t =>
                                    {
                                        t?.Focus();
                                        t?.SelectAll();
                                    });
                        },
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}