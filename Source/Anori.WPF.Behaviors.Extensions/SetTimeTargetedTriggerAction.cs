namespace Anori.WPF.Behaviors.Extensions
{
    using System;
    using System.Windows.Controls;

    using Anori.WPF.Behaviors;

    /// <summary>
    /// SetTimeTargetedTriggerAction Class
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.TargetedTriggerAction{System.Windows.Controls.TextBox}" />
    public class SetTimeTargetedTriggerAction : TargetedTriggerAction<TextBox>
    {
        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            if (this.Target != null)
            {
                this.Target.Text = DateTime.Now.ToLongTimeString();
            }
        }
    }
}
