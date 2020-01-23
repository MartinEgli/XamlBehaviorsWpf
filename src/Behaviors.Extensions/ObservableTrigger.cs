namespace Behaviors.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    /// A trigger that listens for a specified event on its source and fires when that event is fired.
    /// </summary>
    public class ObservableTrigger : ObservableTriggerBase<object>
    {
        /// <summary>
        /// The event name property
        /// </summary>
        public static readonly DependencyProperty ObservableNameProperty = DependencyProperty.Register("ObservableName",
                                                                                                    typeof(string),
                                                                                                    typeof(DependencyPropertyChangedEventTrigger),
                                                                                                    new FrameworkPropertyMetadata(
                                                                                                        "Loaded",
                                                                                                        new PropertyChangedCallback(OnEventNameChanged)));

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventTrigger"/> class.
        /// </summary>
        public ObservableTrigger()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventTrigger"/> class.
        /// </summary>
        /// <param name="observableName">Name of the event.</param>
        public ObservableTrigger(string observableName)
        {
            this.ObservableName = observableName;
        }

        /// <summary>
        /// Gets or sets the name of the event to listen for. This is a dependency property.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string ObservableName
        {
            get { return (string)this.GetValue(ObservableNameProperty); }
            set { this.SetValue(ObservableNameProperty, value); }
        }

        /// <summary>
        /// Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns></returns>
        protected override string GetEventName() => this.ObservableName;

        /// <summary>
        /// Called when [event name changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args) => ((DependencyPropertyChangedEventTrigger)sender).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
    }
}
