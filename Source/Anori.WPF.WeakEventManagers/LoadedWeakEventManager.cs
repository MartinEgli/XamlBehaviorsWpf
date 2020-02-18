namespace Anori.WPF.Interactivities.WeakEventManagers
{
    #region

    using System;
    using System.Windows;

    using JetBrains.Annotations;

    #endregion

    /// <inheritdoc />
    /// <summary>
    /// Loaded WeakEventManager Class
    /// </summary>
    /// <seealso cref="T:System.Windows.WeakEventManager" />
    public sealed class LoadedWeakEventManager : WeakEventManager
    {
        /// <inheritdoc />
        /// <summary>
        /// Prevents a default instance of the <see cref="T:Anori.WPF.Interactivities.WeakEventManagers.LoadedWeakEventManager" /> class from being created.
        /// </summary>
        private LoadedWeakEventManager()
        {
        }

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="System.ArgumentNullException">
        /// source
        /// or
        /// handler
        /// </exception>
        public static void AddHandler(
            DependencyObject source,
            EventHandler<RoutedEventArgs> handler)
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
        public static void RemoveHandler(
            DependencyObject source,
            EventHandler<RoutedEventArgs> handler)
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
        /// Gets the current manager.
        /// </summary>
        /// <returns></returns>
        private static LoadedWeakEventManager GetCurrentManager()
        {
            var type = typeof(LoadedWeakEventManager);

            if (WeakEventManager.GetCurrentManager(type) is LoadedWeakEventManager manager)
            {
                return manager;
            }

            manager = new LoadedWeakEventManager();
            SetCurrentManager(type, manager);

            return manager;
        }

        /// <summary>
        /// Adds the listener.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="listener">The listener.</param>
        /// <exception cref="System.ArgumentNullException">
        /// source
        /// or
        /// listener
        /// </exception>
        /// <exception cref="ArgumentNullException">source
        /// or
        /// listener</exception>
        public static void AddListener([NotNull] FrameworkElement source, [NotNull] IWeakEventListener listener)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            GetCurrentManager().ProtectedAddListener(source, listener);
        }

        /// <summary>
        /// Removes the listener.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="listener">The listener.</param>
        /// <exception cref="System.ArgumentNullException">
        /// source
        /// or
        /// listener
        /// </exception>
        /// <exception cref="ArgumentNullException">source
        /// or
        /// listener</exception>
        public static void RemoveListener([NotNull] FrameworkElement source, [NotNull] IWeakEventListener listener)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }

            GetCurrentManager().ProtectedRemoveListener(source, listener);
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs args) => this.DeliverEvent(sender, args);

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, starts listening for the event being managed.
        /// After <see cref="M:System.Windows.WeakEventManager.StartListening(System.Object)" /> is first called,
        /// the manager should be in the state of calling <see cref="M:System.Windows.WeakEventManager.DeliverEvent(System.Object,System.EventArgs)" /> or <see cref="M:System.Windows.WeakEventManager.DeliverEventToList(System.Object,System.EventArgs,System.Windows.WeakEventManager.ListenerList)" /> whenever the relevant event from the provided source is handled.
        /// </summary>
        /// <param name="source">The source to begin listening on.</param>
        protected override void StartListening(object source)
        {
            if (source is FrameworkElement trigger)
            {
                trigger.Loaded += this.OnLoaded;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, stops listening on the provided source for the event being managed.
        /// </summary>
        /// <param name="source">The source to stop listening on.</param>
        protected override void StopListening(object source)
        {
            if (source is FrameworkElement trigger)
            {
                trigger.Loaded -= this.OnLoaded;
            }
        }
    }
}
