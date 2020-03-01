// -----------------------------------------------------------------------
// <copyright file="MoveFocusTriggerAction.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Extensions
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class MoveFocusTriggerAction : TargetedTriggerAction<Button>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveFocusTriggerAction"/> class.
        /// </summary>
        /// <param name="focusNavigationDirection">The focus navigation direction.</param>
        public MoveFocusTriggerAction(FocusNavigationDirection focusNavigationDirection)
        {
            this.FocusNavigationDirection = focusNavigationDirection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveFocusTriggerAction"/> class.
        /// </summary>
        public MoveFocusTriggerAction()
        {
            this.FocusNavigationDirection = FocusNavigationDirection.Next;
        }

        /// <summary>
        /// Gets or sets the focus navigation direction.
        /// </summary>
        /// <value>
        /// The focus navigation direction.
        /// </value>
        public FocusNavigationDirection FocusNavigationDirection { get; set; }

        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            if (this.Target is UIElement uiElement)
            {
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
