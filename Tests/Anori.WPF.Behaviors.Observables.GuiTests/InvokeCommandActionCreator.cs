using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    public class InvokeCommandActionCreator : ITriggerActionCreator
    {
        public BindingBase CommandParameterBinding { get; set; }

        public object CommandParameter { get; set; }

        public BindingBase CommandBinding { get; set; }

        public ICommand Command { get; set; }

        public TriggerAction Create()
        {
            InvokeCommandAction action = new InvokeCommandAction();
            if (Command != null)
            {
                action.Command = this.Command;
            }

            if (CommandBinding != null)
            {
                BindingBase binding = CommandBinding.CloneBindingBase();
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, binding);
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, CommandBinding);
            }

            if (CommandParameter != null)
            {
                action.CommandParameter = this.CommandParameter;
            }

            if (CommandParameterBinding != null)
            {
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandParameterProperty,
                    CommandParameterBinding.CloneBindingBase());
            }

            return action;
        }
    }
}
