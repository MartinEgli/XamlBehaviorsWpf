// -----------------------------------------------------------------------
// <copyright file="ItemClickBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Item Click Behavior
    /// </summary>
    /// <seealso cref="UiElementClickBehavior{ListBox}" />
    /// <inheritdoc />
    public class ItemClickBehavior : UiElementClickBehavior<ListBox>
    {
        /// <summary>
        ///     The clicked index property
        /// </summary>
        public static readonly DependencyProperty ClickedIndexProperty = DependencyProperty.Register(
            nameof(ClickedIndex),
            typeof(int),
            typeof(ItemClickBehavior),
            new FrameworkPropertyMetadata(-1));

        /// <summary>
        ///     Gets or sets the index of the clicked.
        /// </summary>
        /// <value>
        ///     The index of the clicked.
        /// </value>
        public int ClickedIndex
        {
            get => (int)this.GetValue(ClickedIndexProperty);
            set => this.SetValue(ClickedIndexProperty, value);
        }

        /// <summary>
        ///     Items the clicked.
        /// </summary>
        /// <inheritdoc />
        protected override void OnClicked() => this.ClickedIndex = this.AssociatedObject.SelectedIndex;
    }
}