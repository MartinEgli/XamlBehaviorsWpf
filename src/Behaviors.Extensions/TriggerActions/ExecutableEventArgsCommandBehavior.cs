// -----------------------------------------------------------------------
// <copyright file="ExecutableEventArgsCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.TriggerActions
{
    using System;
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    ///     A CommandBehavior that exposes a public ExecuteCommand method. It provides the functionality to invoke commands and
    ///     update Enabled state of the target control.
    ///     It is not possible to make the <see cref="T:Anori.WPF.Interactivities.InvokeCommandAction" /> inherit from
    ///     <see cref="T:WpfApp17.CommandBehaviorBase`1" />, since the
    ///     <see cref="T:Anori.WPF.Interactivities.InvokeCommandAction" />
    ///     must already inherit from <see cref="T:System.Windows.Interactivity.TriggerAction`1" />, so we chose to follow the
    ///     aggregation approach.
    /// </summary>
    public class ExecutableEventArgsCommandBehavior : CommandBehaviorBase<FrameworkElement>
    {
        /// <summary>
        ///     The event arguments converter
        /// </summary>
        private IEventArgsConverter eventArgsConverter;

        /// <summary>
        ///     The event arguments converter parameter
        /// </summary>
        private object eventArgsConverterParameter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExecutableEventArgsCommandBehavior" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public ExecutableEventArgsCommandBehavior(FrameworkElement target)
            : base(target)
        {
        }

        /// <summary>
        ///     Gets or sets the event arguments converter.
        /// </summary>
        /// <value>
        ///     The event arguments converter.
        /// </value>
        public IEventArgsConverter EventArgsConverter
        {
            get => this.eventArgsConverter;
            set
            {
                if (this.eventArgsConverter == value)
                {
                    return;
                }

                this.eventArgsConverter = value;
                this.UpdateEnabledState();
            }
        }

        /// <summary>
        ///     Gets or sets the event arguments converter parameter.
        /// </summary>
        /// <value>
        ///     The event arguments converter parameter.
        /// </value>
        public object EventArgsConverterParameter
        {
            get => this.eventArgsConverterParameter;
            set
            {
                if (this.eventArgsConverterParameter == value)
                {
                    return;
                }

                this.eventArgsConverterParameter = value;
                this.UpdateEnabledState();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the command, if it's set, providing the
        ///     <see cref="P:Anori.WPF.Interactivities.CommandBehaviorBase`1.CommandParameter" />
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void ExecuteCommand(object parameter)
        {
            if (this.EventArgsConverter != null && parameter is EventArgs args)
            {
                this.Command?.Execute(
                    this.EventArgsConverter.Convert(
                        args,
                        this.CommandParameterType,
                        this.EventArgsConverterParameter,
                        this.TargetObject));
            }
            else
            {
                this.Command?.Execute(this.CommandParameter ?? parameter);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Updates the target object's IsEnabled property based on the commands ability to execute.
        /// </summary>
        protected override void UpdateEnabledState()
        {
            if (this.TargetObject == null)
            {
                this.Command = null;
                this.CommandParameter = null;
                return;
            }

            if (this.EventArgsConverter != null)
            {
                return;
            }

            if (this.Command != null && this.AutoEnable)
            {
                this.TargetObject.IsEnabled = this.Command.CanExecute(this.CommandParameter);
            }
        }
    }
}