// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerActionCreator" />
    public class InvokeCommandActionCreator : TriggerActionCreator
    {
        /// <summary>
        ///     The action
        /// </summary>
        private InvokeCommandAction action;

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
        /// <returns></returns>
        public override TriggerAction Create() => action = new InvokeCommandAction();

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BindingOperations.ClearBinding(action, InvokeCommandAction.CommandProperty);

            object dataContext = e.NewValue;
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
