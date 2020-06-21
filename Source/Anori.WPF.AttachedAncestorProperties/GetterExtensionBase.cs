// -----------------------------------------------------------------------
// <copyright file="GetterExtensionBase.cs" company="Anori Soft"
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

    public abstract class GetterExtensionBase : UpdateableMarkupExtension
    {
        /// <summary>
        /// Gets the setter property.
        /// </summary>
        /// <returns></returns>
        protected abstract DependencyProperty GetSetterProperty();

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of
        ///     the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">
        ///     A service provider helper that can provide services for the markup extension.
        /// </param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        protected sealed override object ProvideValueInternal([NotNull] IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValueTarget =
                (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueTarget.TargetObject is DependencyObject dependencyObject)
            {
                return this.Create(serviceProvider, dependencyObject, this.GetSetterProperty());
            }

            return null;
        }

        protected abstract object Create(
            [NotNull] IServiceProvider serviceProvider,
            [NotNull] DependencyObject dependencyObject,
            [NotNull]  DependencyProperty setterProperty);
    }
}
