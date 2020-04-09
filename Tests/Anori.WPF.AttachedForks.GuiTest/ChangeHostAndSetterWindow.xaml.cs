// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Windows;

namespace Anori.WPF.AttachedForks.GuiTest
{
    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class ChangeHostAndSetterWindow : Window
    {
        private readonly FrameworkElement control;

        public ChangeHostAndSetterWindow()
        {
            this.InitializeComponent();
            DataContext = new ChangeHostAndSetterViewModel();
        }


        private void AddFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Host");

            AttachedForkString.AddHost(this.Panel, "Panel");
        }

        private void RemoveFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Host");

            AttachedForkString.RemoveHost(this.Panel);

  //          this.Panel.ClearValue(AttachedForkString.SetterProperty);
        }
    }
}
