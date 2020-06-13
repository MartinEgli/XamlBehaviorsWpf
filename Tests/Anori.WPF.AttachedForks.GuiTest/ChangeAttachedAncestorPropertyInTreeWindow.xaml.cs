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
    public partial class ChangeAttachedAncestorPropertyInTreeWindow : Window
    {
        public ChangeAttachedAncestorPropertyInTreeWindow()
        {
            this.InitializeComponent();
        }

        private void AddAttachedAncestorProperty_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");

            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.Panel, "Panel");
        }

        private void RemoveAttachedAncestorProperty_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");

            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.Panel);

            //          this.Panel.ClearValue(AttachedAncestorPropertyString.SetterProperty);
        }
    }
}
