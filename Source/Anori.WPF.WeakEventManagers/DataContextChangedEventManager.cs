// -----------------------------------------------------------------------
// <copyright file="DataContextChangedEventManager.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.WeakEventManagers
{
    using System;
    using System.Windows;

    using JetBrains.Annotations;

    /// <summary>
    ///     Manager for the DependencyObject.LostFocus event.
    /// </summary>
    /// <seealso cref="System.Windows.WeakEventManager" />
    public class DataContextChangedEventManager : WeakEventManager
    {
        /// <summary>
        ///     Prevents a default instance of the <see cref="Anori.WPF.WeakEventManagers.DataContextChangedEventManager" /> class
        ///     from being created.
        /// </summary>
        private DataContextChangedEventManager()
        {
        }

        /// <summary>
        ///     Adds the listener.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     source
        ///     or
        ///     handler
        /// </exception>
        public static void AddHandler(DependencyObject source, EventHandler<DataContextChangedEventArgs> handler)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            GetCurrentManager().ProtectedAddHandler(source, handler);
        }

        /// <summary>
        ///     Removes the handler.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     source
        ///     or
        ///     handler
        /// </exception>
        public static void RemoveHandler(DependencyObject source, EventHandler<DataContextChangedEventArgs> handler)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            GetCurrentManager().ProtectedRemoveHandler(source, handler);
        }

        /// <summary>
        ///     When overridden in a derived class, starts listening for the event being managed. After the
        ///     <see cref="M:System.Windows.WeakEventManager.StartListening(System.Object)" /> method is first called, the manager
        ///     should be in the state of calling
        ///     <see cref="M:System.Windows.WeakEventManager.DeliverEvent(System.Object,System.EventArgs)" /> or
        ///     <see
        ///         cref="M:System.Windows.WeakEventManager.DeliverEventToList(System.Object,System.EventArgs,System.Windows.WeakEventManager.ListenerList)" />
        ///     whenever the relevant event from the provided source is handled.
        /// </summary>
        /// <param name="source">The source to begin listening on.</param>
        protected override void StartListening(object source)
        {
            if (!(source is FrameworkElement element))
            {
                return;
            }

            element.DataContextChanged += this.OnDataContextChanged;
        }

        /// <summary>
        ///     When overridden in a derived class, stops listening on the provided source for the event being managed.
        /// </summary>
        /// <param name="source">The source to stop listening on.</param>
        protected override void StopListening(object source)
        {
            if (!(source is FrameworkElement element))
            {
                return;
            }

            element.DataContextChanged -= this.OnDataContextChanged;
        }

        /// <summary>
        ///     Gets the current manager.
        /// </summary>
        /// <value>
        ///     The current manager.
        /// </value>
        [NotNull]
        private static DataContextChangedEventManager GetCurrentManager()
        {
            Type typeFromHandle = typeof(DataContextChangedEventManager);
            DataContextChangedEventManager dataContextChangedEventManager =
                (DataContextChangedEventManager)GetCurrentManager(typeFromHandle);
            if (dataContextChangedEventManager != null)
            {
                return dataContextChangedEventManager;
            }

            dataContextChangedEventManager = new DataContextChangedEventManager();
            SetCurrentManager(typeFromHandle, dataContextChangedEventManager);

            return dataContextChangedEventManager;
        }

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            DataContextChangedEventArgs dataContextChangedEventArgs =
                new DataContextChangedEventArgs(args.OldValue, args.NewValue);
            this.DeliverEvent(sender, dataContextChangedEventArgs);
        }
    }
}
