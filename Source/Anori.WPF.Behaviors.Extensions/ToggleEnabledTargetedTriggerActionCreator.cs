using System.Windows;
using Behaviors.Extensions;

namespace Anori.WPF.Behaviors.Extensions
{
    public class ToggleEnabledTargetedTriggerActionCreator : ITriggerActionCreator
    {
        public object TargetObject { get; set; }

        public string XTargetName { get; set; }

        public TriggerAction Create(DependencyObject dependencyObject)
        {
            return new ToggleEnabledTargetedTriggerAction
            {
                TargetName = this.XTargetName, TargetObject = this.TargetObject
            };
        }

        public void Attach(DependencyObject associatedObject)
        {
        }

        public void Detach(DependencyObject associatedObject)
        {
        }
    }
}
