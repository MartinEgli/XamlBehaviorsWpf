// -----------------------------------------------------------------------
// <copyright file="BindAllChildrenBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Behaviors
{
    using System.Windows;

    using Bfa.Common.WPF.Extensions;

    /// <summary>
    ///     The BindAllChildrenBehavior class.
    /// </summary>
    /// <seealso cref="Microsoft.Xaml.Behaviors.Behavior{T}" />
    public class BindAllChildrenBehavior : Microsoft.Xaml.Behaviors.Behavior<FrameworkElement>
    {
        /// <summary>
        ///     Gets or sets the name of the element.
        /// </summary>
        /// <value>
        ///     The name of the element.
        /// </value>
        public string ElementName { get; set; }

        /// <summary>
        ///     Gets or sets the dependency property.
        /// </summary>
        /// <value>
        ///     The dependency property.
        /// </value>
        public string DependencyPropertyName { get; set; }

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            if (!string.IsNullOrWhiteSpace(this.DependencyPropertyName) && !string.IsNullOrWhiteSpace(this.ElementName))
            {
                var dependencyObject = this.AssociatedObject.FindDependencyObjectByName(this.ElementName);
                if (dependencyObject != null)
                {
                    var dependencyProperty = dependencyObject.GetDependencyPropertyByName(this.DependencyPropertyName);
                    if (dependencyProperty != null)
                    {
                        this.AssociatedObject.SetChildren(dependencyObject, dependencyProperty);
                    }
                }
            }

            base.OnAttached();
        }
    }
}