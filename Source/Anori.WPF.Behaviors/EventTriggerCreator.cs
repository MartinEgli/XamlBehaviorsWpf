using System.Windows.Markup;

namespace Anori.WPF.Behaviors
{
    [ContentProperty("ActionCreators")]
    public class EventTriggerCreator : EventTrigger, ITriggerCreator
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
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                this.Actions.Add(actionCreator.Create());
            }

            return this;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
