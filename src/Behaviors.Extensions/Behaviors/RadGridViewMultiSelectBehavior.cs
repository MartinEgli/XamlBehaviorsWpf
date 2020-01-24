// -----------------------------------------------------------------------
// <copyright file="RadGridViewMultiSelectBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.Windows;

    using Telerik.Windows.Controls;

    /// <summary>
    /// Behavior for supporting two-way binding of a RadGridView's SelectedItems collection.
    /// </summary>
    /// <remarks>
    /// Adapted from:
    /// https://www.telerik.com/forums/binding-multi-selected-items-in-mvvm.
    /// </remarks>
    public class RadGridViewMultiSelectBehavior : Microsoft.Xaml.Behaviors.Behavior<RadGridView>
    {
        /// <summary>
        /// DependencyProperty that represents the SelectedItems property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            nameof(SelectedItems),
            typeof(INotifyCollectionChanged),
            typeof(RadGridViewMultiSelectBehavior),
            new PropertyMetadata(OnSelectedItemsPropertyChanged));

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        public INotifyCollectionChanged SelectedItems
        {
            get => (INotifyCollectionChanged)this.GetValue(SelectedItemsProperty);
            set => this.SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Called when the behavior is attached.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject.SelectedItems != null)
            {
                this.AssociatedObject.SelectedItems.CollectionChanged += this.OnGridSelectedItemsCollectionChanged;
            }
        }

        /// <summary>
        /// Called when the SelectedItems property has changed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="args">The event args</param>
        private static void OnSelectedItemsPropertyChanged(
            DependencyObject target,
            DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged +=
                    ((RadGridViewMultiSelectBehavior)target).OnContextSelectedItemsCollectionChanged;
            }
        }

        /// <summary>
        /// Transfers objects from source list to target list.
        /// </summary>
        /// <param name="source">The source list.</param>
        /// <param name="target">The target list.</param>
        private static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
            {
                return;
            }

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }

        /// <summary>
        /// Called when the context SelectedItems collection has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnContextSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UnsubscribeFromEvents();

            Transfer(this.SelectedItems as IList, this.AssociatedObject.SelectedItems);

            this.SubscribeToEvents();
        }

        /// <summary>
        /// Called when the grid SelectedItems collection has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnGridSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UnsubscribeFromEvents();

            Transfer(this.AssociatedObject.SelectedItems, this.SelectedItems as IList);

            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (this.AssociatedObject.SelectedItems != null)
            {
                this.AssociatedObject.SelectedItems.CollectionChanged += this.OnGridSelectedItemsCollectionChanged;
            }

            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged += this.OnContextSelectedItemsCollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (this.AssociatedObject.SelectedItems != null)
            {
                this.AssociatedObject.SelectedItems.CollectionChanged -= this.OnGridSelectedItemsCollectionChanged;
            }

            if (this.SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged -= this.OnContextSelectedItemsCollectionChanged;
            }
        }
    }
}
