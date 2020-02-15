// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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

        protected override void DataContextChanged(InvokeCommandAction action, object dataContext)
        {
            BindingOperations.ClearBinding(action, InvokeCommandAction.CommandProperty);

            if (Command != null)
            {
                action.Command = this.Command;
            }

            if (CommandBinding != null)
            {
                BindingBase binding = CommandBinding.CloneBindingBase(dataContext);
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, binding);
            }

            BindingOperations.ClearBinding(action, InvokeCommandAction.CommandParameterProperty);

            if (CommandParameter != null)
            {
                action.CommandParameter = this.CommandParameter;
            }

            if (CommandParameterBinding != null)
            {
                BindingOperations.SetBinding(
                    action,
                    InvokeCommandAction.CommandParameterProperty,
                    CommandParameterBinding.CloneBindingBase(dataContext));
            }
        }
    }
}
