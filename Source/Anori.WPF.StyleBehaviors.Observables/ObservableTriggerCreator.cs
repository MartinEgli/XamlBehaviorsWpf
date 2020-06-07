// -----------------------------------------------------------------------
// <copyright file="ObservableTriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors.Observables
{
    using Anori.WPF.Behaviors;
    using Anori.WPF.Behaviors.Observables;
    using JetBrains.Annotations;
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;
    using TriggerBase = Anori.WPF.Behaviors.TriggerBase;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerCreator" />
    /// <seealso cref="Anori.WPF.Behaviors.EventTrigger" />
    /// <seealso cref="ITriggerCreator" />
    [ContentProperty("NextActionCreators")]
    public class ObservableTriggerCreator : ITriggerCreator
    {
        /// <summary>
        ///     The trigger creators
        /// </summary>
        private TriggerActionCreatorCollection actionCreators;

        /// <summary>
        ///     The observable trigger
        /// </summary>
        private ObservableTrigger observableTrigger;

        /// <summary>
        ///     Gets or sets the trigger creators.
        /// </summary>
        /// <value>
        ///     The trigger creators.
        /// </value>
        public TriggerActionCreatorCollection NextActionCreators
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
        ///     Gets or sets the error trigger creators.
        /// </summary>
        /// <value>
        ///     The error trigger creators.
        /// </value>
        public TriggerActionCreatorCollection ErrorActionCreators
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the completed trigger creators.
        /// </summary>
        /// <value>
        ///     The completed trigger creators.
        /// </value>
        public TriggerActionCreatorCollection CompletedActionCreators
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
            this.Register(observableTrigger, dependencyObject);

            foreach (ITriggerActionCreator nextActionCreator in this.NextActionCreators)
            {
                TriggerAction triggerAction = nextActionCreator.Create(dependencyObject);
                this.observableTrigger.NextActions.Add(triggerAction);
            }

            foreach (ITriggerActionCreator completedActionCreator in this.CompletedActionCreators)
            {
                TriggerAction triggerAction = completedActionCreator.Create(dependencyObject);
                this.observableTrigger.CompletedActions.Add(triggerAction);
            }

            foreach (ITriggerActionCreator errorActionCreator in this.ErrorActionCreators)
            {
                TriggerAction triggerAction = errorActionCreator.Create(dependencyObject);
                this.observableTrigger.ErrorActions.Add(triggerAction);
            }

            return this.observableTrigger;
        }

        public void Register([NotNull] ObservableTrigger trigger, [NotNull] DependencyObject associatedObject)
        {
            if (trigger == null)
            {
                throw new ArgumentNullException(nameof(trigger));
            }

            if (associatedObject == null)
            {
                throw new ArgumentNullException(nameof(associatedObject));
            }

            if (associatedObject is FrameworkElement frameworkElement)
            {
                DependencyPropertyChangedEventHandler OnDataContextChanged =
                    (sender, args) => this.DataContextChanged(trigger, args.NewValue);

                frameworkElement.DataContextChanged += OnDataContextChanged;
            }
        }

        /// <summary>
        ///     Datas the context changed.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="dataContext">The data context.</param>
        protected void DataContextChanged(ObservableTrigger trigger, object dataContext)
        {
            this.observableTrigger.ObservableName = this.ObservableName;
            this.observableTrigger.SourceName = this.SourceName;
            SetupSourceObject(observableTrigger, dataContext);
        }

        /// <summary>
        ///     Setups the command parameter.
        /// </summary>
        /// <param name="observableAction">The trigger trigger.</param>
        /// <param name="dataContext">The data context.</param>
        private void SetupSourceObject([NotNull] ObservableTrigger trigger, [NotNull] object dataContext)
        {
            BindingOperations.ClearBinding(trigger, ObservableTrigger.SourceObjectProperty);

            if (this.SourceObject != null)
            {
                trigger.SourceObject = this.SourceObject;
            }

            if (this.SourceObjectBinding != null)
            {
                BindingOperations.SetBinding(
                    trigger,
                    ObservableTrigger.SourceObjectProperty,
                    this.SourceObjectBinding.CloneBindingBase(dataContext));
            }
        }

        /// <summary>
        ///     Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        //public void Attach(DependencyObject associatedObject)
        //{
        //    foreach (ITriggerActionCreator actionCreator in NextActionCreators)
        //    {
        //        actionCreator.Attach(associatedObject);
        //    }
        //}

        ///// <summary>
        /////     Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        ///// </summary>
        //public void Detach(DependencyObject associatedObject)
        //{
        //    foreach (ITriggerActionCreator actionCreator in NextActionCreators)
        //    {
        //        actionCreator.Detach(associatedObject);
        //    }
        //}
    }
}
