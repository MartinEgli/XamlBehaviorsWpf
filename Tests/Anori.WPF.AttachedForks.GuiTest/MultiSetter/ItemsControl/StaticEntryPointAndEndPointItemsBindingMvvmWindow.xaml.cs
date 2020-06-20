﻿namespace Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter.ItemsControl
{
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for DynamicEndPointTextBindingWindow.xaml
    /// </summary>
    public partial class StaticEntryPointAndEndPointItemsBindingMvvmWindow : Window
    {
        public StaticEntryPointAndEndPointItemsBindingMvvmWindow()
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
