// -----------------------------------------------------------------------
// <copyright file="AttachedGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WPF.Extensions;
using JetBrains.Annotations;
using System;
using System.Windows;
using System.Windows.Markup;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TOwner"></typeparam>
    /// <seealso cref="System.Windows.Markup.MarkupExtension" />
    public abstract class AttachedGetterExtension<TOwner, TValue> : UpdateableMarkupExtension
        where TOwner : AttachedAncestorProperty<TOwner, TValue>
    {
        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValueInternal([NotNull] IServiceProvider serviceProvider)
        {
            var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if ((provideValueTarget.TargetObject is DependencyObject dependencyObject))
            {
                return Create(dependencyObject);
            }
            return null;
        }

        /// <summary>
        /// Creates the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        private object Create([NotNull] DependencyObject dependencyObject) =>
            new AttachedAncestorPropertyUpdateableSetter<TOwner, TValue>(dependencyObject, this.UpdateValue).ProvideValue();
    }
}
