// -----------------------------------------------------------------------
// <copyright file="EventObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     EventObserver is designed to help manage event handlers by detatching when disposed. Creating this object will also
    ///     attach in the constructor.
    /// </summary>
    public sealed class EventObserver : IDisposable
    {
        /// <summary>
        ///     The event information
        /// </summary>
        private readonly EventInfo eventInfo;

        /// <summary>
        ///     The handler
        /// </summary>
        private readonly Delegate handler;

        /// <summary>
        ///     The target
        /// </summary>
        private readonly object target;

        /// <summary>
        ///     Creates an instance of EventObserver and attaches to the supplied event on the supplied target. Call dispose to
        ///     detach.
        /// </summary>
        /// <param name="eventInfo">The event to attach and detach from.</param>
        /// <param name="target">The target object the event is defined on. Null if the method is static.</param>
        /// <param name="handler">The delegate to attach to the event.</param>
        public EventObserver(EventInfo eventInfo, object target, Delegate handler)
        {
            if (eventInfo == null)
            {
                throw new ArgumentNullException(nameof(eventInfo));
            }

            this.eventInfo = eventInfo;
            this.target = target;
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
            this.eventInfo.AddEventHandler(this.target, handler);
        }

        /// <summary>
        ///     Detaches the handler from the event.
        /// </summary>
        public void Dispose()
        {
            this.eventInfo.RemoveEventHandler(this.target, this.handler);
        }
    }
}
