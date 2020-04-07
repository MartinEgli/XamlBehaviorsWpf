// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Data;
using Anori.WPF.AttachedForks;

namespace AttachedPropertyTests
{
    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class SwitchContentWindow : Window
    {
        private readonly FrameworkElement control;

        public SwitchContentWindow()
        {
            this.InitializeComponent();
            this.control = new Anori.WPF.AttachedForks.GuiTest.AttachedUserControl();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Control2.Content = null;
            this.Control1.Content = this.control;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Control1.Content = null;
            this.Control2.Content = this.control;
        }

        private void AddFork_Click(object sender, RoutedEventArgs e)
        {
            this.Panel.SetValue(AttachedForkString.SetterProperty, "Panel");
        }

        private void RemoveFork_Click(object sender, RoutedEventArgs e)
        {
            this.Panel.ClearValue(AttachedForkString.SetterProperty);
        }
    }
}
