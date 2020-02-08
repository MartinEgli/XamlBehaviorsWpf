namespace Anori.WPF.Behaviors
{
    public class EventTriggerCreator : ITriggerCreator
    {
        public TriggerBase Create()
        {
            return new EventTrigger(EventName);
        }

        public string EventName
        {
            get;
            set;
        }
    }
}
