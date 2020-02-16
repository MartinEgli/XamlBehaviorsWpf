// -----------------------------------------------------------------------
// <copyright file="ConditionalKeyTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Triggers
{
    #region

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using Anori.WPF.Behaviors.Input;

    using ConditionCollection = Anori.WPF.Conditions.ConditionCollection;
    using KeyTrigger = Anori.WPF.Behaviors.Triggers.KeyTrigger;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     Conditional Key Trigger
    /// </summary>
    /// <seealso cref="T:Microsoft.Expression.Interactivity.Input.KeyTrigger" />
    public class ConditionalKeyTrigger : Triggers.KeyTrigger
    {
        /// <summary>
        ///     The condition property
        /// </summary>
        public static readonly DependencyProperty ConditionProperty = DependencyProperty.Register(
            nameof(Condition),
            typeof(bool),
            typeof(ConditionalKeyTrigger),
            new PropertyMetadata(true));

        /// <summary>
        ///     The conditions property key
        /// </summary>
        public static readonly DependencyPropertyKey ConditionsPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Conditions),
            typeof(ConditionCollection),
            typeof(ConditionalKeyTrigger),
            new FrameworkPropertyMetadata());

        /// <summary>
        ///     The conditions property
        /// </summary>
        public static readonly DependencyProperty ConditionsProperty = ConditionsPropertyKey.DependencyProperty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalKeyTrigger" /> class.
        /// </summary>
        public ConditionalKeyTrigger()
        {
            var conditions = new ConditionCollection();
            this.SetValue(ConditionsPropertyKey, conditions);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ConditionalKeyTrigger" /> is condition.
        /// </summary>
        /// <value>
        ///     <c>true</c> if condition; otherwise, <c>false</c>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool Condition
        {
            get => (bool)this.GetValue(ConditionProperty);
            set => this.SetValue(ConditionProperty, value);
        }

        /// <summary>
        ///     Gets the conditions.
        /// </summary>
        /// <value>
        ///     The conditions.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ConditionCollection Conditions => (ConditionCollection)this.GetValue(ConditionsProperty);

        /// <inheritdoc />
        /// <summary>
        ///     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.TargetElement == null)
            {
                return;
            }

            switch (this.Route)
            {
                case KeyTriggerRoute.Bubbling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                    this.TargetElement.KeyDown -= this.OnKeyPressWithConditions;
                    this.TargetElement.KeyDown -= this.OnKeyPressWithCondition;
                    break;

                case KeyTriggerRoute.Bubbling:
                    this.TargetElement.KeyUp -= this.OnKeyPressWithConditions;
                    this.TargetElement.KeyUp -= this.OnKeyPressWithCondition;
                    break;

                case KeyTriggerRoute.Tunneling when this.FiredOn == KeyTriggerFiredOn.KeyDown:
                    this.TargetElement.PreviewKeyDown -= this.OnKeyPressWithConditions;
                    this.TargetElement.PreviewKeyDown -= this.OnKeyPressWithCondition;
                    break;

                case KeyTriggerRoute.Tunneling:
                    this.TargetElement.PreviewKeyUp -= this.OnKeyPressWithConditions;
                    this.TargetElement.PreviewKeyUp -= this.OnKeyPressWithCondition;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(this.Route.ToString());
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
                case KeyTriggerRoute.Bubbling:
                    this.BubblingEvent();
                    break;

                case KeyTriggerRoute.Tunneling:
                    this.TunnelingEvent();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(this.Route.ToString());
            }
        }

        /// <summary>
        ///     Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        protected void OnKeyPressWithCondition(object sender, KeyEventArgs e)
        {
            if (!this.Condition)
            {
                return;
            }

            base.OnKeyPress(sender, e);
        }

        /// <summary>
        ///     Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        protected void OnKeyPressWithConditions(object sender, KeyEventArgs e)
        {
            if (!this.Conditions.AreAll())
            {
                return;
            }

            base.OnKeyPress(sender, e);
        }

        /// <summary>
        ///     Register the Bubbling event.
        /// </summary>
        private void BubblingEvent()
        {
            if (this.Conditions != null && this.Conditions.Any())
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.TargetElement.KeyDown += this.OnKeyPressWithConditions;
                } else
                {
                    this.TargetElement.KeyUp += this.OnKeyPressWithConditions;
                }
            } else
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.TargetElement.KeyDown += this.OnKeyPressWithCondition;
                } else
                {
                    this.TargetElement.KeyUp += this.OnKeyPressWithCondition;
                }
            }
        }

        /// <summary>
        ///     Register the tunneling event.
        /// </summary>
        private void TunnelingEvent()
        {
            if (this.Conditions != null && this.Conditions.Any())
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.TargetElement.PreviewKeyDown += this.OnKeyPressWithConditions;
                } else
                {
                    this.TargetElement.PreviewKeyUp += this.OnKeyPressWithConditions;
                }
            } else
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.TargetElement.PreviewKeyDown += this.OnKeyPressWithCondition;
                } else
                {
                    this.TargetElement.PreviewKeyUp += this.OnKeyPressWithCondition;
                }
            }
        }
    }
}
