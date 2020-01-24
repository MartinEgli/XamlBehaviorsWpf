// -----------------------------------------------------------------------
// <copyright file="UiElementClickBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows;
    using System.Windows.Input;

    using Bfa.Common.WPF.Inputs;

    /// <summary>
    ///     Behavior on TreeViewItem click
    /// </summary>
    /// <typeparam name="TUiElement">The type of the UI element.</typeparam>
    /// <seealso cref="Microsoft.Xaml.Behaviors.Behavior{TUiElement}" />
    /// <inheritdoc />
    public abstract class UiElementClickBehavior<TUiElement> : Microsoft.Xaml.Behaviors.Behavior<TUiElement>
        where TUiElement : UIElement
    {
        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            var uiElement = this.AssociatedObject;
            if (uiElement == null)
            {
                return;
            }

            uiElement.MouseUp += this.OnMouseUp;
            uiElement.KeyUp += this.OnKeyUp;
            uiElement.TouchUp += this.OnTouchUp;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            var uiElement = this.AssociatedObject;
            if (uiElement != null)
            {
                uiElement.MouseUp -= this.OnMouseUp;
                uiElement.KeyUp -= this.OnKeyUp;
                uiElement.TouchUp -= this.OnTouchUp;
            }

            base.OnDetaching();
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight version.
        /// </summary>
        /// <returns>
        ///     The FrameworkElement to which this trigger
        ///     is attached.
        /// </returns>
        protected TUiElement GetAssociatedObject()
        {
            return this.AssociatedObject;
        }

        /// <summary>
        ///     Called when [clicked].
        /// </summary>
        protected abstract void OnClicked();

        /// <summary>
        ///     Called when [key up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void OnKeyUp(object sender, KeyEventArgs args)
        {
            if (args.IsClick())
            {
                this.OnClicked();
            }
        }

        /// <summary>
        ///     Called when [mouse up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (args.IsClick())
            {
                this.OnClicked();
            }
        }

        /// <summary>
        ///     Called when [touch up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TouchEventArgs" /> instance containing the event data.</param>
        private void OnTouchUp(object sender, TouchEventArgs args)
        {
            if (args.IsClick())
            {
                this.OnClicked();
            }
        }
    }
}