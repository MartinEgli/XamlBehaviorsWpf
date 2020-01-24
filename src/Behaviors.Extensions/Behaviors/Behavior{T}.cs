// -----------------------------------------------------------------------
// <copyright file="Behavior{T}.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System;
    using System.Windows;

    using Bfa.Common.WPF.WeakEventManagers;

    /// <summary>
    ///     Blend Behavior with weak loaded event
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <seealso cref="Bfa.Common.WPF.Behaviors.INotifyAttachedObjectLoaded{T}" />
    /// <seealso cref="System.Windows.IWeakEventListener" />
    /// <seealso cref="T:System.Windows.IWeakEventListener" />
    public abstract class Behavior<T> : Microsoft.Xaml.Behaviors.Behavior<T>,
                                        INotifyAttachedObjectLoaded<T>,
                                        IWeakEventListener
        where T : FrameworkElement
    {
        /// <summary>
        ///     The loaded
        /// </summary>
        /// ReSharper disable once NotAccessedField.Local
        private RoutedEventHandler loaded;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Behavior{T}" /> class.
        /// </summary>
        protected Behavior()
        {
            this.loaded = (s, e) => this.OnAttachedObjectLoaded();
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Behavior{T}" /> class.
        /// </summary>
        ~Behavior()
        {
            this.loaded = null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when [attached object loaded].
        /// </summary>
        public event EventHandler AttachedObjectLoaded;

        /// <summary>
        ///     Gets the attached object.
        /// </summary>
        /// <returns>The Type.</returns>
        /// <inheritdoc />
        public T GetAttachedObject() => this.AssociatedObject;

        /// <summary>
        ///     Receives events from the centralized event manager.
        /// </summary>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager" /> calling this method.</param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        /// <returns>
        ///     <see langword="true" /> if the listener handled the event. It is considered an error by the
        ///     <see cref="T:System.Windows.WeakEventManager" /> handling in WPF to register a listener for an event that the
        ///     listener does not handle. Regardless, the method should return <see langword="false" /> if it receives an event
        ///     that it does not recognize or handle.
        /// </returns>
        /// <inheritdoc />
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e) =>
            this.OnReceiveWeakEvent(managerType, sender, e);

        /// <inheritdoc />
        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            LoadedWeakEventManager.AddListener(this.AssociatedObject, this);
        }

        /// <summary>
        ///     Called when [attached object loaded].
        /// </summary>
        protected virtual void OnAttachedObjectLoaded()
        {
            this.AttachedObjectLoaded?.Invoke(this, EventArgs.Empty);
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
            base.OnDetaching();
            LoadedWeakEventManager.RemoveListener(this.AssociatedObject, this);
        }

        /// <summary>
        ///     Called when [receive weak event].
        /// </summary>
        /// <param name="managerType">Type of the manager.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <returns>
        ///     <see langword="true" /> if the listener handled the event. It is considered an error by the
        ///     <see cref="T:System.Windows.WeakEventManager" /> handling in WPF to register a listener for an event that the
        ///     listener does not handle. Regardless, the method should return <see langword="false" /> if it receives an event
        ///     that it does not recognize or handle.
        /// </returns>
        protected virtual bool OnReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType != typeof(LoadedWeakEventManager))
            {
                return false;
            }

            this.OnAttachedObjectLoaded();
            return true;
        }
    }
}