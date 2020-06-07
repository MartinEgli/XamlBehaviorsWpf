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
    public partial class ChangeAttachedAncestorPropertyAndSetterBindingWindow : Window
    {
        private readonly FrameworkElement control;

        public ChangeAttachedAncestorPropertyAndSetterBindingWindow()
        {
            this.InitializeComponent();
            DataContext = new ChangeHostAndSetterViewModel();
        }

        private void AddAttachedAncestorProperty_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Attached Ancestor Property");

            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.Panel, "Panel");
        }

        private void RemoveAttachedAncestorProperty_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Attached Ancestor Property");

            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.Panel);
        }
    }
}
