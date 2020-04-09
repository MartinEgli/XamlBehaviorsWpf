using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedForks.GuiTest
{
    public class ChangeHostAndSetterViewModel : INotifyPropertyChanged
    {
        private string borderText;
        private string panelText;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
