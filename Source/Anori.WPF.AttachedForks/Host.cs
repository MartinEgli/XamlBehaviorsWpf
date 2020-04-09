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
        /// <summary>
        /// The value
        /// </summary>
        private T value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (EqualityComparer<T>.Default.Equals(value, this.value)) return;
                this.value = value;
                this.OnValueChanged(value);
                this.OnPropertyChanged();
            }
        }

        public Host([NotNull] DependencyObject owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public DependencyObject Owner { get; }

        public event EventHandler<T> ValueChanged;

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
        /// Occurs when [host changed].
        /// </summary>
        public event EventHandler HostChanged;

        /// <summary>
        /// Called when [host changed].
        /// </summary>
        protected virtual void OnHostChanged()
        {
            this.HostChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [value changed].
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void OnValueChanged(T value)
        {
            this.ValueChanged?.Invoke(this, value);
        }
    }
}
