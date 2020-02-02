// -----------------------------------------------------------------------
// <copyright file="SelectOnFocus.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Behaviors.Extensions.GuiTests
{
    using System.Windows.Controls;
    using System.Windows.Input;

    using Anori.Xaml.Behaviors;

    public class SelectOnFocus : Behavior<ListViewItem>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.PreviewGotKeyboardFocus += this.AssociatedObjectOnPreviewGotKeyboardFocus;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewGotKeyboardFocus -= this.AssociatedObjectOnPreviewGotKeyboardFocus;
            base.OnDetaching();
        }

        private void AssociatedObjectOnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }
    }
}
