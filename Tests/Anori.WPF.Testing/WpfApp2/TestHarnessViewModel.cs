using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace Anori.WPF.Testing
{
    public class TestHarnessViewModel :  INotifyPropertyChanged
    {
        private UserControl content;

        public event PropertyChangedEventHandler PropertyChanged;

        public new UserControl Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (Equals(value, this.content))
                {
                    return;
                }
                this.content = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
