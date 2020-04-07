using System.Windows;

namespace AttachedPropertyTests
{
    /// <summary>
    /// Interaction logic for SimpleBindingWindow.xaml
    /// </summary>
    public partial class SimpleBindingWindow : Window
    {
        public SimpleBindingWindow()
        {
            InitializeComponent();
            DataContext = new SimpleBindingViewModel();
        }
    }
}