// -----------------------------------------------------------------------
// <copyright file="ConditionBehavior.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Core
{
    using System.Windows.Markup;

    /// <summary>
    ///     A behavior that attaches to a trigger and controls the conditions
    ///     to fire the actions.
    /// </summary>
    [ContentProperty("Condition")]
    public class ConditionBehavior : Behavior<TriggerBase>
    {
        /// <summary>
        /// The condition property
        /// </summary>
        public static readonly System.Windows.DependencyProperty ConditionProperty =
            System.Windows.DependencyProperty.Register(
                "Condition",
                typeof(ICondition),
                typeof(ConditionBehavior),
                new System.Windows.PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the IConditon object on behavior.
        /// </summary>
        /// <value>
        ///     The name of the condition to change.
        /// </value>
        public ICondition Condition
        {
            get { return (ICondition)this.GetValue(ConditionProperty); }
            set { this.SetValue(ConditionProperty, value); }
        }

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewInvoke += this.OnPreviewInvoke;
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewInvoke -= this.OnPreviewInvoke;
            base.OnDetaching();
        }

        /// <summary>
        ///     The event handler that is listening to the preview invoke event that is fired by
        ///     the trigger. Setting PreviewInvokeEventArgs.Cancelling to True will
        ///     cancel the invocation.
        /// </summary>
        /// <param name="sender">The trigger base object.</param>
        /// <param name="e">An object of type PreviewInvokeEventArgs where e.Cancelling can be set to True.</param>
        private void OnPreviewInvoke(object sender, PreviewInvokeEventArgs e)
        {
            if (this.Condition != null)
            {
                e.Cancelling = !this.Condition.Evaluate();
            }
        }
    }
}
