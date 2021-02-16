// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyHelperBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using Anori.WPF.Behaviors;
    using Anori.WPF.Extensions;

    using JetBrains.Annotations;

    public abstract class AncestorPropertyHelperBase<TValue> : DependencyObject

    {
        /// <summary>
        ///     The endPointUpdater property
        /// </summary>
        internal static readonly DependencyProperty ShadowEndPointUpdatersProperty =
            DependencyProperty.RegisterAttached(
                "ShadowEndPointUpdaters",
                typeof(EndPointUpdaters),
                typeof(AncestorPropertyHelper),
                new PropertyMetadata(default(EndPointUpdater)));

        /// <summary>
        ///     Adds the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public static void AddAncestorProperty(
            [NotNull] FrameworkElement element,
            TValue value,
            DependencyProperty setterProperty)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Add Attached Ancestor Property Element {0}", (object)element?.Name);
            element.SetValue(setterProperty, value);
            GetAncestor(element, setterProperty, out EndPointUpdater updater);
            updater?.UpdateGetters();
        }

        /// <summary>
        ///     Removes the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public static void RemoveAncestorProperty([NotNull] FrameworkElement element, DependencyProperty setterProperty)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Remove AttachedAncestorProperty Element {0}", (object)element?.Name);
            EndPointUpdaters updaters = EndPointUpdaters.GetOrCreate(element, ShadowEndPointUpdatersProperty);
            EndPointUpdater endPointUpdater = updaters.GetItem(setterProperty);
            if (endPointUpdater != null)
            {
                endPointUpdater.UnsubscribeGetters();
                element.ClearValue(setterProperty);
                updaters.Remove(setterProperty);
                endPointUpdater.UpdateGetters();
            } else
            {
                element.ClearValue(setterProperty);
            }
        }

        /// <summary>
        ///     Adds the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        internal static void AddValueChangedHandler(
            [NotNull] DependencyObject target,
            DependencyProperty setterProperty,
            [NotNull] EventHandler valueChangedAction) =>
            target.AddValueChanged(setterProperty, valueChangedAction);

        /// <summary>
        ///     Gets the attached property endPointUpdater.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="endPointUpdater">The endPointUpdater.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">target</exception>
        /// <exception cref="InvalidOperationException">Ancestor is null</exception>
        [CanBeNull]
        internal static DependencyObject GetAncestor(
            [NotNull] DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [CanBeNull] out EndPointUpdater endPointUpdater)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            DependencyObject ancestor = target.GetAncestor(
                setterProperty,
                ShadowEndPointUpdatersProperty,
                out object setter,
                out object shadow);

            EndPointUpdaters updaters = null;
            if (setter != DependencyProperty.UnsetValue)
            {
                updaters = (EndPointUpdaters)shadow;
            }

            if (updaters == null)
            {
                updaters = EndPointUpdaters.GetOrCreate(ancestor, ShadowEndPointUpdatersProperty);
            }

            endPointUpdater = updaters.GetOrCreateItem(setterProperty);
            return ancestor;
        }

        /// <summary>
        ///     Gets the or create end point updater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <returns></returns>
        internal static EndPointUpdater GetOrCreateEndPointUpdater(
            [NotNull] DependencyObject element,
            [NotNull] DependencyProperty setterProperty)
        {
            return EndPointUpdaters.GetOrCreate(element, ShadowEndPointUpdatersProperty)
                .GetOrCreateItem(setterProperty);
        }

        /// <summary>
        ///     Gets the shadow end point updater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <returns></returns>
        internal static EndPointUpdater GetUpdater(DependencyObject element, DependencyProperty setterProperty) =>
            EndPointUpdaters.GetOrCreate(element, ShadowEndPointUpdatersProperty).GetItem(setterProperty);

        /// <summary>
        ///     Gets the value or register parent changed.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="sourceChanged">The parent changed.</param>
        /// <returns></returns>
        internal static TValue GetValueOrRegisterParentChanged(
            DependencyObject target,
            DependencyProperty setterProperty,
            EventHandler sourceChanged) =>
            target.GetValueOrRegisterParentNotifier<TValue>(
                setterProperty,
                ParentChangedAction,
                sourceChanged,
                new ParentNotifiers());

        /// <summary>
        ///     Removes the updater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="setterProperty">The setter property.</param>
        internal static void RemoveUpdater(DependencyObject element, DependencyProperty setterProperty) =>
            EndPointUpdaters.GetOrCreate(element, ShadowEndPointUpdatersProperty).Remove(setterProperty);

        /// <summary>
        ///     Removes the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        internal static void RemoveValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] EventHandler valueChangedAction) =>
            target.RemoveValueChanged(setterProperty, valueChangedAction);

        /// <summary>
        ///     Sets the shadow end point updater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <returns></returns>
        internal static EndPointUpdater SetShadowEndPointUpdater(
            [NotNull] DependencyObject element,
            [NotNull] DependencyProperty setterProperty)
        {
            return EndPointUpdaters.GetOrCreate(element, ShadowEndPointUpdatersProperty).GetItem(setterProperty);
        }

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

            //newContent.SetterPropterty = SetterProperty;
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
