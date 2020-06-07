// -----------------------------------------------------------------------
// <copyright file="KeyTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Triggers
{
    #region

    using Anori.WPF.Behaviors.Input;
    using System;
    using System.Windows;
    using System.Windows.Input;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     A Trigger that is triggered by a keyboard event.  If the target Key and Modifiers are detected, it fires.
    /// </summary>
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
        ///     The target element
        /// </summary>
        public UIElement TargetElement { get; protected set; }

        /// <summary>
        ///     If true, the Trigger only listens to its trigger Source object, which means that element must have focus for the
        ///     trigger to fire.
        ///     If false, the Trigger listens at the root, so any unhandled KeyDown/Up messages will be caught.
        /// </summary>
        public bool ActiveOnFocus
        {
            get => (bool)this.GetValue(ActiveOnFocusProperty);
            set => this.SetValue(ActiveOnFocusProperty, value);
        }

        /// <summary>
        ///     Determines whether or not to listen to the KeyDown or KeyUp event.
        /// </summary>
        public KeyTriggerFiredOn FiredOn
        {
            get => (KeyTriggerFiredOn)this.GetValue(FiredOnProperty);
            set => this.SetValue(FiredOnProperty, value);
        }

        /// <summary>The key that must be pressed for the trigger to fire.</summary>
        public Key Key
        {
            get => (Key)this.GetValue(KeyProperty);
            set => this.SetValue(KeyProperty, value);
        }

        /// <summary>
        ///     The modifiers that must be active for the trigger to fire (the default is no modifiers pressed).
        /// </summary>
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

        /// <inheritdoc />
        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
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
            this.TargetElement = !this.ActiveOnFocus ? Window.GetWindow(this.Source) : this.Source;
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
        /// <returns></returns>
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
