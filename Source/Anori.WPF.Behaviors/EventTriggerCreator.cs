using System.Windows.Markup;

namespace Anori.WPF.Behaviors
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.EventTrigger" />
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerCreator" />
    [ContentProperty("ActionCreators")]
    public class EventTriggerCreator : EventTrigger, ITriggerCreator
    {
        /// <summary>
        /// The action creators
        /// </summary>
        private TriggerActionCreatorCollection actionCreators;

        /// <summary>
        /// Gets or sets the action creators.
        /// </summary>
        /// <value>
        /// The action creators.
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

        //public object SourceObject { get; set; }

        //public string SourceName { get; set; }

        //public string EventName
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public TriggerBase Create()
        {
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                var triggerAction = actionCreator.Create();
                this.Actions.Add(triggerAction);
            }

            return this;
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                actionCreator.Attach(AssociatedObject);
            }
        }

        /// <summary>
        /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            foreach (ITriggerActionCreator actionCreator in ActionCreators)
            {
                actionCreator.Detach(AssociatedObject);
            }

            base.OnDetaching();
        }
    }
}
