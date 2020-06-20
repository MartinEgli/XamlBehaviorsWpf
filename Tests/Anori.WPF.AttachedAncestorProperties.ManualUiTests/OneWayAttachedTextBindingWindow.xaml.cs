using System.Windows;
using AttachedPropertyTests;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests
{
    /// <summary>
    /// Interaction logic for DynamicEndPointTextBindingWindow.xaml
    /// </summary>
    public partial class OneWayAttachedTextBindingWindow : Window
    {
        public OneWayAttachedTextBindingWindow()
        {
            InitializeComponent();

            this.DataContext = new SimpleAttachedTextBindingViewModel();
        }
    }
}
