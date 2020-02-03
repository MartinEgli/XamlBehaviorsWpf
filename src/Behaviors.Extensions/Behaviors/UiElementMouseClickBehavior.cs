// -----------------------------------------------------------------------
// <copyright file="UiElementMouseClickBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Windows;
    using System.Windows.Input;

    using Anori.WPF.Inputs;

    /// <summary>
    ///     Behavior on TreeViewItem click
    /// </summary>
    /// <typeparam name="TUiElement">The type of the UI element.</typeparam>
    /// <seealso cref="Microsoft.Xaml.Behaviors.Behavior{TUiElement}" />
    public abstract class UiElementMouseClickBehavior<TUiElement> : Microsoft.Xaml.Behaviors.Behavior<TUiElement>
        where TUiElement : UIElement
    {
        /// <summary>
        ///     The click count property
        /// </summary>
        public static readonly DependencyProperty ClickCountProperty = DependencyProperty.Register(
            nameof(ClickCount),
            typeof(int),
            typeof(UiElementMouseClickBehavior<TUiElement>),
            new PropertyMetadata(1));

        /// <summary>
        ///     The modifier keys property
        /// </summary>
        public static readonly DependencyProperty ModifierKeysProperty = DependencyProperty.Register(
            nameof(ModifierKeys),
            typeof(ModifierKeys),
            typeof(UiElementMouseClickBehavior<TUiElement>),
            new PropertyMetadata(ModifierKeys.None));

        /// <summary>
        ///     The mouse button property
        /// </summary>
        public static readonly DependencyProperty MouseButtonProperty = DependencyProperty.Register(
            nameof(MouseButton),
            typeof(MouseButton),
            typeof(UiElementMouseClickBehavior<TUiElement>),
            new PropertyMetadata(MouseButton.Left));

        /// <summary>
        ///     Gets or sets the modifier keys.
        /// </summary>
        /// <value>
        ///     The modifier keys.
        /// </value>
        public ModifierKeys ModifierKeys
        {
            get => (ModifierKeys)this.GetValue(ModifierKeysProperty);
            set => this.SetValue(ModifierKeysProperty, value);
        }

        /// <summary>
        ///     Gets or sets the mouse button.
        /// </summary>
        /// <value>
        ///     The mouse button.
        /// </value>
        public MouseButton MouseButton
        {
            get => (MouseButton)this.GetValue(MouseButtonProperty);
            set => this.SetValue(MouseButtonProperty, value);
        }

        /// <summary>
        ///     Gets or sets the click count.
        /// </summary>
        /// <value>
        ///     The click count.
        /// </value>
        public int ClickCount
        {
            get => (int)this.GetValue(ClickCountProperty);
            set => this.SetValue(ClickCountProperty, value);
        }

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
        protected TUiElement GetAssociatedObject() => this.AssociatedObject;

        /// <summary>
        ///     Called when [clicked].
        /// </summary>
        protected abstract void OnClicked();

        /// <summary>
        ///     Called when [mouse up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (args.IsClick(this.MouseButton, this.ModifierKeys, this.ClickCount))
            {
                this.OnClicked();
            }
        }
    }
}