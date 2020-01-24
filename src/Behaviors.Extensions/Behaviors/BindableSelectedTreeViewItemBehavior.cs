// -----------------------------------------------------------------------
// <copyright file="BindableSelectedTreeViewItemBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1119 // StatementMustNotUseUnnecessaryParenthesis

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Bindable Selected Tree View Item Behavior
    /// </summary>
    public class BindableSelectedTreeViewItemBehavior : Microsoft.Xaml.Behaviors.Behavior<TreeView>
    {
        /// <summary>
        ///     The selected item property
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(BindableSelectedTreeViewItemBehavior),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedItemChanged));

        /// <summary>
        ///     Gets or sets the selected item.
        /// </summary>
        /// <value>
        ///     The selected item.
        /// </value>
        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        ///     Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null)
            {
                return;
            }

            this.AssociatedObject.SelectedItemChanged += this.OnTreeViewSelectedItemChanged;
        }

        /// <summary>
        ///     Called when [detaching].
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject == null)
            {
                return;
            }

            this.AssociatedObject.SelectedItemChanged -= this.OnTreeViewSelectedItemChanged;
        }

        /// <summary>
        ///     Called when [selected item changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is BindableSelectedTreeViewItemBehavior behavior))
            {
                return;
            }

            var generator = behavior.AssociatedObject?.ItemContainerGenerator;
            if (generator == null)
            {
                return;
            }

            if (generator.ContainerFromItem(e.NewValue) is TreeViewItem item)
            {
                item.SetValue(TreeViewItem.IsSelectedProperty, true);
            }
        }

        /// <summary>
        /// Called when [TreeView selected item changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedPropertyChangedEventArgs">The <see cref="RoutedPropertyChangedEventArgs{Object}"/> instance containing the event data.</param>
        private void OnTreeViewSelectedItemChanged(
            object sender,
            RoutedPropertyChangedEventArgs<object> routedPropertyChangedEventArgs) =>
            this.SelectedItem = routedPropertyChangedEventArgs.NewValue;
    }
}

#pragma warning restore SA1119 // StatementMustNotUseUnnecessaryParenthesis