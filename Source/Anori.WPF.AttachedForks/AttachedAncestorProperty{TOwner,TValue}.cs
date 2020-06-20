// -----------------------------------------------------------------------
// <copyright file="AttachedAncestorProperty{TOwner,TValue}.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using Anori.WPF.Behaviors;
    using Anori.WPF.Extensions;

    using JetBrains.Annotations;

    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    public abstract class AttachedAncestorProperty<TOwner, TValue> : AttachedAncestorProperty

    {
        /// <summary>
        ///     The getter property
        /// </summary>
        public static readonly DependencyProperty GetterProperty = DependencyProperty.RegisterAttached(
            "Getter",
            typeof(AttachedAncestorPropertyGetterBase),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new PropertyMetadata(OnGetterChanged));

        /// <summary>
        ///     The setter property
        /// </summary>
        public static readonly DependencyProperty SetterProperty = DependencyProperty.RegisterAttached(
            "Setter",
            typeof(TValue),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        /// <summary>
        ///     The endPointUpdater property
        /// </summary>
        internal static readonly DependencyProperty ShadowEndPointUpdaterProperty = DependencyProperty.RegisterAttached(
            "ShadowEndPointUpdater",
            typeof(EndPointUpdater),
            typeof(AttachedAncestorProperty<TOwner, TValue>),
            new PropertyMetadata(default(EndPointUpdater)));

        /// <summary>
        ///     Adds the endPointUpdater.
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
            GetAncestor(element, out EndPointUpdater shadowAttachedAncestorProperty);
            shadowAttachedAncestorProperty?.UpdateGetters();
        }

        /// <summary>
        ///     Adds the attached ancestor property.
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
            GetAncestor(element, out EndPointUpdater shadowAttachedAncestorProperty);
            shadowAttachedAncestorProperty?.UpdateGetters();
        }

        /// <summary>
        ///     Adds the attached ancestor property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="path">The path.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public static void AddAttachedAncestorProperty([NotNull] FrameworkElement element, PropertyPath path)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Add Attached Ancestor Property Element {0}", (object)element?.Name);
            Binding binding = new Binding { Path = path };
            element.SetBinding(SetterProperty, binding);
            GetAncestor(element, out EndPointUpdater shadowAttachedAncestorProperty);
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
        ///     Gets the getter.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static AttachedAncestorPropertyGetterBase GetGetter([NotNull] DependencyObject dependencyObject) =>
            (AttachedAncestorPropertyGetterBase)dependencyObject.GetValue(GetterProperty);

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
        public static TValue GetValueOrRegisterParentChanged(DependencyObject target, EventHandler sourceChanged) =>
            target.GetValueOrRegisterParentNotifier<TValue>(
                SetterProperty,
                ParentChangedAction,
                sourceChanged,
                new ParentNotifiers());

        /// <summary>
        ///     Removes the endPointUpdater.
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
            EndPointUpdater shadowAttachedAncestorProperty = GetShadowEndPointUpdater(element);
            if (shadowAttachedAncestorProperty != null)
            {
                shadowAttachedAncestorProperty.UnsubscribeGetters();
                element.ClearValue(SetterProperty);
                element.ClearValue(ShadowEndPointUpdaterProperty);
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
        public static void SetGetter(
            [NotNull] DependencyObject dependencyObject,
            [NotNull] AttachedAncestorPropertyGetterBase value) =>
            dependencyObject.SetValue(GetterProperty, value);

        /// <summary>
        ///     AttachedAncestorPropertyBindableSetter of <see cref="DependencyProperty" /> default assembly.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to set the default assembly to.</param>
        /// <param name="value">The assembly.</param>
        public static void SetSetter(DependencyObject dependencyObject, TValue value) =>
            dependencyObject.SetValueSync(SetterProperty, value);

        /// <summary>
        ///     Gets the attached property endPointUpdater.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="endPointUpdater">The endPointUpdater.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">target</exception>
        /// <exception cref="InvalidOperationException">Ancestor is null</exception>
        [CanBeNull]
        internal static DependencyObject GetAncestor(
            [NotNull] DependencyObject target,
            [CanBeNull] out EndPointUpdater endPointUpdater)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            DependencyObject ancestor = target.GetAncestor(
                SetterProperty,
                ShadowEndPointUpdaterProperty,
                out object s,
                out object h);

            if (s != DependencyProperty.UnsetValue)
            {
                endPointUpdater = (EndPointUpdater)h;
            } else
            {
                endPointUpdater = new EndPointUpdater(
                    ancestor ?? throw new InvalidOperationException("Ancestor is null"));
                SetShadowEndPointUpdater(ancestor, endPointUpdater);
            }

            return ancestor;
        }

        /// <summary>
        ///     Gets the or create host.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">element</exception>
        [NotNull]
        internal static EndPointUpdater GetOrCreateEndPointUpdater([NotNull] DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            EndPointUpdater shadowAttachedAncestorProperty = GetShadowEndPointUpdater(element);
            if (shadowAttachedAncestorProperty != null
                && shadowAttachedAncestorProperty != DependencyProperty.UnsetValue)
            {
                return shadowAttachedAncestorProperty;
            }

            shadowAttachedAncestorProperty = new EndPointUpdater(element);
            SetShadowEndPointUpdater(element, shadowAttachedAncestorProperty);

            return shadowAttachedAncestorProperty;
        }

        /// <summary>
        ///     Gets the shadow attached ancestor property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal static EndPointUpdater GetShadowEndPointUpdater(DependencyObject element) =>
            (EndPointUpdater)element.GetValue(ShadowEndPointUpdaterProperty);

        /// <summary>
        ///     Sets the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        internal static void SetShadowEndPointUpdater(DependencyObject element, EndPointUpdater value) =>
            element.SetValue(ShadowEndPointUpdaterProperty, value);

        /// <summary>
        ///     Getters the changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        /// <exception cref="InvalidOperationException"></exception>
        private static void OnGetterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            Getter oldContent = (Getter)args.OldValue;
            Getter newContent = (Getter)args.NewValue;

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

            newContent.Type = typeof(TOwner);
            newContent.Attach(dependencyObject);
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
