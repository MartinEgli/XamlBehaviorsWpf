﻿// -----------------------------------------------------------------------
// <copyright file="ObservableTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    using JetBrains.Annotations;

    /// <summary>
    ///     A trigger that listens for a specified event on its source and fires when that event is fired.
    /// </summary>
    public class ObservableTrigger : ObservableTriggerBase<object>
    {
        /// <summary>
        ///     The event name property
        /// </summary>
        public static readonly DependencyProperty ObservableNameProperty = DependencyProperty.Register(
            nameof(ObservableName),
            typeof(string),
            typeof(ObservableTrigger),
            new FrameworkPropertyMetadata(OnObservableNameChanged));

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableTrigger" /> class.
        /// </summary>
        public ObservableTrigger()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableTrigger" /> class.
        /// </summary>
        /// <param name="observableName">Name of the observable property.</param>
        /// <exception cref="ArgumentNullException">observableName is null.</exception>
        public ObservableTrigger([NotNull] string observableName)
        {
            this.ObservableName = observableName ?? throw new ArgumentNullException(nameof(observableName));
        }

        ~ObservableTrigger()
        {
        }

        /// <summary>
        ///     Gets or sets the name of the event to listen for. This is a dependency property.
        /// </summary>
        /// <value>
        ///     The name of the event.
        /// </value>
        [SuppressMessage("Anori.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        [CanBeNull]
        public string ObservableName
        {
            get { return (string)this.GetValue(ObservableNameProperty); }
            set { this.SetValue(ObservableNameProperty, value); }
        }

        /// <summary>
        ///     Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
        protected override string GetObservableName() => this.ObservableName;

        /// <summary>
        ///     Called when [event name changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnObservableNameChanged(object sender, DependencyPropertyChangedEventArgs args) =>
            ((ObservableTrigger)sender).OnObservableNameChanged((string)args.OldValue, (string)args.NewValue);
    }
}
