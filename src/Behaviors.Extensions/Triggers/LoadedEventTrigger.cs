// -----------------------------------------------------------------------
// <copyright file="LoadedEventTrigger.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Triggers
{
    using System;
    using System.Windows;

    using Bfa.Common.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;

    /// <inheritdoc />
    /// <summary>
    ///     Loaded Event Trigger Class
    /// </summary>
    /// <seealso cref="!:System.Windows.Interactivity.EventTriggerBase{System.Windows.UIElement}" />
    public class LoadedEventTrigger : EventTriggerBase<UIElement>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns>The event name.</returns>
        protected override string GetEventName() => nameof(Window.Loaded);

        /// <inheritdoc />
        /// <summary>
        ///     Called when the event associated with this EventTriggerBase is fired. By default, this will invoke all actions on
        ///     the trigger.
        /// </summary>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Override this to provide more granular control over when actions associated with this trigger will be invoked.
        /// </remarks>
        protected override void OnEvent(EventArgs eventArgs) =>
            this.Dispatch(trigger => trigger.InvokeActions(eventArgs));
    }
}