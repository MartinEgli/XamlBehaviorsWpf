// -----------------------------------------------------------------------
// <copyright file="ExecutableDependencyPropertyChangedEventArgsCommandBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.TriggerActions
{
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
    public class ExecutableDependencyPropertyChangedEventArgsCommandBehavior : CommandBehaviorBase<FrameworkElement>
    {
        /// <summary>
        ///     The event arguments converter
        /// </summary>
        private IDependencyPropertyChangedEventArgsConverter dependencyPropertyChangedEventArgsConverter;

        /// <summary>
        ///     The event arguments converter parameter
        /// </summary>
        private object eventArgsConverterParameter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExecutableDependencyPropertyChangedEventArgsCommandBehavior" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public ExecutableDependencyPropertyChangedEventArgsCommandBehavior(FrameworkElement target)
            : base(target)
        {
        }

        /// <summary>
        ///     Gets or sets the event arguments converter.
        /// </summary>
        /// <value>
        ///     The event arguments converter.
        /// </value>
        public IDependencyPropertyChangedEventArgsConverter DependencyPropertyChangedEventArgsConverter
        {
            get => this.dependencyPropertyChangedEventArgsConverter;
            set
            {
                if (this.dependencyPropertyChangedEventArgsConverter == value)
                {
                    return;
                }

                this.dependencyPropertyChangedEventArgsConverter = value;
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
            if (this.DependencyPropertyChangedEventArgsConverter != null
                && parameter is DependencyPropertyChangedEventArgs args)
            {
                this.Command?.Execute(
                    this.DependencyPropertyChangedEventArgsConverter.Convert(
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

            if (this.DependencyPropertyChangedEventArgsConverter != null)
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