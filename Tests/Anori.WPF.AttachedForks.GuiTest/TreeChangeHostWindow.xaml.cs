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
    public partial class TreeChangeHostWindow : Window
    {
        private readonly FrameworkElement control;

        public TreeChangeHostWindow()
        {
            this.InitializeComponent();
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
        }

        private void AddSubFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub Host");
            AttachedForkString.AddHost(this.SubPanel1, "SubPanel1");
        }

        private void RemoveSubFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub Host");
            AttachedForkString.RemoveHost(this.SubPanel1);
        }
    }
}
