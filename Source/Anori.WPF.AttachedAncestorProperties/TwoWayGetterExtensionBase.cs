// -----------------------------------------------------------------------
// <copyright file="TwoWayGetterExtensionBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    using JetBrains.Annotations;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public abstract class TwoWayGetterExtensionBase : GetterExtensionBase
    {
        /// <summary>
        /// The setter property
        /// </summary>
        private readonly DependencyProperty setterProperty;

        protected override DependencyProperty GetSetterProperty()
        {
            return this.setterProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayGetterExtensionBase"/> class.
        /// </summary>
        /// <param name="setterProperty">The setter property.</param>
        /// <exception cref="ArgumentNullException">setterProperty</exception>
        protected TwoWayGetterExtensionBase([NotNull] DependencyProperty setterProperty) =>
            this.setterProperty = setterProperty ?? throw new ArgumentNullException(nameof(setterProperty));

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public PropertyPath Path { get; set; }

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
        ///     Creates the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        protected override object Create(
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] DependencyObject dependencyObject,
            DependencyProperty setterProperty) =>
            new AncestorPropertyTwoWaySetter(dependencyObject, this.UpdateSourceTrigger, setterProperty, this.Path)
                .ProvideValue(serviceProvider);
    }
}
