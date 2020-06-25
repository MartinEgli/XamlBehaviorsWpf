// -----------------------------------------------------------------------
// <copyright file="OneWayGetterExtensionBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using JetBrains.Annotations;

    using System;
    using System.Windows;

    public abstract class OneWayGetterExtensionBase : GetterExtensionBase
    {
        /// <summary>
        /// The setter property
        /// </summary>
        private readonly DependencyProperty setterProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayGetterExtensionBase"/> class.
        /// </summary>
        /// <param name="setterProperty">The setter property.</param>
        protected OneWayGetterExtensionBase(DependencyProperty setterProperty)
        {
            this.setterProperty = setterProperty;
        }

        /// <summary>
        /// Creates the specified dependency object.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <returns></returns>
        protected override object Create(
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] DependencyObject dependencyObject,
            [NotNull] DependencyProperty setterProperty) =>
            new AncestorPropertyOneWaySetter(dependencyObject, setterProperty, this.UpdateValue).ProvideValue(
                serviceProvider);

        /// <summary>
        ///     Gets the setter property.
        /// </summary>
        /// <returns></returns>
        protected override DependencyProperty GetSetterProperty() => this.setterProperty;
    }
}
