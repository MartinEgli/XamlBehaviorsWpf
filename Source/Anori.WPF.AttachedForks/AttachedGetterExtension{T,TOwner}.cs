// -----------------------------------------------------------------------
// <copyright file="AttachedGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Markup;
using Anori.WPF.Extensions;

namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOwner"></typeparam>
    /// <seealso cref="System.Windows.Markup.MarkupExtension" />
    public abstract class AttachedGetterExtension<T, TOwner> : UpdateableMarkupExtension
        where TOwner : AttachedFork<T, TOwner>
    {
        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        protected override object ProvideValueInternal(IServiceProvider serviceProvider)
        {
            var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (!(provideValueTarget.TargetObject is DependencyObject dependencyObject))
            {
                return null;
            }

            return new Setter(dependencyObject,this.UpdateValue).ProvideValue();
        }

        internal class Setter
        {
            /// <summary>
            /// The dependency object
            /// </summary>
            private readonly DependencyObject dependencyObject;

            /// <summary>
            /// The update action
            /// </summary>
            private readonly Action<object> updateAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="Setter"/> class.
            /// </summary>
            /// <param name="dependencyObject">The dependency object.</param>
            /// <param name="updateAction">The update action.</param>
            public Setter(DependencyObject dependencyObject, Action<object> updateAction)
            {
                this.dependencyObject = dependencyObject;
                this.updateAction = updateAction;
            }

            /// <summary>
            /// Provides the value.
            /// </summary>
            /// <returns></returns>
            public object ProvideValue()
            {
                ((FrameworkElement)dependencyObject).Loaded += this.OnLoaded;

                return AttachedFork<T, TOwner>.GetValueOrRegisterParentChanged(dependencyObject, OnSourceChanged);

            }

            /// <summary>
            ///     Updates the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            private void Update(T value) => this.updateAction(value);

            /// <summary>
            ///     Called when [loaded].
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
            private void OnLoaded(object sender, RoutedEventArgs e)
            {
                if (sender is DependencyObject dependencyObject)

                {
                    this.Update(AttachedFork<T, TOwner>.GetValueOrRegisterParentChanged(dependencyObject, OnSourceChanged));
                }
            }

            /// <summary>
            /// Called when [source changed].
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            private void OnSourceChanged(object sender, EventArgs e)
            {
                this.Update(AttachedFork<T, TOwner>.GetValueOrRegisterParentChanged(dependencyObject, OnSourceChanged));
            }
        }



    }
}
