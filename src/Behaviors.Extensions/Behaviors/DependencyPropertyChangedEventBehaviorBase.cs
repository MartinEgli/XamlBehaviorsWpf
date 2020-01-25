// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventBehaviorBase.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    ///     Blend Behavior to catch a bubbling RoutedEvents and raise a ICommand
    /// </summary>
    /// <seealso cref="Microsoft.Xaml.Behaviors.Behavior{FrameworkElement}" />
    /// <seealso cref="T:System.Windows.Interactivity.Behavior{FrameworkElement}" />
    public abstract class
        DependencyPropertyChangedEventBehaviorBase : Microsoft.Xaml.Behaviors.Behavior<FrameworkElement>
    {
        /// <summary>
        ///     The routed event property
        /// </summary>
        public static readonly DependencyProperty DependencyPropertyChangedEventProperty = DependencyProperty.Register(
            nameof(DependencyPropertyChangedEvent),
            typeof(string),
            typeof(DependencyPropertyChangedEventBehaviorBase),
            new PropertyMetadata(null, OnEventNameChanged));

        /// <summary>
        ///     The is enabled property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(
            "IsEnabled",
            typeof(bool),
            typeof(DependencyPropertyChangedEventBehaviorBase),
            new FrameworkPropertyMetadata(true));

        /// <summary>
        ///     The event handler method information
        /// </summary>
        private MethodInfo eventHandlerMethodInfo;

        /// <summary>
        ///     Gets or sets the routed event.
        /// </summary>
        /// <value>
        ///     The routed event.
        /// </value>
        public string DependencyPropertyChangedEvent
        {
            get => (string)this.GetValue(DependencyPropertyChangedEventProperty);
            set => this.SetValue(DependencyPropertyChangedEventProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this action will run when invoked. This is a dependency property.
        /// </summary>
        /// <value>
        ///     <c>True</c> if this action will be run when invoked; otherwise, <c>False</c>.
        /// </value>
        public bool IsEnabled
        {
            get => (bool)this.GetValue(IsEnabledProperty);
            set => this.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        ///     Gets or sets the dependency property changed event handler.
        /// </summary>
        /// <value>
        ///     The dependency property changed event handler.
        /// </value>
        protected DependencyPropertyChangedEventHandler DependencyPropertyChangedEventHandler { get; set; }

        /// <summary>
        ///     Attempts to invoke the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        internal void CallInvoke(DependencyPropertyChangedEventArgs parameter)
        {
            if (this.IsEnabled)
            {
                this.Invoke(parameter);
            }
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight version.
        /// </summary>
        /// <returns>
        ///     The FrameworkElement to which this trigger
        ///     is attached.
        /// </returns>
        protected FrameworkElement GetAssociatedObject() => this.AssociatedObject;

        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected virtual void Invoke(DependencyPropertyChangedEventArgs parameter)
        {
            this.DependencyPropertyChangedEventHandler?.Invoke(this, parameter);
        }

        /// <summary>
        ///     Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.RegisterEvent(this.AssociatedObject, this.DependencyPropertyChangedEvent);
        }

        /// <summary>
        ///     Called when [detaching].
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.UnregisterEvent(this.AssociatedObject, this.DependencyPropertyChangedEvent);
        }

        /// <summary>
        ///     Called when the event associated with this EventTriggerBase is fired. By default, this will invoke all actions on
        ///     the trigger.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        /// <remarks>Override this to provide more granular control over when actions associated with this trigger will be invoked.</remarks>
        protected virtual void OnEvent(DependencyPropertyChangedEventArgs eventArgs) => this.CallInvoke(eventArgs);

        /// <summary>
        ///     Determines whether [is valid event] [the specified event information].
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>
        ///     <c>true</c> if [is valid event] [the specified event information]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidEvent(EventInfo eventInfo)
        {
            var eventHandlerType = eventInfo.EventHandlerType;
            if (!typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
            {
                return false;
            }

            var invokeMethod = eventHandlerType.GetMethod("Invoke");
            if (invokeMethod == null)
            {
                return false;
            }

            var parameters = invokeMethod.GetParameters();
            return parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType)
                                          && typeof(DependencyPropertyChangedEventArgs).IsAssignableFrom(
                                              parameters[1].ParameterType);
        }

        /// <summary>
        ///     Called when [event name changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args) =>
            ((DependencyPropertyChangedEventBehaviorBase)sender).OnEventNameChanged(
                (string)args.OldValue,
                (string)args.NewValue);

        /// <summary>
        ///     Called when [event name changed].
        /// </summary>
        /// <param name="oldEventName">Old name of the event.</param>
        /// <param name="newEventName">New name of the event.</param>
        private void OnEventNameChanged(string oldEventName, string newEventName)
        {
            var associatedObject = this.GetAssociatedObject();
            if (associatedObject == null)
            {
                return;
            }

            this.UnregisterEvent(associatedObject, oldEventName);

            this.RegisterEvent(associatedObject, newEventName);
        }

        /// <summary>
        ///     Called when [event implementation].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        /// ReSharper disable once UnusedParameter.Local
        private void RaiseEvent(object sender, DependencyPropertyChangedEventArgs eventArgs) => this.OnEvent(eventArgs);

        /// <summary>
        ///     Registers the event.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <exception cref="ArgumentException">Could not find eventName on the Target.</exception>
        private void RegisterEvent(object obj, string eventName)
        {
            var targetType = obj.GetType();
            var eventInfo = targetType.GetEvent(eventName);
            if (eventInfo == null)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "Cannot Find Event Name {0} in type {1} Exception Message",
                        eventName,
                        obj.GetType().Name));
            }

            if (!IsValidEvent(eventInfo))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "Invalid Event Name {0} in type {1} Exception Message",
                        eventName,
                        obj.GetType().Name));
            }

            this.eventHandlerMethodInfo = typeof(DependencyPropertyChangedEventBehaviorBase).GetMethod(
                nameof(this.RaiseEvent),
                BindingFlags.NonPublic | BindingFlags.Instance);

            eventInfo.AddEventHandler(
                obj,
                Delegate.CreateDelegate(eventInfo.EventHandlerType, this, this.eventHandlerMethodInfo));
        }

        /// <summary>
        ///     Unregisters the event implementation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="eventName">Name of the event.</param>
        private void UnregisterEvent(object obj, string eventName)
        {
            var targetType = obj.GetType();

            if (this.eventHandlerMethodInfo == null)
            {
                return;
            }

            var eventInfo = targetType.GetEvent(eventName);
            eventInfo.RemoveEventHandler(
                obj,
                Delegate.CreateDelegate(eventInfo.EventHandlerType, this, this.eventHandlerMethodInfo));
            this.eventHandlerMethodInfo = null;
        }
    }
}