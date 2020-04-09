// -----------------------------------------------------------------------
// <copyright file="AttachedFork.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
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


        public static void RemoveHost([NotNull] FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Remove Host Element {0}", (object)((FrameworkElement)element)?.Name);
            var host = GetHost(element);
            if (host != null)
            {
                host.UnsubscribeGetters();
                element.ClearValue(AttachedFork<T, TOwner>.SetterProperty);
                element.ClearValue(AttachedFork<T, TOwner>.HostProperty);
                host.UpdateGetters();
            } else
            {
                element.ClearValue(AttachedFork<T, TOwner>.SetterProperty);
            }
        }

        public static void AddHost(FrameworkElement element, T value)
        {
            Debug.WriteLine("Add Host Element {0}", (object)((FrameworkElement)element)?.Name);
            element.SetValue(AttachedFork<T, TOwner>.SetterProperty, value);
            var hostObject = GetAttachedHostObject(element, out var host);
            //var host = GetHost(hostObject);
            host.UpdateGetters();
        }

        /// <summary>
        /// Getters the changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void GetterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject hostObject = AttachedFork<T, TOwner>.GetAttachedHostObject(dependencyObject, out _);
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
            new FrameworkPropertyMetadata( SetterChanged, CoerceValueCallback)
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

       

        /// <summary>
        /// Coerces the value callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns></returns>
        private static object CoerceValueCallback(DependencyObject dependencyObject, object baseValue)
        {
            var host = dependencyObject.GetValue(HostProperty);
            if (host == DependencyProperty.UnsetValue)
            {
                var h = new Host<T>(dependencyObject);
                dependencyObject.SetValue(HostProperty, h);
            }

            return baseValue;
        }

        private static void InitHost(DependencyObject dependencyObject)
        {
            var host = dependencyObject.GetValue(HostProperty);
            if (host == null)
            {
                var h = new Host<T>(dependencyObject);
                //h.ValueChanged += (s, v) =>
                //{
                //    Debug.WriteLine("Value Changing {0}", (object)((FrameworkElement)dependencyObject)?.Name);
                //    dependencyObject.SetValue(SetterProperty, v);
                //};
                dependencyObject.SetValue(HostProperty, h);
            }
        }


        /// <summary>
        /// The host property
        /// </summary>
        internal static readonly DependencyProperty HostProperty = DependencyProperty.RegisterAttached(
            "Host", typeof(Host<T>), typeof(AttachedFork<T, TOwner>), new PropertyMetadata(default(Host<T>), HostChanged));

        private static void HostChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
           // if (e.OldValue is Host<T> oldHost)
           // {
           //     oldHost.UpdateGetters();
           // }

           // if (e.NewValue != null)
           //{
           //    //    newHost.UpdateGetters();
           //    //}

           //    var hostObject = GetAttachedHostObject(dependencyObject);

           //    if (hostObject != null)
           //    {
           //        Debug.WriteLine("Upper Host {0} of {1}", ((FrameworkElement)hostObject)?.Name,
           //            ((FrameworkElement)dependencyObject)?.Name);
           //        var host = GetHost(hostObject);
           //        host?.UpdateGetters();
           //    }
           //}
        }

        /// <summary>
        /// Sets the host.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        internal static void SetHost(DependencyObject element, Host<T> value)
        {
            element.SetValue(HostProperty, value);
        }

        internal static Host<T> GetHost(DependencyObject element)
        {
            return (Host<T>)element.GetValue(HostProperty);
        }

        [NotNull]
        internal static Host<T> GetOrCreateHost([NotNull] DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var host = AttachedFork<T, TOwner>.GetHost(element);
            if (host == null || host == DependencyProperty.UnsetValue)
            {
                host = new Host<T>(element);
                AttachedFork<T, TOwner>.SetHost(element, host);
            }

            return host;
        }

        /// <summary>
        ///     Setters the changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void SetterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //InitHost(obj);
            //GetHost(obj).Value = (T)e.NewValue;
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
        public static DependencyObject GetAttachedHostObject([NotNull] DependencyObject target, out Host<T> host)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var hostObject = target.GetAttachedHostObject(
                SetterProperty, HostProperty, out var s, out var h);
           
            if (s != DependencyProperty.UnsetValue)
            {
                host = (Host<T>)h;
            } else
            {
                host = new Host<T>(hostObject ?? throw new InvalidOperationException("HostObject is null"));
                SetHost(hostObject, host);
            }
            return hostObject;
        }


        public static DependencyObject GetAttachedHostProperty([NotNull] DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var parentDependencyObject = target.GetAttachedProperty(
                HostProperty);

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
