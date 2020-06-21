// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System.Windows;
    using System.Windows.Data;

    using Anori.WPF.Extensions;

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
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });


        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static TValue GetSetter(DependencyObject dependencyObject) =>
            dependencyObject.GetValueSync<TValue>(SetterProperty);



        /// <summary>
        /// Sets the setter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetSetter(DependencyObject dependencyObject, TValue value) =>
            dependencyObject.SetValueSync(SetterProperty, value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSelf">The type of the owner.</typeparam>
    public abstract class AncestorPropertyBase<TSelf> : AncestorPropertyBase<TSelf, object>
        where TSelf : AncestorPropertyBase<TSelf, object>
    {
    }

    public abstract class AncestorStringPropertyBase<TSelf> : AncestorPropertyBase<TSelf, string>
        where TSelf : AncestorPropertyBase<TSelf, string>
    {
    }


    public class AncestorStringProperty : AncestorStringPropertyBase<AncestorStringProperty>
    {
    }
}
