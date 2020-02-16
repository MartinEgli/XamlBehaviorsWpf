// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using Anori.WPF.Behaviors;

    using JetBrains.Annotations;

    using System;
    using System.Windows.Data;
    using System.Windows.Input;

    /// <summary>
    ///     InvokeCommandActionCreator
    /// </summary>
    /// <seealso cref="ITriggerActionCreator" />
    public class InvokeCommandActionCreator : TriggerActionCreator<InvokeCommandAction>
    {
        /// <summary>
        ///     Gets or sets the command parameter binding.
        /// </summary>
        /// <value>
        ///     The command parameter binding.
        /// </value>
        [CanBeNull]
        public BindingBase CommandParameterBinding { get; set; }

        /// <summary>
        ///     Gets or sets the command parameter.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        [CanBeNull]
        public object CommandParameter { get; set; }

        /// <summary>
        ///     Gets or sets the command binding.
        /// </summary>
        /// <value>
        ///     The command binding.
        /// </value>
        [CanBeNull]
        public BindingBase CommandBinding { get; set; }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        [CanBeNull]
        public ICommand Command { get; set; }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override InvokeCommandAction CreateTriggerAction() => new InvokeCommandAction();

        /// <summary>
        ///     Called when [data context changed].
        /// </summary>
        /// <param name="triggerAction"></param>
        /// <param name="dataContext"></param>
        /// <exception cref="System.Exception">Command and CommandBinding</exception>
        protected override void DataContextChanged(InvokeCommandAction triggerAction, object dataContext)
        {
            this.SetupCommand(triggerAction, dataContext);
            this.SetupCommandParameter(triggerAction, dataContext);
        }

        /// <summary>
        ///     Setups the command parameter.
        /// </summary>
        /// <param name="triggerAction">The trigger action.</param>
        /// <param name="dataContext">The data context.</param>
        private void SetupCommandParameter([NotNull] InvokeCommandAction triggerAction, [NotNull] object dataContext)
        {
            BindingOperations.ClearBinding(triggerAction, InvokeCommandAction.CommandParameterProperty);

            if (this.CommandParameter != null)
            {
                triggerAction.CommandParameter = this.CommandParameter;
            }

            if (this.CommandParameterBinding != null)
            {
                BindingOperations.SetBinding(
                    triggerAction,
                    InvokeCommandAction.CommandParameterProperty,
                    this.CommandParameterBinding.CloneBindingBase(dataContext));
            }
        }

        /// <summary>
        ///     Setups the command.
        /// </summary>
        /// <param name="triggerAction">The trigger action.</param>
        /// <param name="dataContext">The data context.</param>
        /// <exception cref="System.Exception">Command and CommandBinding</exception>
        private void SetupCommand([NotNull] InvokeCommandAction triggerAction, [NotNull] object dataContext)
        {
            BindingOperations.ClearBinding(triggerAction, InvokeCommandAction.CommandProperty);

            if (this.Command != null)
            {
                if (this.CommandBinding != null)
                {
                    throw new Exception("Command and CommandBinding");
                }

                triggerAction.Command = this.Command;
            }

            if (this.CommandBinding != null)
            {
                BindingBase binding = this.CommandBinding.CloneBindingBase(dataContext);
                BindingOperations.SetBinding(triggerAction, InvokeCommandAction.CommandProperty, binding);
            }
        }
    }
}
