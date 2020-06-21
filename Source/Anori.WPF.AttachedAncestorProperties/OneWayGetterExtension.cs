// -----------------------------------------------------------------------
// <copyright file="OneWayGetterExtension.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using JetBrains.Annotations;

    using System;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public class OneWayGetterExtension : GetterExtensionBase
    {
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
        ///     Creates the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        protected override object Create(
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] DependencyObject dependencyObject,
            [NotNull]  DependencyProperty setterProperty) =>
            new AncestorPropertyOneWaySetter(dependencyObject, setterProperty, this.UpdateValue).ProvideValue(
                serviceProvider);

        /// <summary>
        ///     Gets the setter property.
        /// </summary>
        /// <returns></returns>
        protected override DependencyProperty GetSetterProperty() => SetterProperty;
        
    }
}
