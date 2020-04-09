using System.Diagnostics;
using System.Linq;
using System.Windows;
using AttachedPropertyTests;

namespace Anori.WPF.AttachedForks.GuiTest
{
    /// <summary>
    /// Interaction logic for SimpleBindingWindow.xaml
    /// </summary>
    public partial class ItemsControlBindingWindow : Window
    {
        public ItemsControlBindingWindow()
        {
            InitializeComponent();
            ViewModel = new ItemsControlBindingViewModel();
            this.DataContext = ViewModel;
        }

        public ItemsControlBindingViewModel ViewModel { get; set; }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Item");

            ViewModel.Items.Add(new ItemViewModel());
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Item");
            if (ViewModel.Items.Any())
            {
                ViewModel.Items.RemoveAt(0);
            }
        }
    }
}
