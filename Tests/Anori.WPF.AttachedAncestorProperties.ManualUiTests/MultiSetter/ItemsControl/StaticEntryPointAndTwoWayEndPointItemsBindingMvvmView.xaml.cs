﻿namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.ItemsControl
{
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for StaticEntryPointAndEndPointItemsBindingMvvmView.xaml
    /// </summary>
    public partial class StaticEntryPointAndTwoWayEndPointItemsBindingMvvmView : UserControl
    {
        public StaticEntryPointAndTwoWayEndPointItemsBindingMvvmView()
        {
            InitializeComponent();
            this.ViewModel = new ItemsControlBindingViewModel();
            this.DataContext = this.ViewModel;
        }

        public ItemsControlBindingViewModel ViewModel { get; set; }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Item");

            this.ViewModel.Items.Add(new ItemViewModel());
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Item");
            if (this.ViewModel.Items.Any())
            {
                this.ViewModel.Items.RemoveAt(0);
            }
        }
    }
}