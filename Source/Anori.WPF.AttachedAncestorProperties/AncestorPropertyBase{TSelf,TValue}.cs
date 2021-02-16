// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyBase{TSelf,TValue}.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using Anori.WPF.Extensions;

    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// </summary>
    /// <typeparam name="TSelf">The type of the self.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    public abstract class AncestorPropertyBase<TSelf, TValue> : DependencyObject
        where TSelf : AncestorPropertyBase<TSelf, TValue>
    {
        /// <summary>
        ///     The setter property
        /// </summary>
        public static readonly DependencyProperty SetterProperty = DependencyProperty.RegisterAttached(
            "Setter",
            typeof(TValue),
            typeof(AncestorPropertyBase<TSelf, TValue>),
            new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        /// <summary>
        ///     Gets the setter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static TValue GetSetter(DependencyObject dependencyObject) =>
            dependencyObject.GetValueSync<TValue>(SetterProperty);

        /// <summary>
        ///     Sets the setter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetSetter(DependencyObject dependencyObject, TValue value) =>
            dependencyObject.SetValueSync(SetterProperty, value);
    }
}
