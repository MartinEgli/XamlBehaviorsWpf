// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests
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
            this.control = new SingleSetter.Controls.AttachedOneWayUserControl();
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
    }
}
