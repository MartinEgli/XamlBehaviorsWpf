// -----------------------------------------------------------------------
// <copyright file="TriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using JetBrains.Annotations;

    using System;
    using System.ComponentModel;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Windows;

    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerActionCreator" />
    public abstract class TriggerActionCreator : ITriggerActionCreator
    {
        /// <summary>
        ///     The unregister action
        /// </summary>
        private Action unregisterAction;

        /// <summary>
        ///     Attaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        /// <exception cref="ArgumentNullException">associatedObject</exception>
        public void Attach([NotNull] DependencyObject associatedObject)
        {
            if (associatedObject == null)
            {
                throw new ArgumentNullException(nameof(associatedObject));
            }

            if (associatedObject is FrameworkElement frameworkElement)
            {
                void Action() => frameworkElement.DataContextChanged -= this.OnDataContextChanged;
                Interlocked.Exchange(ref this.unregisterAction, Action)?.Invoke();
                frameworkElement.DataContextChanged += this.OnDataContextChanged;
            }
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        abstract public TriggerAction Create();

        /// <summary>
        ///     Detaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Detach([NotNull] DependencyObject associatedObject) => this.Unregister();

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        abstract protected void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e);

        /// <summary>
        ///     Unregisters this instance.
        /// </summary>
        private void Unregister() => Interlocked.Exchange(ref this.unregisterAction, null)?.Invoke();
    }
}
