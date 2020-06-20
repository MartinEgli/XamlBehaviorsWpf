// -----------------------------------------------------------------------
// <copyright file="AttachedBindingGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WPF.Extensions;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WPF.Extensions.UpdateableMarkupExtension" />
    public abstract class AttachedBindingGetterExtension<TOwner, TValue> : UpdateableMarkupExtension
        where TOwner : AttachedAncestorProperty<TOwner, TValue>
    {
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public PropertyPath Path { get; set; }

        /// <summary>
        /// Gets or sets the update source trigger.
        /// </summary>
        /// <value>
        /// The update source trigger.
        /// </value>
        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get;
            set;
        }

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValueInternal(IServiceProvider serviceProvider)
        {
            var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueTarget.TargetObject is DependencyObject dependencyObject)
            {
                return this.Create(serviceProvider, dependencyObject);
            }
            return null;
        }

        /// <summary>
        /// Creates the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        private object Create([NotNull] IServiceProvider serviceProvider,
            [NotNull] DependencyObject dependencyObject) =>
            new AttachedAncestorPropertyBindableSetter<TOwner, TValue>(dependencyObject, this.UpdateSourceTrigger, this.Path).ProvideValue(serviceProvider);
    }
}
