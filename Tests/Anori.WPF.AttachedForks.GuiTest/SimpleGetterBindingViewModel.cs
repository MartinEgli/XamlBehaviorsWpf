// -----------------------------------------------------------------------
// <copyright file="SimpleGetterBindingViewModel.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    public class SimpleGetterBindingViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The getterText
        /// </summary>
        private string getterText;

        /// <summary>
        ///     The setter text
        /// </summary>
        private string setterText;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets the getterText.
        /// </summary>
        /// <value>The getterText.</value>
        public string GetterText
        {
            get => this.getterText;
            set
            {
                if (value == this.getterText)
                {
                    return;
                }

                this.getterText = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the setter text.
        /// </summary>
        /// <value>The setter text.</value>
        public string SetterText
        {
            get => this.setterText;
            set
            {
                if (value == this.setterText)
                {
                    return;
                }

                this.setterText = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
