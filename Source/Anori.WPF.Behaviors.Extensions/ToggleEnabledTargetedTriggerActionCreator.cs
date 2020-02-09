using Behaviors.Extensions;

namespace Anori.WPF.Behaviors.Extensions
{
    public class ToggleEnabledTargetedTriggerActionCreator : ITriggerActionCreator
    {
        public object TargetObject { get; set; }

        public string TargetName { get; set; }

        public TriggerAction Create()
        {
            return new ToggleEnabledTargetedTriggerAction
            {
                TargetName = this.TargetName,
                TargetObject = this.TargetObject
            };
        }
    }
}
