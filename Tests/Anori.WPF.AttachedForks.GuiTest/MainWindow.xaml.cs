// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using Anori.WPF.AttachedForks.GuiTest;

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
            var dialog = new SwitchContentWindow();
            dialog.ShowDialog();
        }

        private void OneToMultiButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OneToMultibleWindow();
            dialog.ShowDialog();
        }

        private void SimpleBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SimpleBindingWindow();
            dialog.ShowDialog();
        }

        private void ControlBindingWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ControlBindingWindow();
            dialog.ShowDialog();

        }

        private void ChangeHost_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new ChangeHostWindow();
            dialog.ShowDialog();
        }
    }
}
