// -----------------------------------------------------------------------
// <copyright file="GetterExtension.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    using Anori.WPF.Extensions;

    using JetBrains.Annotations;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public class GetterExtension : UpdateableMarkupExtension
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
        /// When implemented in a derived class, returns an object that is provided as the value of
        /// the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValueInternal([NotNull] IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValueTarget =
                (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if ((provideValueTarget.TargetObject is DependencyObject dependencyObject))
            {
                return Create(dependencyObject, serviceProvider);
            }

            return null;
        }

        /// <summary>
        /// Creates the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        private object Create([NotNull] DependencyObject dependencyObject, [NotNull] IServiceProvider serviceProvider)
        {
            return new AncestorPropertyUpdateableSetter(dependencyObject, this.SetterProperty, this.UpdateValue)
                .ProvideValue(serviceProvider);
        }
    }
}
