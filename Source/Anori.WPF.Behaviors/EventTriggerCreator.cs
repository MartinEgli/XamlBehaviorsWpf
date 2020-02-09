using System.Windows.Markup;

namespace Anori.WPF.Behaviors
{
    [ContentProperty("ActionCreators")]
    public class EventTriggerCreator : ITriggerCreator
    {
        private TriggerActionCreatorCollection actionCreators;

        public TriggerActionCreatorCollection ActionCreators
        {
            get
            {
                TriggerActionCreatorCollection triggerActionCreatorCollection =
                    this.actionCreators ?? new TriggerActionCreatorCollection();
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

        public TriggerBase Create()
        {
            EventTrigger eventTrigger =
                new EventTrigger(EventName) { SourceName = SourceName, SourceObject = SourceObject };
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                eventTrigger.Actions.Add(actionCreator.Create());
            }

            return eventTrigger;
        }
    }
}
