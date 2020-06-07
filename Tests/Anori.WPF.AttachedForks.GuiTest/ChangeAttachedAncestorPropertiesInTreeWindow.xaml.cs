// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest
{
    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class ChangeAttachedAncestorPropertiesInTreeWindow : Window
    {
        private readonly FrameworkElement control;

        public ChangeAttachedAncestorPropertiesInTreeWindow()
        {
            this.InitializeComponent();
        }

        private void AddFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");
            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.Panel, "Panel");
        }

        private void RemoveFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");
            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.Panel);
        }

        private void AddSubFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub AttachedAncestorProperty");
            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.SubPanel1, "SubPanel1");
        }

        private void RemoveSubFork_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub AttachedAncestorProperty");
            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.SubPanel1);
        }
    }
}
