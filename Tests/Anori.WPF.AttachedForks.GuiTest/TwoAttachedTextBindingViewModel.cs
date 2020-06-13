using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    public class TwoAttachedTextBindingViewModel : INotifyPropertyChanged
    {
        private string text1;

        public string Text1
        {
            get => this.text1;
            set
            {
                if (value == this.text1)
                {
                    return;
                }

                this.text1 = value;
                this.OnPropertyChanged();
            }
        }


        private string text2;

        public string Text2
        {
            get => this.text2;
            set
            {
                if (value == this.text2)
                {
                    return;
                }

                this.text2 = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
