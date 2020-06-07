// -----------------------------------------------------------------------
// <copyright file="AttachedAncestorProperty.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using Anori.WPF.Behaviors;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    public abstract class AttachedAncestorProperty<TOwner, TValue> : AttachedAncestorProperty

    {
        /// <summary>
        /// The getter property
        /// </summary>
        public static readonly DependencyProperty GetterProperty = DependencyProperty.RegisterAttached(
            "Getter",
            typeof(AttachedAncestorPropertyGetter),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new FrameworkPropertyMetadata(GetterChanged));

        /// <summary>
        ///     The setter property
        /// </summary>
        public static readonly DependencyProperty SetterProperty = DependencyProperty.RegisterAttached(
            "Setter",
            typeof(TValue),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new FrameworkPropertyMetadata(SetterChanged, CoerceValueCallback)
            {
                BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        /// <summary>
        ///     The shadowAttachedAncestorProperty property
        /// </summary>
        internal static readonly DependencyProperty ShadowAttachedAncestorPropertyProperty = DependencyProperty.RegisterAttached(
            "ShadowAttachedAncestorProperty",
            typeof(ShadowAttachedAncestorProperty),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new PropertyMetadata(default(ShadowAttachedAncestorProperty)));

        /// <summary>
        ///     Adds the shadowAttachedAncestorProperty.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void AddAttachedAncestorProperty([NotNull] FrameworkElement element, TValue value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Add Attached Ancestor Property Element {0}", (object)element?.Name);
            element.SetValue(SetterProperty, value);
            GetAncestor(element, out ShadowAttachedAncestorProperty shadowAttachedAncestorProperty);
            shadowAttachedAncestorProperty?.UpdateGetters();
        }

        /// <summary>
        /// Adds the attached ancestor property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="binding">The binding.</param>
        /// <exception cref="System.ArgumentNullException">element</exception>
        public static void AddAttachedAncestorProperty([NotNull] FrameworkElement element, BindingBase binding)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Add Attached Ancestor Property Element {0}", (object)element?.Name);
            element.SetBinding(SetterProperty, binding);
            GetAncestor(element, out ShadowAttachedAncestorProperty shadowAttachedAncestorProperty);
            shadowAttachedAncestorProperty?.UpdateGetters();
        }


        public static void AddAttachedAncestorProperty([NotNull] FrameworkElement element, PropertyPath path)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Add Attached Ancestor Property Element {0}", (object)element?.Name);
            var binding = new Binding {Path = path};
            element.SetBinding(SetterProperty, binding);
            GetAncestor(element, out ShadowAttachedAncestorProperty shadowAttachedAncestorProperty);
            shadowAttachedAncestorProperty?.UpdateGetters();
        }

        /// <summary>
        ///     Adds the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void AddValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] EventHandler valueChangedAction) =>
            target.AddValueChanged(SetterProperty, valueChangedAction);

        /// <summary>
        /// Gets the attached property shadowAttachedAncestorProperty.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="shadowAttachedAncestorProperty">The shadowAttachedAncestorProperty.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">target</exception>
        /// <exception cref="InvalidOperationException">Ancestor is null</exception>
        [CanBeNull]
        internal static DependencyObject GetAncestor(
            [NotNull] DependencyObject target,
            [CanBeNull] out ShadowAttachedAncestorProperty shadowAttachedAncestorProperty)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            DependencyObject ancestor = target.GetAncestor(
                SetterProperty, ShadowAttachedAncestorPropertyProperty, out object s, out object h);

            if (s != DependencyProperty.UnsetValue)
            {
                shadowAttachedAncestorProperty = (ShadowAttachedAncestorProperty)h;
            } else
            {
                shadowAttachedAncestorProperty = new ShadowAttachedAncestorProperty(ancestor ?? throw new InvalidOperationException("Ancestor is null"));
                SetShadowAttachedAncestorProperty(ancestor, shadowAttachedAncestorProperty);
            }

            return ancestor;
        }

        /// <summary>
        ///     Gets the getter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static AttachedAncestorPropertyGetter GetGetter(
            [NotNull] DependencyObject dependencyObject) =>
            (AttachedAncestorPropertyGetter)dependencyObject.GetValue(GetterProperty);

        /// <summary>
        ///     Getter of <see cref="DependencyProperty" /> default assembly.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to get the default assembly from.</param>
        /// <returns>The default assembly.</returns>
        public static TValue GetSetter(DependencyObject dependencyObject) =>
            dependencyObject.GetValueSync<TValue>(SetterProperty);

        /// <summary>
        ///     Gets the value or register parent changed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="sourceChanged">The parent changed.</param>
        /// <returns></returns>
        public static TValue GetValueOrRegisterParentChanged(DependencyObject target, EventHandler sourceChanged)
        {
            TValue value = target.GetValueOrRegisterParentNotifier<TValue>(
                SetterProperty,
                ParentChangedAction,
                sourceChanged,
                new ParentNotifiers());

            return value;
        }

        /// <summary>
        /// Removes the shadowAttachedAncestorProperty.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public static void RemoveAttachedAncestorProperty([NotNull] FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Remove AttachedAncestorProperty Element {0}", (object)element?.Name);
            ShadowAttachedAncestorProperty shadowAttachedAncestorProperty = GetShadowAttachedAncestorProperty(element);
            if (shadowAttachedAncestorProperty != null)
            {
                shadowAttachedAncestorProperty.UnsubscribeGetters();
                element.ClearValue(SetterProperty);
                element.ClearValue(ShadowAttachedAncestorPropertyProperty);
                shadowAttachedAncestorProperty.UpdateGetters();
            } else
            {
                element.ClearValue(SetterProperty);
            }
        }

        /// <summary>
        ///     Removes the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void RemoveValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] EventHandler valueChangedAction) =>
            target.RemoveValueChanged(SetterProperty, valueChangedAction);

        /// <summary>
        ///     Sets the getter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetGetter(DependencyObject dependencyObject, AttachedAncestorPropertyGetter value) => dependencyObject.SetValue(GetterProperty, value);

        /// <summary>
        ///     AttachedAncestorPropertyBindableSetter of <see cref="DependencyProperty" /> default assembly.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to set the default assembly to.</param>
        /// <param name="value">The assembly.</param>
        public static void SetSetter(DependencyObject dependencyObject, TValue value) => dependencyObject.SetValueSync(SetterProperty, value);

        /// <summary>
        /// Gets the shadow attached ancestor property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal static ShadowAttachedAncestorProperty GetShadowAttachedAncestorProperty(DependencyObject element) => (ShadowAttachedAncestorProperty)element.GetValue(ShadowAttachedAncestorPropertyProperty);

        /// <summary>
        /// Gets the or create host.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">element</exception>
        [NotNull]
        internal static ShadowAttachedAncestorProperty GetOrCreateShadowAttachedAncestorProperty([NotNull] DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var shadowAttachedAncestorProperty = GetShadowAttachedAncestorProperty(element);
            if (shadowAttachedAncestorProperty != null &&
                shadowAttachedAncestorProperty != DependencyProperty.UnsetValue)
            {
                return shadowAttachedAncestorProperty;
            }

            shadowAttachedAncestorProperty = new ShadowAttachedAncestorProperty(element);
            SetShadowAttachedAncestorProperty(element, shadowAttachedAncestorProperty);

            return shadowAttachedAncestorProperty;
        }

        /// <summary>
        ///     Sets the shadowAttachedAncestorProperty.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        internal static void SetShadowAttachedAncestorProperty(DependencyObject element, ShadowAttachedAncestorProperty value) => element.SetValue(ShadowAttachedAncestorPropertyProperty, value);

        /// <summary>
        ///     Coerces the value callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns></returns>
        private static object CoerceValueCallback(DependencyObject dependencyObject, object baseValue) => baseValue;

        /// <summary>
        ///     Getters the changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private static void GetterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            //DependencyObject hostObject = GetAncestor(dependencyObject);
            //if (hostObject != null)
            //{
            //    // BindingOperations.SetBinding(hostObject, setterProperty , ((AttachedAncestorPropertyGetter)e.NewValue).Binding);
            //    //   EventHandler ValueChangedHandler= (sender, args) =>
            //    //   {
            //    ////       SetGetter(((DependencyObject)dependencyObject), ((DependencyObject)hostObject).GetValueSync<T>(setterProperty));
            //    //   };
            //    //   AddValueChangedHandler(hostObject, ValueChangedHandler);

            //    //AddValueChanged(hostObject,
            //    //    obj => SetGetter(dependencyObject, obj));
            //}

            AttachedAncestorPropertyGetter oldContent = (AttachedAncestorPropertyGetter)args.OldValue;
            AttachedAncestorPropertyGetter newContent = (AttachedAncestorPropertyGetter)args.NewValue;

            if (oldContent == newContent)
            {
                return;
            }

            if (((IAttachedObject)oldContent)?.AssociatedObject != null)
            {
                oldContent.Detach();
            }

            if (newContent == null || dependencyObject == null)
            {
                return;
            }

            if (((IAttachedObject)newContent).AssociatedObject != null)
            {
                throw new InvalidOperationException();
            }

            newContent.Attach(dependencyObject);
        }
        
        /// <summary>
        ///     Parents the changed action.
        /// </summary>
        /// <param name="obj">The object.</param>
        private static void ParentChangedAction(DependencyObject obj)
        {
        }

        /// <summary>
        ///     Setters the changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void SetterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            //InitHost(obj);
            //GetShadowAttachedAncestorProperty(obj).Value = (T)e.NewValue;
        }
       
    }
}
