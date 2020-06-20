// -----------------------------------------------------------------------
// <copyright file="EndPointUpdaters.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using JetBrains.Annotations;

    internal class EndPointUpdaters : Dictionary<DependencyProperty, EndPointUpdater>
    {
        private readonly DependencyObject element;

        private readonly DependencyProperty shadowProperty;

        public EndPointUpdaters([NotNull] DependencyObject element, [NotNull] DependencyProperty shadowProperty)
        {
            this.element        = element        ?? throw new ArgumentNullException(nameof(element));
            this.shadowProperty = shadowProperty ?? throw new ArgumentNullException(nameof(shadowProperty));
        }

        [NotNull]
        public static EndPointUpdaters GetOrCreate(
            [NotNull] DependencyObject element,
            [NotNull] DependencyProperty shadowProperty)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (shadowProperty == null)
            {
                throw new ArgumentNullException(nameof(shadowProperty));
            }

            EndPointUpdaters updaters = (EndPointUpdaters)element.GetValue(shadowProperty);
            if (updaters != null && updaters != DependencyProperty.UnsetValue)
            {
                return updaters;
            }

            updaters = new EndPointUpdaters(element, shadowProperty);
            updaters.SetShadowProperty();

            return updaters;
        }

        [CanBeNull]
        public static EndPointUpdaters GetShadowEndPointUpdaters(
            DependencyObject element,
            DependencyProperty shadowProperty) =>
            (EndPointUpdaters)element.GetValue(shadowProperty);

        public EndPointUpdater GetOrCreateItem([NotNull] DependencyProperty setterProperty)
        {
            if (this.element == null)
            {
                throw new ArgumentNullException(nameof(this.element));
            }

            if (setterProperty == null)
            {
                throw new ArgumentNullException(nameof(setterProperty));
            }

            if (this.TryGetValue(setterProperty, out EndPointUpdater endPointUpdater))
            {
                return endPointUpdater;
            }

            endPointUpdater = new EndPointUpdater(this.element);
            this.Add(setterProperty, endPointUpdater);

            return endPointUpdater;
        }

        public void SetShadowProperty() => this.element.SetValue(this.shadowProperty, this);

        [CanBeNull]
        public EndPointUpdater GetItem([NotNull] DependencyProperty setterProperty)
        {
            if (setterProperty == null)
            {
                throw new ArgumentNullException(nameof(setterProperty));
            }

            return this.TryGetValue(setterProperty, out EndPointUpdater endPointUpdater) ? endPointUpdater : null;
        }
    }
}
