using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    public class ChangeHostAndSetterViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The border text
        /// </summary>
        private string borderText;
        /// <summary>
        /// The panel text
        /// </summary>
        private string panelText;

        /// <summary>
        /// Gets or sets the border text.
        /// </summary>
        /// <value>
        /// The border text.
        /// </value>
        public string BorderText
        {
            get
            {
                return this.borderText;
            }
            set
            {
                if (value == this.borderText) return;
                this.borderText = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the panel text.
        /// </summary>
        /// <value>
        /// The panel text.
        /// </value>
        public string PanelText
        {
            get
            {
                return this.panelText;
            }
            set
            {
                if (value == this.panelText) return;
                this.panelText = value;
                this.OnPropertyChanged();
            }
        }

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
    }
}
