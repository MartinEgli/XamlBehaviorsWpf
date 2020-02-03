// -----------------------------------------------------------------------
// <copyright file="CommandBehaviorBase.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.TriggerActions
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Anori.WPF.Extensions;

    /// <summary>
    ///     Base behavior to handle connecting a <see cref="System.Windows.Controls.Control" /> to a Command.
    /// </summary>
    /// <typeparam name="T">The target object must derive from Control</typeparam>
    /// <remarks>
    ///     CommandBehaviorBase can be used to provide new behaviors for commands.
    /// </remarks>
    public abstract class CommandBehaviorBase<T>
        where T : FrameworkElement
    {
        /// <summary>
        ///     The command can execute changed handler
        /// </summary>
        private readonly EventHandler commandCanExecuteChangedHandler;

        /// <summary>
        ///     The target object
        /// </summary>
        private readonly WeakReference targetObject;

        /// <summary>
        ///     The automatic enabled
        /// </summary>
        private bool autoEnabled = true;

        /// <summary>
        ///     The command
        /// </summary>
        private ICommand command;

        /// <summary>
        ///     The command parameter
        /// </summary>
        private object commandParameter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBehaviorBase{T}" /> class.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        protected CommandBehaviorBase(T targetObject)
        {
            this.targetObject = new WeakReference(targetObject);
            this.commandCanExecuteChangedHandler = this.CommandCanExecuteChanged;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [automatic enable].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [automatic enable]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoEnable
        {
            get => this.autoEnabled;
            set
            {
                this.autoEnabled = value;
                this.UpdateEnabledState();
            }
        }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command
        {
            get => this.command;
            set
            {
                if (this.command != null)
                {
                    this.command.CanExecuteChanged -= this.commandCanExecuteChangedHandler;
                    this.CommandParameterType = null;
                }

                this.command = value;
                this.CommandParameterType = CommandExtensions.GetCommandParameterType(this.command);
                if (this.command == null)
                {
                    return;
                }

                this.command.CanExecuteChanged += this.commandCanExecuteChangedHandler;
                this.UpdateEnabledState();
            }
        }

        /// <summary>
        ///     Gets or sets the command parameter.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        public object CommandParameter
        {
            get => this.commandParameter;
            set
            {
                if (this.commandParameter == value)
                {
                    return;
                }

                this.commandParameter = value;
                this.UpdateEnabledState();
            }
        }

        /// <summary>
        ///     Gets the type of the command parameter.
        /// </summary>
        /// <value>
        ///     The type of the command parameter.
        /// </value>
        protected Type CommandParameterType { get; private set; }

        /// <summary>
        ///     Gets the target object.
        /// </summary>
        /// <value>
        ///     The target object.
        /// </value>
        protected T TargetObject => this.targetObject.Target as T;

        /// <summary>
        ///     Executes the command, if it's set, providing the <see cref="CommandParameter" />
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void ExecuteCommand(object parameter) =>
            this.Command?.Execute(this.CommandParameter ?? parameter);

        /// <summary>
        ///     Updates the target object's IsEnabled property based on the commands ability to execute.
        /// </summary>
        protected virtual void UpdateEnabledState()
        {
            if (this.TargetObject == null)
            {
                this.Command = null;
                this.CommandParameter = null;
            }
            else if (this.Command != null && this.AutoEnable)
            {
                this.TargetObject.IsEnabled = this.Command.CanExecute(this.CommandParameter);
            }
        }

        /// <summary>
        ///     Commands the can execute changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void CommandCanExecuteChanged(object sender, EventArgs e) => this.UpdateEnabledState();
    }
}