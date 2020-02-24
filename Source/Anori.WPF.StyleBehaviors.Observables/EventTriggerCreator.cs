// -----------------------------------------------------------------------
// <copyright file="EventTriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors.Observables
{
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    using Anori.WPF.Behaviors;
    using Anori.WPF.Behaviors.Observables;

    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;
    using TriggerBase = Anori.WPF.Behaviors.TriggerBase;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.EventTrigger" />
    /// <seealso cref="ITriggerCreator" />
    [ContentProperty("ActionCreators")]
    public class ObservableTriggerCreator : ITriggerCreator
    {
        /// <summary>
        ///     The action creators
        /// </summary>
        private TriggerActionCreatorCollection actionCreators;

        /// <summary>
        ///     The observable trigger
        /// </summary>
        private ObservableTrigger observableTrigger;

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
        public string ObservableName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the error action creators.
        /// </summary>
        /// <value>
        ///     The error action creators.
        /// </value>
        public TriggerActionCreatorCollection ErrorActionCreators
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the completed action creators.
        /// </summary>
        /// <value>
        ///     The completed action creators.
        /// </value>
        public TriggerActionCreatorCollection CompletedActionCreators
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the next action creators.
        /// </summary>
        /// <value>
        ///     The next action creators.
        /// </value>
        public TriggerActionCreatorCollection NextActionCreators
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the source object binding.
        /// </summary>
        /// <value>
        ///     The source object binding.
        /// </value>
        public Binding SourceObjectBinding
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
            this.observableTrigger = new ObservableTrigger();
            this.observableTrigger.ObservableName = this.ObservableName;
            this.observableTrigger.SourceName = this.SourceName;
            this.observableTrigger.SourceObject = this.SourceObject;
            foreach (ITriggerActionCreator actionCreator in this.ActionCreators)
            {
                TriggerAction triggerAction = actionCreator.Create(dependencyObject);
                this.observableTrigger.Actions.Add(triggerAction);
            }

            return this.observableTrigger;
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
