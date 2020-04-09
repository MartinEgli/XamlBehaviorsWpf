// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.AttachedForks.GuiTest
{
    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class ChangeHostWindow : Window
    {
        private readonly FrameworkElement control;

        public ChangeHostWindow()
        {
            this.InitializeComponent();
            this.control = new Anori.WPF.AttachedForks.GuiTest.AttachedUserControl();
        }


        private void AddFork_Click(object sender, RoutedEventArgs e)
        {
            this.Panel.SetValue(AttachedForkString.SetterProperty, "Panel");
        }

        private void RemoveFork_Click(object sender, RoutedEventArgs e)
        {
            AttachedForkString.RemoveHost(this.Panel);

  //          this.Panel.ClearValue(AttachedForkString.SetterProperty);
        }
    }
}
