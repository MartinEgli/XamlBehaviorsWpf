// -----------------------------------------------------------------------
// <copyright file="EventTriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using Anori.WPF.Behaviors;
    using System.Windows;
    using System.Windows.Markup;
    using EventTrigger = Anori.WPF.Behaviors.EventTrigger;
    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;
    using TriggerBase = Behaviors.TriggerBase;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.EventTrigger" />
    /// <seealso cref="ITriggerCreator" />
    [ContentProperty("ActionCreators")]
    public class EventTriggerCreator : ITriggerCreator
    {
        /// <summary>
        ///     The action creators
        /// </summary>
        private TriggerActionCreatorCollection actionCreators;

        /// <summary>
        ///     The event trigger
        /// </summary>
        private EventTrigger eventTrigger;

        /// <summary>
        ///     Gets or sets the action creators.
        /// </summary>
        /// <value>
        ///     The action creators.
        /// </value>
        public TriggerActionCreatorCollection ActionCreators
        {
            get => this.actionCreators ?? (this.actionCreators = new TriggerActionCreatorCollection());
            set => this.actionCreators = value;
        }

        /// <summary>
        ///     Gets or sets the source object.
        /// </summary>
        /// <value>
        ///     The source object.
        /// </value>
        public object SourceObject { get; set; }

        /// <summary>
        ///     Gets or sets the name of the source.
        /// </summary>
        /// <value>
        ///     The name of the source.
        /// </value>
        public string SourceName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the event.
        /// </summary>
        /// <value>
        ///     The name of the event.
        /// </value>
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
            this.eventTrigger = new EventTrigger();
            this.eventTrigger.EventName = this.EventName;
            this.eventTrigger.SourceName = this.SourceName;
            this.eventTrigger.SourceObject = this.SourceObject;
            foreach (ITriggerActionCreator actionCreator in this.ActionCreators)
            {
                TriggerAction triggerAction = actionCreator.Create(dependencyObject);
                this.eventTrigger.Actions.Add(triggerAction);
            }

            return this.eventTrigger;
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
