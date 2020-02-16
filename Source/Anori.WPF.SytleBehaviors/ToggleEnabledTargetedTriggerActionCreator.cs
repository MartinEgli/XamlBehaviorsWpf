// -----------------------------------------------------------------------
// <copyright file="ToggleEnabledTargetedTriggerActionCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System.Windows;

    using Anori.WPF.Behaviors.Extensions;

    using global::Behaviors.Extensions;

    using TriggerAction = Anori.WPF.Behaviors.TriggerAction;

    /// <summary>
    ///     ToggleEnabledTargetedTriggerActionCreator
    /// </summary>
    /// <seealso cref="Anori.WPF.StyleBehaviors.ITriggerActionCreator" />
    public class ToggleEnabledTargetedTriggerActionCreator : ITriggerActionCreator
    {
        /// <summary>
        ///     Gets or sets the target object.
        /// </summary>
        /// <value>
        ///     The target object.
        /// </value>
        public object TargetObject { get; set; }

        /// <summary>
        ///     Gets or sets the name of the target element.
        /// </summary>
        /// <value>
        ///     The name of the target element.
        /// </value>
        public string TargetElementName { get; set; }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        public TriggerAction Create(DependencyObject dependencyObject)
        {
            return new ToggleEnabledTargetedTriggerAction
                   {
                       TargetName = this.TargetElementName, TargetObject = this.TargetObject
                   };
        }

        /// <summary>
        ///     Attaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        public void Attach(DependencyObject associatedObject)
        {
        }

        /// <summary>
        ///     Detaches the specified associated object.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        public void Detach(DependencyObject associatedObject)
        {
        }
    }
}
