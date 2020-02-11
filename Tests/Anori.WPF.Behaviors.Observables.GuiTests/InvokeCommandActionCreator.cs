using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    public class InvokeCommandActionCreator : ITriggerActionCreator
    {
        public TriggerAction Create()
        {
            var action = new InvokeCommandAction();
            if (Command != null)
            {
                action.Command = this.Command;
            }

            if (CommandBinding != null)
            {
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty,
                    CommandBinding.CloneBindingBase());
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

        public BindingBase CommandParameterBinding { get; set; }

        public object CommandParameter { get; set; }

        public BindingBase CommandBinding { get; set; }

        public ICommand Command { get; set; }
    }
}
