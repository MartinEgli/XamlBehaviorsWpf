using System.Windows;
using System.Windows.Markup;

namespace Anori.WPF.Behaviors
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.EventTrigger" />
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerCreator" />
    [ContentProperty("ActionCreators")]
    public class EventTriggerCreator : ITriggerCreator
    {
        /// <summary>
        ///     The action creators
        /// </summary>
        private TriggerActionCreatorCollection actionCreators;

        private EventTrigger eventTrigger;

        /// <summary>
        ///     Gets or sets the action creators.
        /// </summary>
        /// <value>
        ///     The action creators.
        /// </value>
        public TriggerActionCreatorCollection ActionCreators
        {
            get
            {
                TriggerActionCreatorCollection triggerActionCreatorCollection =
                    this.actionCreators;
                if (triggerActionCreatorCollection == null)
                {
                    triggerActionCreatorCollection = new TriggerActionCreatorCollection();
                }

                actionCreators = triggerActionCreatorCollection;
                return triggerActionCreatorCollection;
            }
            set
            {
                this.actionCreators = value;
            }
        }

        public object SourceObject { get; set; }

        public string SourceName { get; set; }

        public string EventName
        {
            get;
            set;
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        public TriggerBase Create(DependencyObject dependencyObject)
        {
            eventTrigger = new EventTrigger();
            this.eventTrigger.EventName = EventName;
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                TriggerAction triggerAction = actionCreator.Create(dependencyObject);
                eventTrigger.Actions.Add(triggerAction);
            }

            return eventTrigger;
        }

        /// <summary>
        ///     Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        //public void Attach(DependencyObject associatedObject)
        //{
        //    foreach (ITriggerActionCreator actionCreator in ActionCreators)
        //    {
        //        actionCreator.Attach(associatedObject);
        //    }
        //}

        ///// <summary>
        /////     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        ///// </summary>
        //public void Detach(DependencyObject associatedObject)
        //{
        //    foreach (ITriggerActionCreator actionCreator in ActionCreators)
        //    {
        //        actionCreator.Detach(associatedObject);
        //    }
        //}
    }
}
