using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.ITriggerActionCreator" />
    public class InvokeCommandActionCreator : ITriggerActionCreator
    {
        /// <summary>
        /// The action
        /// </summary>
        private InvokeCommandAction action;

        /// <summary>
        /// The dispose
        /// </summary>
        private Action dispose;

        /// <summary>
        /// Gets or sets the command parameter binding.
        /// </summary>
        /// <value>
        /// The command parameter binding.
        /// </value>
        public BindingBase CommandParameterBinding { get; set; }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public object CommandParameter { get; set; }

        /// <summary>
        /// Gets or sets the command binding.
        /// </summary>
        /// <value>
        /// The command binding.
        /// </value>
        public BindingBase CommandBinding { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command { get; set; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public TriggerAction Create() => this.action = new InvokeCommandAction();

        /// <summary>
        /// Attaches the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Attach(DependencyObject obj)
        {
            if (!(obj is FrameworkElement frameworkElement))
            {
                return;
            }

            DetachDataContext();
            frameworkElement.DataContextChanged += this.OnDataContextChanged;
            this.dispose = () => frameworkElement.DataContextChanged -= this.OnDataContextChanged;
        }

        /// <summary>
        /// Called when [data context changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dataContext = e.NewValue;
            if (Command != null)
            {
                action.Command = this.Command;
            }

            if (CommandBinding != null)
            {
                BindingBase binding = CommandBinding.CloneBindingBase(dataContext);
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, binding);
            }

            if (CommandParameter != null)
            {
                action.CommandParameter = this.CommandParameter;
            }

            if (CommandParameterBinding != null)
            {
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandParameterProperty,
                    CommandParameterBinding.CloneBindingBase(dataContext));
            }
        }

        /// <summary>
        /// Detaches the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Detach(DependencyObject obj) => this.DetachDataContext();

        /// <summary>
        /// Detaches the data context.
        /// </summary>
        private void DetachDataContext()
        {
            var detach = this.dispose;
            this.dispose = null;
            detach?.Invoke();
        }
    }
}
