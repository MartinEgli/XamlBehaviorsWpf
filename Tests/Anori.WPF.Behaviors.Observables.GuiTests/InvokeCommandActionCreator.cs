using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    public class InvokeCommandActionCreator : ITriggerActionCreator
    {
        public TriggerAction Create()
        {
            var action = new InvokeCommandAction();
            if (Commnd != null)
            {
                action.Command = this.Commnd;
            }

            if (CommandBinding != null)
            {
                BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, CommandBinding.CloneBindingBase())
            }

            return action;
        }

        public BindingBase CommandBinding { get; set; }

        public ICommand Commnd { get; set; }
    }
}
