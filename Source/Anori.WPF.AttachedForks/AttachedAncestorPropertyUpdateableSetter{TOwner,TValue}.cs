using System;
using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties
{
    internal class AttachedAncestorPropertyUpdateableSetter<TOwner, TValue>
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
        /// Initializes a new instance of the <see cref="AttachedAncestorPropertyUpdateableSetter{TOwner,TValue}"/> class.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="updateAction">The update action.</param>
        public AttachedAncestorPropertyUpdateableSetter(DependencyObject dependencyObject, Action<object> updateAction)
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
            ((FrameworkElement)this.dependencyObject).Loaded += this.OnLoaded;
            return AttachedAncestorProperty<TOwner, TValue>.GetValueOrRegisterParentChanged(this.dependencyObject, this.OnSourceChanged);
        }

        /// <summary>
        ///     Updates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void Update(TValue value) => this.updateAction(value);

        /// <summary>
        ///     Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is DependencyObject dependencyObj)
            {
                this.Update(AttachedAncestorProperty<TOwner, TValue>.GetValueOrRegisterParentChanged(dependencyObj, this.OnSourceChanged));
            }
        }

        /// <summary>
        /// Called when [source changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSourceChanged(object sender, EventArgs e) => this.Update(AttachedAncestorProperty<TOwner, TValue>.GetValueOrRegisterParentChanged(this.dependencyObject, this.OnSourceChanged));
    }
}
