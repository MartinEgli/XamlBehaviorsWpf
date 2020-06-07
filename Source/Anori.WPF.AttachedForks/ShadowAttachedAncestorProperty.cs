using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal class ShadowAttachedAncestorProperty : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowAttachedAncestorProperty"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <exception cref="ArgumentNullException">owner</exception>
        public ShadowAttachedAncestorProperty([NotNull] DependencyObject owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public DependencyObject Owner { get; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Updates the getters.
        /// </summary>
        public void UpdateGetters() => OnHostChanged();

        /// <summary>
        /// Updates the getters.
        /// </summary>
        public void UnsubscribeGetters() => OnUnsubscribe();

        /// <summary>
        /// Occurs when [host changed].
        /// </summary>
        public event EventHandler AttachedAncestorChanged;

        /// <summary>
        /// Occurs when [unsubscribe].
        /// </summary>
        public event EventHandler Unsubscribe;

        /// <summary>
        /// Called when [host changed].
        /// </summary>
        protected virtual void OnHostChanged() => this.AttachedAncestorChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Called when [unsubscribe].
        /// </summary>
        protected virtual void OnUnsubscribe() => this.Unsubscribe?.Invoke(this, EventArgs.Empty);
    }
}
