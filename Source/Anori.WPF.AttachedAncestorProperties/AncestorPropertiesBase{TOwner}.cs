// -----------------------------------------------------------------------
// <copyright file="AncestorPropertiesBase{TOwner}.cs" company="Anori Soft"
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

    /// <summary>
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    public abstract class AncestorPropertiesBase<TOwner> : DependencyObject
    {
        /// <summary>
        ///     The endPointUpdater property
        /// </summary>
        internal static readonly DependencyProperty ShadowEndPointUpdatersProperty =
            DependencyProperty.RegisterAttached(
                "ShadowEndPointUpdaters",
                typeof(EndPointUpdaters),
                typeof(AncestorPropertiesBase<TOwner>),
                new PropertyMetadata(default(EndPointUpdater)));

        /// <summary>
        ///     Adds the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void AddAncestorProperty<TValue>(
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
        ///     Adds the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void AddValueChangedHandler(
            [NotNull] DependencyObject target,
            DependencyProperty setterProperty,
            [NotNull] EventHandler valueChangedAction) =>
            target.AddValueChanged(setterProperty, valueChangedAction);

        /// <summary>
        ///     Removes the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public static void RemoveAttachedAncestorProperty(
            [NotNull] FrameworkElement element,
            DependencyProperty setterProperty)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Debug.WriteLine("Remove AttachedAncestorProperty Element {0}", (object)element?.Name);
            DependencyProperty shadowAttachedAncestorProperty = GetShadowEndPointUpdater(element, setterProperty);
            if (shadowAttachedAncestorProperty != null)
            {
                //shadowAttachedAncestorProperty.UnsubscribeGetters();
                element.ClearValue(setterProperty);

                //element.ClearValue(ShadowEndPointUpdaterProperty);
                //shadowAttachedAncestorProperty.UpdateGetters();
            } else
            {
                element.ClearValue(setterProperty);
            }
        }

        /// <summary>
        ///     Removes the value changed handler.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void RemoveValueChangedHandler(
            [NotNull] DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] EventHandler valueChangedAction) =>
            target.RemoveValueChanged(setterProperty, valueChangedAction);

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

            EndPointUpdaters endPointUpdaters = null;
            if (setter != DependencyProperty.UnsetValue)
            {
                endPointUpdaters = (EndPointUpdaters)shadow;
            }

            if (endPointUpdaters == null)
            {
                endPointUpdaters = EndPointUpdaters.GetOrCreate(ancestor, ShadowEndPointUpdatersProperty);
            }

            endPointUpdater = endPointUpdaters.GetOrCreateItem(setterProperty);

            return ancestor;
        }

        /// <summary>
        ///     Gets the shadow attached ancestor property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal static EndPointUpdaters GetShadowEndPointUpdaters(DependencyObject element) =>
            (EndPointUpdaters)element.GetValue(ShadowEndPointUpdatersProperty);

        internal static EndPointUpdater SetShadowEndPointUpdater(
            DependencyObject element,
            DependencyProperty setterProperty,
            EndPointUpdaters updaters)
        {
            if (!updaters.TryGetValue(setterProperty, out EndPointUpdater endPointUpdater))
            {
                endPointUpdater = new EndPointUpdater(element);
                updaters.Add(setterProperty, endPointUpdater);
            }

            return endPointUpdater;
        }

        /// <summary>
        ///     Sets the endPointUpdater.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        internal static void SetShadowEndPointUpdaters(DependencyObject element, EndPointUpdaters value) =>
            element.SetValue(ShadowEndPointUpdatersProperty, value);

        private static DependencyProperty GetShadowEndPointUpdater(
            FrameworkElement element,
            DependencyProperty setterProperty)
        {
            throw new NotImplementedException();
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
