using System.Windows;
using AttachedPropertyTests;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    /// <summary>
    /// Interaction logic for DynamicEndPointTextBindingWindow.xaml
    /// </summary>
    public partial class TwoWayAttachedTextBindingWindow : Window
    {
        public TwoWayAttachedTextBindingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SimpleAttachedTextBindingViewModel vm)
            {
                vm.Text = "Reset";
            }
        }
    }
}
