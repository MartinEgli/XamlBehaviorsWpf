// -----------------------------------------------------------------------
// <copyright file="ObservableTrigger.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    ///     A trigger that listens for a specified event on its source and fires when that event is fired.
    /// </summary>
    public class ObservableTrigger : ObservableTriggerBase<object>
    {
        /// <summary>
        ///     The event name property
        /// </summary>
        public static readonly DependencyProperty ObservableNameProperty = DependencyProperty.Register(
            "ObservableName",
            typeof(string),
            typeof(ObservableTrigger),
            new FrameworkPropertyMetadata("Loaded", OnObservableNameChanged));

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableTrigger" /> class.
        /// </summary>
        public ObservableTrigger()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableTrigger" /> class.
        /// </summary>
        /// <param name="observableName">Name of the observable.</param>
        public ObservableTrigger(string observableName)
        {
            this.ObservableName = observableName;
        }

        /// <summary>
        ///     Gets or sets the name of the event to listen for. This is a dependency property.
        /// </summary>
        /// <value>
        ///     The name of the event.
        /// </value>
        [SuppressMessage("Anori.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
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
