using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    public class InvokeCommandActionCreator : ITriggerActionCreator
    {
        private InvokeCommandAction action;

        private Action unregisterAction;
        public BindingBase CommandParameterBinding { get; set; }

        public object CommandParameter { get; set; }

        public BindingBase CommandBinding { get; set; }

        public ICommand Command { get; set; }

        public TriggerAction Create()
        {
            return action = new InvokeCommandAction();
        }

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject is FrameworkElement frameworkElement)
            {
                Unregister();
                frameworkElement.DataContextChanged += OnDataContextChanged;
                this.unregisterAction = () => frameworkElement.DataContextChanged -= OnDataContextChanged;
            }
        }

        public void Detach(DependencyObject associatedObject)
        {
            throw new NotImplementedException();
        }

        public void Unregister()
        {
            if (this.unregisterAction == null)
            {
                return;
            }

            this.unregisterAction();
            this.unregisterAction = null;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
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
    }
}
