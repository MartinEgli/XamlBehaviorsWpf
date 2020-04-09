using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedForks
{
    public class Host<T> : INotifyPropertyChanged
    {
    

        public Host([NotNull] DependencyObject owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Updates the getters.
        /// </summary>
        public void UpdateGetters()
        {
            OnHostChanged();
        }

        /// <summary>
        /// Updates the getters.
        /// </summary>
        public void UnsubscribeGetters()
        {
            OnUnsubscribe();
        }

        /// <summary>
        /// Occurs when [host changed].
        /// </summary>
        public event EventHandler HostChanged;

        public event EventHandler Unsubscribe;


        /// <summary>
        /// Called when [host changed].
        /// </summary>
        protected virtual void OnHostChanged()
        {
            this.HostChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUnsubscribe()
        {
            this.Unsubscribe?.Invoke(this, EventArgs.Empty);
        }
    }
}
