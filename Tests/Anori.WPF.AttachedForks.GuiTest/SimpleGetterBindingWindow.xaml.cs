using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
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
