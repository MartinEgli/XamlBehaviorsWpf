// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WPF.AttachedAncestorProperties.GuiTest;
using System.Windows;

namespace AttachedPropertyTests
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Anori.WPF.AttachedAncestorProperties.GuiTest.SwitchContentWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiCachedButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleCachedWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiStyleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleStyleWindow();
            dialog.ShowDialog();
        }

        private void SimpleBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DynamicEndPointTextBindingWindow{DataContext = new SimpleAttachedTextBindingViewModel()};
            dialog.ShowDialog();
        }

        private void SimpleWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticEntryPointAndUpdateableEndPointTextWindow();
            dialog.ShowDialog();
        }

        private void TwoWayTextBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TwoWayAttachedTextBindingWindow();
            dialog.DataContext = new SimpleAttachedTextBindingViewModel();

            dialog.ShowDialog();
        }
        private void OneWayTextBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneWayAttachedTextBindingWindow();
            dialog.ShowDialog();
        }

        private void ControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ControlBindingWindow();
            dialog.ShowDialog();
        }

        private void ChangeHost_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChangeAttachedAncestorPropertyInTreeWindow();
            dialog.ShowDialog();
        }

        private void ChangeHostAndSetter_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ChangeAttachedAncestorPropertyAndSetterBindingWindow();
            dialog.ShowDialog();
        }

        private void TreeChangeHost_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticChangeEntryPointsTreeAndUpdateEndPointWindow();
            dialog.ShowDialog();
        }

        private void ItemsControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ItemsControlBindingWindow();
            dialog.ShowDialog();
        }

        private void MuliAttachedBoolControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MultiAttachedBoolControlBindingWindow();
            dialog.ShowDialog();
        }

        private void MuliAttachedBoolControlWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticMultiEntryPointsAndUpdateableEndPointsBoolControlWindow();
            dialog.ShowDialog();
        }

        private void DynamicMultiEntryMultiEndPointBooleanBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DynamicMultiEntryMultiEndPointBooleanBindingWindow();
            dialog.ShowDialog();
        }

        private void StaticMultiEndPointBooleanBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StaticMultiEndPointBooleanBindingWindow();
            dialog.ShowDialog();
        }

        private void SimpleGetterBinding_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Anori.WPF.AttachedAncestorProperties.GuiTest.SimpleGetterBindingWindow();
            dialog.ShowDialog();
        }
    }
}
