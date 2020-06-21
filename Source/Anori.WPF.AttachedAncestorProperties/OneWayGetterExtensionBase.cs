// -----------------------------------------------------------------------
// <copyright file="OneWayGetterExtensionBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.Windows;

    using JetBrains.Annotations;

    public abstract class OneWayGetterExtensionBase : GetterExtensionBase
    {
        private readonly DependencyProperty setterProperty;

        public OneWayGetterExtensionBase(DependencyProperty setterProperty)
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

    public abstract class OneWayGetterExtensionBase<TOwner> : OneWayGetterExtensionBase
        where TOwner : AncestorPropertyBase<TOwner>

    {
        protected OneWayGetterExtensionBase()
            : base(AncestorPropertyBase<TOwner>.SetterProperty)
        {
        }
    }

    public abstract class OneWayStringGetterExtensionBase<TOwner> : OneWayGetterExtensionBase<TOwner, string>
        where TOwner : AncestorStringPropertyBase<TOwner>

    {
        
    }


    public class OneWayStringGetterExtension : OneWayStringGetterExtensionBase<AncestorStringProperty>

    {
      
    }

    public abstract class OneWayGetterExtensionBase<TOwner, TValue> : OneWayGetterExtensionBase
        where TOwner : AncestorPropertyBase<TOwner, TValue>

    {
        protected OneWayGetterExtensionBase()
            : base(AncestorPropertyBase<TOwner, TValue>.SetterProperty)
        {
        }
    }
}
