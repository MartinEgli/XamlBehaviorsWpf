﻿// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
//     Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleFatSetter.Static
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class StaticChangeEntryPointsTreeAndTwoWayEndPointView : UserControl
    {
        public StaticChangeEntryPointsTreeAndTwoWayEndPointView()
        {
            this.InitializeComponent();
        }

        private void AddPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");
            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.Panel, "Panel");
        }

        private void AddSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub AttachedAncestorProperty");
            AttachedAncestorPropertyString.AddAttachedAncestorProperty(this.SubPanel1, "SubPanel1");
        }

        private void RemovePanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");
            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.Panel);
        }

        private void RemoveSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub AttachedAncestorProperty");
            AttachedAncestorPropertyString.RemoveAttachedAncestorProperty(this.SubPanel1);
        }
    }
}
