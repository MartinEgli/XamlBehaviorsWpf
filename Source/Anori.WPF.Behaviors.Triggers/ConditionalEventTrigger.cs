// -----------------------------------------------------------------------
// <copyright file="ConditionalEventTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Triggers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     Conditional Event Trigger Class
    /// </summary>
    /// <seealso cref="T:System.Windows.FrameworkContentElement" />
    [ContentProperty(nameof(Actions))]
    public class ConditionalEventTrigger : FrameworkContentElement
    {
        /// <summary>
        ///     The condition property
        /// </summary>
        public static readonly DependencyProperty ConditionProperty = DependencyProperty.Register(
            nameof(Condition),
            typeof(bool),
            typeof(ConditionalEventTrigger));

        /// <summary>
        ///     The triggers property
        /// </summary>
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached(
            nameof(Triggers),
            typeof(ConditionalEventTriggerCollection),
            typeof(ConditionalEventTrigger),
            new PropertyMetadata(null, OnTriggersChanged));

        /// <summary>
        ///     The trigger actions event
        /// </summary>
        private static readonly RoutedEvent TriggerActionsEvent = EventManager.RegisterRoutedEvent(
            "",
            RoutingStrategy.Direct,
            typeof(EventHandler),
            typeof(ConditionalEventTrigger));

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Anori.WPF.Behaviors.Triggers.ConditionalEventTrigger" /> class.
        /// </summary>
        public ConditionalEventTrigger()
        {
            this.Actions = new List<TriggerAction>();
        }

        /// <summary>
        ///     Gets or sets the routed event.
        /// </summary>
        /// <value>
        ///     The routed event.
        /// </value>
        public RoutedEvent RoutedEvent { get; set; }

        /// <summary>
        ///     Gets or sets the actions.
        /// </summary>
        /// <value>
        ///     The actions.
        /// </value>
        public List<TriggerAction> Actions { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ConditionalEventTrigger" /> is condition.
        /// </summary>
        /// <value>
        ///     <c>true</c> if condition; otherwise, <c>false</c>.
        /// </value>
        public bool Condition
        {
            get => (bool)this.GetValue(ConditionProperty);
            set => this.SetValue(ConditionProperty, value);
        }

        /// <summary>
        ///     Called when [triggers changed].
        ///     When "Triggers" is set, register handlers for each trigger in the list
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void OnTriggersChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is FrameworkElement element))
            {
                return;
            }

            if (!(dependencyPropertyChangedEventArgs.NewValue is List<ConditionalEventTrigger> triggers))
            {
                return;
            }

            foreach (ConditionalEventTrigger trigger in triggers)
            {
                element.AddHandler(
                    trigger.RoutedEvent,
                    new RoutedEventHandler((sender, e) => trigger.OnRoutedEvent(element)));
            }
        }

        /// <summary>
        ///     Gets the triggers.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ConditionalEventTriggerCollection GetTriggers(DependencyObject obj) =>
            (ConditionalEventTriggerCollection)obj.GetValue(TriggersProperty);

        /// <summary>
        ///     Sets the triggers.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetTriggers(DependencyObject obj, ConditionalEventTriggerCollection value) =>
            obj.SetValue(TriggersProperty, value);

        /// <summary>
        ///     Called when [routed event].
        ///     When an event fires, check the condition and if it is true fire the actions
        /// </summary>
        /// <param name="element">The element.</param>
        private void OnRoutedEvent(FrameworkElement element)
        {
            this.DataContext = element.DataContext; // Allow data binding to access element properties
            if (!this.Condition)
            {
                return;
            }

            // Construct an EventTrigger containing the actions, then trigger it
            EventTrigger trigger = new EventTrigger { RoutedEvent = TriggerActionsEvent };
            foreach (TriggerAction action in this.Actions)
            {
                trigger.Actions.Add(action);
            }

            element.Triggers.Add(trigger);
            try
            {
                element.RaiseEvent(new RoutedEventArgs(TriggerActionsEvent));
            } finally
            {
                element.Triggers.Remove(trigger);
            }
        }
    }
}
