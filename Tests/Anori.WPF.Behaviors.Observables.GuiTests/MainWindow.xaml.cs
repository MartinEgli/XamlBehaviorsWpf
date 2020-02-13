using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Anori.WPF.Behaviors.Core;
using JetBrains.Annotations;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             DataContext = new ViewModel();
       }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            MyCommand = new ActionCommand(this.MyAction);
        }

        public ICommand MyCommand
        {
            get;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MyAction()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
