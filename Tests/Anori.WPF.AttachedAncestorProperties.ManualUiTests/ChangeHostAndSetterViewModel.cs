using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests
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
        /// <value>The border text.</value>
        public string BorderText
        {
            get => borderText;
            set
            {
                if (value == borderText)
                {
                    return;
                }

                borderText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the panel text.
        /// </summary>
        /// <value>The panel text.</value>
        public string PanelText
        {
            get => panelText;
            set
            {
                if (value == panelText)
                {
                    return;
                }

                panelText = value;
                OnPropertyChanged();
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
