using System.Windows;
using Behaviors.Extensions;

namespace Anori.WPF.Behaviors.Extensions
{
    public class ToggleEnabledTargetedTriggerActionCreator : ITriggerActionCreator
    {
        public object TargetObject { get; set; }

        public string XTargetName { get; set; }

        public TriggerAction Create()
        {
            return new ToggleEnabledTargetedTriggerAction
            {
                TargetName = this.XTargetName,
                TargetObject = this.TargetObject
            };
        }

        public void Attach(DependencyObject obj)
        {
        }

        public void Detach(DependencyObject obj)
        {
        }
    }
}
