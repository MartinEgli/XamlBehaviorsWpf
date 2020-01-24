// -----------------------------------------------------------------------
// <copyright file="KeyTrigger.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Triggers
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Extensions;

    using Microsoft.Xaml.Behaviors;
    using Microsoft.Xaml.Behaviors.Input;

    /// <inheritdoc />
    /// <summary>
    ///     A Trigger that is triggered by a keyboard event.  If the target Key and Modifiers are detected, it fires.
    /// </summary>
    /// ReSharper disable MemberCanBeProtected.Global
    /// ReSharper disable MemberCanBePrivate.Global
    /// ReSharper disable StyleCop.SA1603
    public class KeyTrigger : EventTriggerBase<UIElement>
    {
        /// <summary>
        ///     The active on focus property
        /// </summary>
        public static readonly DependencyProperty ActiveOnFocusProperty =
            DependencyProperty.Register(nameof(ActiveOnFocus), typeof(bool), typeof(KeyTrigger));

        /// <summary>
        ///     The fired on property
        /// </summary>
        public static readonly DependencyProperty FiredOnProperty = DependencyProperty.Register(
            nameof(FiredOn),
            typeof(KeyTriggerFiredOn),
            typeof(KeyTrigger));

        /// <summary>
        ///     The key property
        /// </summary>
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            nameof(Key),
            typeof(Key),
            typeof(KeyTrigger));

        /// <summary>
        ///     The modifiers property
        /// </summary>
        public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register(
            nameof(Modifiers),
            typeof(ModifierKeys),
            typeof(KeyTrigger));

        /// <summary>
        ///     The route property
        /// </summary>
        public static readonly DependencyProperty RouteProperty = DependencyProperty.Register(
            nameof(Route),
            typeof(KeyTriggerRoute),
            typeof(ConditionalKeyTrigger),
            new PropertyMetadata(KeyTriggerRoute.Bubbling));

        /// <summary>
        ///     Gets or sets a value indicating whether [active on focus].
        ///     If true, the Trigger only listens to its trigger Source object, which means that element must have focus for the
        ///     trigger to fire.
        ///     If false, the Trigger listens at the root, so any unhandled KeyDown/Up messages will be caught.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [active on focus]; otherwise, <c>false</c>.
        /// </value>
        public bool ActiveOnFocus
        {
            get => (bool)this.GetValue(ActiveOnFocusProperty);
            set => this.SetValue(ActiveOnFocusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the fired on.
        ///     Determines whether or not to listen to the KeyDown or KeyUp event.
        /// </summary>
        /// <value>
        ///     The fired on.
        /// </value>
        public KeyTriggerFiredOn FiredOn
        {
            get => (KeyTriggerFiredOn)this.GetValue(FiredOnProperty);
            set => this.SetValue(FiredOnProperty, value);
        }

        /// <summary>
        ///     Gets or sets the key.
        ///     The key that must be pressed for the trigger to fire.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        public Key Key
        {
            get => (Key)this.GetValue(KeyProperty);
            set => this.SetValue(KeyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the modifiers.
        ///     The modifiers that must be active for the trigger to fire (the default is no modifiers pressed).
        /// </summary>
        /// <value>
        ///     The modifiers.
        /// </value>
        public ModifierKeys Modifiers
        {
            get => (ModifierKeys)this.GetValue(ModifiersProperty);
            set => this.SetValue(ModifiersProperty, value);
        }

        /// <summary>
        ///     Gets or sets the route.
        /// </summary>
        /// <value>
        ///     The route.
        /// </value>
        public KeyTriggerRoute Route
        {
            get => (KeyTriggerRoute)this.GetValue(RouteProperty);
            set => this.SetValue(RouteProperty, value);
        }

        /// <summary>
        ///     Gets or sets the target element.
        /// </summary>
        /// <value>
        ///     The target element.
        /// </value>
        protected UIElement TargetElement { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns>The event name Loaded.</returns>
        protected override string GetEventName() => "Loaded";

        /// <inheritdoc />
        /// <summary>
        ///     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.TargetElement != null)
            {
                switch (this.Route)
                {
                    case KeyTriggerRoute.Bubbling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                        this.TargetElement.KeyDown -= this.OnKeyPress;
                        break;

                    case KeyTriggerRoute.Bubbling:
                        this.TargetElement.KeyUp -= this.OnKeyPress;
                        break;

                    case KeyTriggerRoute.Tunneling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                        this.TargetElement.PreviewKeyDown -= this.OnKeyPress;
                        break;

                    case KeyTriggerRoute.Tunneling:
                        this.TargetElement.PreviewKeyUp -= this.OnKeyPress;
                        break;
                }
            }

            base.OnDetaching();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Called when the event associated with this EventTriggerBase is fired. By default, this will invoke all actions on
        ///     the trigger.
        /// </summary>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Override this to provide more granular control over when actions associated with this trigger will be invoked.
        /// </remarks>
        protected override void OnEvent(EventArgs eventArgs)
        {
            this.TargetElement = !this.ActiveOnFocus ? this.Source.GetRoot() : this.Source;
            switch (this.Route)
            {
                case KeyTriggerRoute.Bubbling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                    this.TargetElement.KeyDown += this.OnKeyPress;
                    break;

                case KeyTriggerRoute.Bubbling:
                    this.TargetElement.KeyUp += this.OnKeyPress;
                    break;

                case KeyTriggerRoute.Tunneling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                    this.TargetElement.PreviewKeyDown += this.OnKeyPress;
                    break;

                case KeyTriggerRoute.Tunneling:
                    this.TargetElement.PreviewKeyUp += this.OnKeyPress;
                    break;
            }
        }

        /// <summary>
        ///     Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        protected virtual void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != this.Key || Keyboard.Modifiers != GetActualModifiers(e.Key, this.Modifiers))
            {
                return;
            }

            this.InvokeActions(e);
        }

        /// <summary>
        ///     Gets the actual modifiers.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The modifiers.</param>
        /// <returns>The modifier key.</returns>
        private static ModifierKeys GetActualModifiers(Key key, ModifierKeys modifiers)
        {
            switch (key)
            {
                case Key.LeftShift:
                case Key.RightShift:
                    modifiers |= ModifierKeys.Shift;
                    break;

                case Key.LeftCtrl:
                case Key.RightCtrl:
                    modifiers |= ModifierKeys.Control;
                    break;

                case Key.LeftAlt:
                case Key.RightAlt:
                case Key.System:
                    modifiers |= ModifierKeys.Alt;
                    break;
            }

            return modifiers;
        }
    }
}