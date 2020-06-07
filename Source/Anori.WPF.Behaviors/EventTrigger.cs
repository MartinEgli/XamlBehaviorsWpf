// -----------------------------------------------------------------------
// <copyright file="EventTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    ///     A trigger that listens for a specified event on its source and fires when that event is fired.
    /// </summary>
    /// <seealso cref="EventTriggerBase{object}" />
    public class EventTrigger : EventTriggerBase<object>
    {
        /// <summary>
        ///     The event name property
        /// </summary>
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(
            "EventName",
            typeof(string),
            typeof(EventTrigger),
            new FrameworkPropertyMetadata("Loaded", OnEventNameChanged));

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventTrigger" /> class.
        /// </summary>
        public EventTrigger()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventTrigger" /> class.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public EventTrigger(string eventName)
        {
            this.EventName = eventName;
        }

        /// <summary>
        ///     Gets or sets the name of the event to listen for. This is a dependency property.
        /// </summary>
        /// <value>
        ///     The name of the event.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string EventName
        {
            get { return (string)this.GetValue(EventNameProperty); }
            set { this.SetValue(EventNameProperty, value); }
        }

        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
        protected override string GetEventName()
        {
            return this.EventName;
        }

        /// <summary>
        ///     Called when [event name changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((EventTrigger)sender).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
        }
    }
}
