// -----------------------------------------------------------------------
// <copyright file="ConditionalEventTrigger.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Triggers
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    /// <inheritdoc />
    /// <summary>
    ///     Conditional Event Trigger Class
    /// </summary>
    /// <seealso cref="T:System.Windows.FrameworkContentElement" />
    /// ReSharper disable MemberCanBePrivate.Global
    /// ReSharper disable UnusedAutoPropertyAccessor.Global
    /// ReSharper disable StyleCop.SA1616
    /// ReSharper disable CollectionNeverUpdated.Global
    [ContentProperty("Actions")]
    public class ConditionalEventTrigger : FrameworkContentElement
    {
        /// <summary>
        ///     The condition property
        /// </summary>
        public static readonly DependencyProperty ConditionProperty = DependencyProperty.Register(
            "Condition",
            typeof(bool),
            typeof(ConditionalEventTrigger));

        /// <summary>
        ///     The triggers property
        /// </summary>
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached(
            "Triggers",
            typeof(ConditionalEventTriggerCollection),
            typeof(ConditionalEventTrigger),
            new PropertyMetadata { PropertyChangedCallback = OnPropertyChanged });

        /// <summary>
        ///     The trigger actions event
        /// </summary>
        private static readonly RoutedEvent TriggerActionsEvent = EventManager.RegisterRoutedEvent(
            string.Empty,
            RoutingStrategy.Direct,
            typeof(EventHandler),
            typeof(ConditionalEventTrigger));

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalEventTrigger" /> class.
        /// </summary>
        public ConditionalEventTrigger()
        {
            this.Actions = new List<TriggerAction>();
        }

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
        ///     Gets or sets the routed event.
        /// </summary>
        /// <value>
        ///     The routed event.
        /// </value>
        public RoutedEvent RoutedEvent { get; set; }

        /// <summary>
        ///     Gets the triggers.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The collection.</returns>
        public static ConditionalEventTriggerCollection GetTriggers(DependencyObject obj)
        {
            return (ConditionalEventTriggerCollection)obj.GetValue(TriggersProperty);
        }

        /// <summary>
        ///     Sets the triggers.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetTriggers(DependencyObject obj, ConditionalEventTriggerCollection value)
        {
            obj.SetValue(TriggersProperty, value);
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // When "Triggers" is set, register handlers for each trigger in the list
            var element = (FrameworkElement)obj;
            var triggers = (List<ConditionalEventTrigger>)e.NewValue;
            foreach (var trigger in triggers)
            {
                element.AddHandler(
                    trigger.RoutedEvent,
                    new RoutedEventHandler((obj2, e2) => trigger.OnRoutedEvent(element)));
            }
        }

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
            var dummyTrigger = new EventTrigger { RoutedEvent = TriggerActionsEvent };
            foreach (var action in this.Actions)
            {
                dummyTrigger.Actions.Add(action);
            }

            element.Triggers.Add(dummyTrigger);
            try
            {
                element.RaiseEvent(new RoutedEventArgs(TriggerActionsEvent));
            }
            finally
            {
                element.Triggers.Remove(dummyTrigger);
            }
        }
    }
}