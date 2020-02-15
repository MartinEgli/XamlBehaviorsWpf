// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerActionCreator" />
    public class InvokeCommandActionCreator : TriggerActionCreator<InvokeCommandAction>
    {
        /// <summary>
        ///     Gets or sets the command parameter binding.
        /// </summary>
        /// <value>
        ///     The command parameter binding.
        /// </value>
        public BindingBase CommandParameterBinding { get; set; }

        /// <summary>
        ///     Gets or sets the command parameter.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        public object CommandParameter { get; set; }

        /// <summary>
        ///     Gets or sets the command binding.
        /// </summary>
        /// <value>
        ///     The command binding.
        /// </value>
        public BindingBase CommandBinding { get; set; }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command { get; set; }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        public override InvokeCommandAction CreateTriggerAction()
        {
            return new InvokeCommandAction();
        }

        protected override void DataContextChanged(InvokeCommandAction triggerAction, object dataContext)
        {
            BindingOperations.ClearBinding(triggerAction, InvokeCommandAction.CommandProperty);

            if (Command != null)
            {
                if (CommandBinding != null)
                {
                    throw new Exception("Command and CommandBinding");
                }

                triggerAction.Command = this.Command;
            }

            if (CommandBinding != null)
            {
                BindingBase binding = CommandBinding.CloneBindingBase(dataContext);
                BindingOperations.SetBinding(triggerAction, InvokeCommandAction.CommandProperty, binding);
            }

            BindingOperations.ClearBinding(triggerAction, InvokeCommandAction.CommandParameterProperty);

            if (CommandParameter != null)
            {
                triggerAction.CommandParameter = this.CommandParameter;
            }

            if (CommandParameterBinding != null)
            {
                BindingOperations.SetBinding(
                    triggerAction,
                    InvokeCommandAction.CommandParameterProperty,
                    CommandParameterBinding.CloneBindingBase(dataContext));
            }
        }
    }
}
