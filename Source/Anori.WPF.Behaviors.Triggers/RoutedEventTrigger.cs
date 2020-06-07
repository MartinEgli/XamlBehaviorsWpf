// -----------------------------------------------------------------------
// <copyright file="RoutedEventTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Triggers
{
    #region

    using Anori.WPF.Behaviors;
    using System;
    using System.Windows;

    #endregion

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="!:System.Windows.Interactivity.EventTriggerBase{System.Windows.DependencyObject}" />
    public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        /// <summary>
        ///     The routed event property
        /// </summary>
        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.Register(
            nameof(RoutedEvent),
            typeof(RoutedEvent),
            typeof(RoutedEventTrigger),
            new PropertyMetadata(default(RoutedEvent)));

        /// <summary>
        ///     The associated element
        /// </summary>
        private FrameworkElement associatedElement;

        /// <summary>
        ///     The routed event handler
        /// </summary>
        private RoutedEventHandler routedEventHandler;

        /// <summary>
        ///     Gets or sets the routed event.
        /// </summary>
        /// <value>
        ///     The routed event.
        /// </value>
        public RoutedEvent RoutedEvent
        {
            get => (RoutedEvent)this.GetValue(RoutedEventProperty);
            set => this.SetValue(RoutedEventProperty, value);
        }

        /// <summary>
        ///     Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        /// <exception cref="ArgumentException">Routed Event trigger can only be associated to framework elements</exception>
        /// <exception cref="NullReferenceException">RoutedEvent is not defined!</exception>
        /// <exception cref="T:System.ArgumentException">Routed Event trigger can only be associated to framework elements</exception>
        /// <inheritdoc />
        protected override void OnAttached()
        {
            if (this.RoutedEvent == null)
            {
                throw new ArgumentException("Routed Event trigger: The RoutedEvent is not defined!");
            }

            if (this.AssociatedObject is IAttachedObject attachedObject)
            {
                this.associatedElement = attachedObject.AssociatedObject as FrameworkElement;
            } else
            {
                this.associatedElement = this.AssociatedObject as FrameworkElement;
            }

            if (this.associatedElement == null)
            {
                throw new ArgumentException(
                    "Routed Event trigger: Trigger can only be associated to framework elements");
            }

            this.routedEventHandler = this.OnRoutedEvent;
            this.associatedElement.AddHandler(this.RoutedEvent, this.routedEventHandler);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            RoutedEventHandler eventHandler = this.routedEventHandler;
            if (eventHandler != null)
            {
                this.associatedElement?.RemoveHandler(this.RoutedEvent, eventHandler);
                this.routedEventHandler = null;
            }

            this.associatedElement = null;
            base.OnDetaching();
        }

        /// <summary>
        ///     Called when [routed event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnRoutedEvent(object sender, RoutedEventArgs args) => this.OnEvent(args);

        /// <inheritdoc />
        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
        protected override string GetEventName() => this.RoutedEvent.Name;
    }
}
