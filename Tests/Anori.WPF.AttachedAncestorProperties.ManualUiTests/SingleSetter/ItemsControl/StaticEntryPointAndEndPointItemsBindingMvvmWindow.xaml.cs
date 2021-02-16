namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.ItemsControl
{
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for DynamicEndPointTextBindingWindow.xaml
    /// </summary>
    public partial class ItemsControlBindingWindow : Window
    {
        public ItemsControlBindingWindow()
        {
            InitializeComponent();
            ViewModel = new SingleFatSetter.ItemsControl.ItemsControlBindingViewModel();
            this.DataContext = ViewModel;
        }

        public SingleFatSetter.ItemsControl.ItemsControlBindingViewModel ViewModel { get; set; }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Item");

            ViewModel.Items.Add(new SingleFatSetter.ItemsControl.ItemViewModel());
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
