// -----------------------------------------------------------------------
// <copyright file="TwoWayGetterExtension.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public class TwoWayGetterExtension : GetterExtensionBase
    {
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public PropertyPath Path { get; set; }

        /// <summary>
        ///     Gets or sets the setter property.
        /// </summary>
        /// <value>The setter property.</value>
        public DependencyProperty SetterProperty
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the update source trigger.
        /// </summary>
        /// <value>The update source trigger.</value>
        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the setter property.
        /// </summary>
        /// <returns></returns>
        protected override DependencyProperty GetSetterProperty() => SetterProperty;

        /// <summary>
        ///     Creates the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        protected override object
            Create([NotNull] IServiceProvider serviceProvider, [NotNull] DependencyObject dependencyObject, DependencyProperty setterProperty) =>
            new AncestorPropertyTwoWaySetter(dependencyObject, this.UpdateSourceTrigger, setterProperty, this.Path)
                .ProvideValue(serviceProvider);
    }
}
