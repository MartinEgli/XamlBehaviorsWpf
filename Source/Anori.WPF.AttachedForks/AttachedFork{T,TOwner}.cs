// -----------------------------------------------------------------------
// <copyright file="AttachedFork.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// Attached Fork
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    public abstract class AttachedFork<T, TOwner> : DependencyObject
        where TOwner : AttachedFork<T, TOwner>

    {
        /// <summary>
        /// The getter property
        /// </summary>
        public static readonly DependencyProperty GetterProperty = DependencyProperty.RegisterAttached(
            "Getter",
            typeof(T),
            typeof(AttachedFork<T, TOwner>),
            new PropertyMetadata(GetterChanged));

        /// <summary>
        /// Getters the changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void GetterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject hostObject = AttachedFork<T, TOwner>.GetAttachedPropertyHost(dependencyObject);
            if (hostObject != null)
            {
                AddValueChangedHandler(hostObject, ValueChangedHandler);

                //AddValueChanged(hostObject,
                //    obj => SetGetter(dependencyObject, obj));
            }
        }


        private static void ValueChangedHandler(object sender, EventArgs e)
        {
            SetGetter(((DependencyObject)sender), ((DependencyObject)sender).GetValueSync<T>(SetterProperty));
        }



        /// <summary>
        /// Sets the getter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetGetter(DependencyObject dependencyObject, T value) => dependencyObject.SetValue(GetterProperty, value);

        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static T GetGetter(DependencyObject dependencyObject) => (T)dependencyObject.GetValue(GetterProperty);

        /// <summary>
        ///     The setter property
        /// </summary>
        public static readonly DependencyProperty SetterProperty = DependencyProperty.RegisterAttached(
            "Setter",
            typeof(T),
            typeof(AttachedFork<T, TOwner>),
            new FrameworkPropertyMetadata(SetterChanged)
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        /// <summary>
        ///     Setters the changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void SetterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var host = GetAttachedPropertyHost(obj);
            if (host != null)
            {
               var temp = host.GetValue(SetterProperty);
               host.SetValue(SetterProperty, null);
               host.SetValue(SetterProperty, temp);


            }
            //Instance.FallbackAssembly = e.NewValue?.ToString();
            //Instance.OnProviderChanged(dependencyObject);
            //          SetSetter(dependencyObject, (T)e.NewValue);
            //SetGetter((T)e.NewValue);
        }

        /// <summary>
        ///     Getter of <see cref="DependencyProperty" /> default assembly.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to get the default assembly from.</param>
        /// <returns>The default assembly.</returns>
        public static T GetSetter(DependencyObject dependencyObject) => dependencyObject.GetValueSync<T>(SetterProperty);

        /// <summary>
        ///     Setter of <see cref="DependencyProperty" /> default assembly.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to set the default assembly to.</param>
        /// <param name="value">The assembly.</param>
        public static void SetSetter(DependencyObject dependencyObject, T value) => dependencyObject.SetValueSync(SetterProperty, value);
        //    public static void SetSetter(DependencyObject dependencyObject, T value) => dependencyObject.SetValueSync(SetterProperty, value);

        ///// <summary>
        /////     Get the assembly from the context, if possible.
        ///// </summary>
        ///// <param name="target">The target object.</param>
        ///// <param name="sourceChanged">The parent changed.</param>
        ///// <param name="fallbackValue">The fallback value.</param>
        ///// <returns>
        /////     The assembly name, if available.
        ///// </returns>
        //public static T GetValueOrRegisterParentChanged(
        //    DependencyObject target,
        //    Action<T> sourceChanged,
        //    T fallbackValue)
        //{
        //    if (target == null)
        //    {
        //        return fallbackValue;
        //    }

        //    var value = GetValueOrRegisterParentChanged(target, sourceChanged);
        //    if (value == null)
        //    {
        //        return fallbackValue;
        //    }

        //    return value;
        //}

        /// <summary>
        ///     Gets the value or register parent changed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="sourceChanged">The parent changed.</param>
        /// <returns></returns>
        public static T GetValueOrRegisterParentChanged(DependencyObject target, EventHandler sourceChanged)
        {
            var value = target.GetValueOrRegisterParentNotifier<T>(
                SetterProperty,
                ParentChangedAction,
                sourceChanged,
                new ParentNotifiers());

            return value;
        }

        ///// <summary>
        ///// Gets the parent object.
        ///// </summary>
        ///// <param name="target">The target.</param>
        ///// <param name="sourceChanged">The source changed.</param>
        ///// <returns></returns>
        //public static object GetParentObject(DependencyObject target, Action<T> sourceChanged)
        //{
        //    var value = default(T);
        //    var parentDependencyObject = target.GetAttachedPropertyObject(
        //        SetterProperty,
        //        ParentChangedAction,
        //        sourceChanged,
        //        new ParentNotifiers());

        //    if (parentDependencyObject != null)
        //    {
        //        value = parentDependencyObject.GetValueSync<T>(SetterProperty);
        //    }

        //    return value;
        //}

        ///// <summary>
        ///// Gets the dependency object.
        ///// </summary>
        ///// <param name="target">The target.</param>
        ///// <param name="sourceChanged">The source changed.</param>
        ///// <returns></returns>
        //public static DependencyObject GetDependencyObject(DependencyObject target, Action<T> sourceChanged)
        //{
        //    var parentDependencyObject = target.GetAttachedPropertyObject(
        //        SetterProperty,
        //        ParentChangedAction,
        //        sourceChanged,
        //        new ParentNotifiers());

        //    return parentDependencyObject;
        //}

        /// <summary>
        /// Gets the dependency object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        [CanBeNull]
        public static object GetAttachedPropertyObject([NotNull] DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var parentDependencyObject = target.GetAttachedPropertyObject(
                SetterProperty);

            return parentDependencyObject;
        }

        /// <summary>
        /// Gets the attached property host.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">target</exception>
        [CanBeNull]
        public static DependencyObject GetAttachedPropertyHost([NotNull] DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var parentDependencyObject = target.GetAttachedPropertyHost(
                SetterProperty);

            return parentDependencyObject;
        }

        ///// <summary>
        ///// Adds the value changed action.
        ///// </summary>
        ///// <param name="target">The target.</param>
        ///// <param name="valueChangedAction">The value changed action.</param>
        //public static void AddValueChangedAction(
        //    [NotNull] DependencyObject target,
        //    [NotNull] Action<T> valueChangedAction)
        //{
        //    target.AddValueChangedAction(SetterProperty, valueChangedAction);
        //}


        /// <summary>
        /// Adds the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void AddValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] EventHandler valueChangedAction)
        {
            target.AddValueChanged(SetterProperty, valueChangedAction);
        }


        /// <summary>
        /// Removes the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void RemoveValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] EventHandler valueChangedAction)
        {
            target.RemoveValueChanged(SetterProperty, valueChangedAction);
        }

        /// <summary>
        ///     Parents the changed action.
        /// </summary>
        /// <param name="obj">The object.</param>
        private static void ParentChangedAction(DependencyObject obj)
        {
        }
    }

    
}
