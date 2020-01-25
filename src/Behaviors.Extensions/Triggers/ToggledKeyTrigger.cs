// -----------------------------------------------------------------------
// <copyright file="ToggledKeyTrigger.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Triggers
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Anori.WPF.Extensions;

    using Microsoft.Xaml.Behaviors.Input;

    /// <inheritdoc />
    /// <summary>
    ///     Toggled Key Trigger
    /// </summary>
    /// <seealso cref="T:Bfa.Infinity.Base.WPF.Triggers.KeyTrigger" />
    public class ToggledKeyTrigger : KeyTrigger
    {
        /// <summary>
        ///     The toggled property
        /// </summary>
        public static readonly DependencyProperty ToggledProperty = DependencyProperty.Register(
            nameof(Toggled),
            typeof(bool),
            typeof(ToggledKeyTrigger),
            new PropertyMetadata(true));

        /// <summary>
        ///     The window
        /// </summary>
        private Window window;

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ToggledKeyTrigger" /> is toggled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if toggled; otherwise, <c>false</c>.
        /// </value>
        public bool Toggled
        {
            get => (bool)this.GetValue(ToggledProperty);
            set => this.SetValue(ToggledProperty, value);
        }

        /// <summary>
        ///     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <inheritdoc />
        protected override void OnDetaching()
        {
            if (this.TargetElement != null)
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.TargetElement.KeyDown -= this.OnKeyPress;
                }
                else
                {
                    this.TargetElement.KeyUp -= this.OnKeyPress;
                }
            }

            var w = this.window;
            if (w != null)
            {
                w.Activated -= this.OnWindowActivated;
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
            if (this.Source.GetRoot() is Window w)
            {
                // ReSharper disable once StyleCop.SA1126
                this.window = w;
                w.Activated += this.OnWindowActivated;
            }

            this.TargetElement = !this.ActiveOnFocus ? this.Source.GetRoot() : this.Source;

            if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
            {
                this.TargetElement.KeyDown += this.OnKeyPress;
            }
            else
            {
                this.TargetElement.KeyUp += this.OnKeyPress;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs" /> instance containing the event data.</param>
        protected override void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (this.Toggled)
            {
                if (e.Key != this.Key || (Keyboard.GetKeyStates(this.Key) & KeyStates.Toggled) != KeyStates.Toggled)
                {
                    return;
                }
            }
            else
            {
                if (e.Key != this.Key || (Keyboard.GetKeyStates(this.Key) & KeyStates.Toggled) == KeyStates.Toggled)
                {
                    return;
                }
            }

            this.InvokeActions(e);
        }

        /// <summary>
        ///     Windows the on activated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnWindowActivated(object sender, EventArgs e)
        {
            if (this.Toggled)
            {
                if ((Keyboard.GetKeyStates(this.Key) & KeyStates.Toggled) != KeyStates.Toggled)
                {
                    return;
                }
            }
            else
            {
                if ((Keyboard.GetKeyStates(this.Key) & KeyStates.Toggled) == KeyStates.Toggled)
                {
                    return;
                }
            }

            this.InvokeActions(e);
        }
    }
}