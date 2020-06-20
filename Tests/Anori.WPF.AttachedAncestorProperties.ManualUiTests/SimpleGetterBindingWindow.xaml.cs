using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests
{
    /// <summary>
    /// Interaction logic for DynamicEndPointTextBindingWindow.xaml
    /// </summary>
    public partial class SimpleGetterBindingWindow : Window
    {
        public SimpleGetterBindingWindow()
        {
            InitializeComponent();
            this.DataContext = new SimpleGetterBindingViewModel();
        }
    }
}
