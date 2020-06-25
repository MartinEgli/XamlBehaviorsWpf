// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class StaticChangeEntryPointsTreeAndOneWayEndPointtView2 : UserControl
    {
        public StaticChangeEntryPointsTreeAndOneWayEndPointtView2()
        {
            this.InitializeComponent();
        }

        private void AddPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");
            AncestorPropertyHelper.AddAncestorProperty(this.Panel, "Panel", AttachedAncestorProperties.SetterAProperty);
        }

        private void RemovePanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");
            AncestorPropertyHelper.RemoveAncestorProperty(this.Panel, AttachedAncestorProperties.SetterAProperty);
        }

        private void AddSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub AttachedAncestorProperty");
            AncestorPropertyHelper.AddAncestorProperty(this.SubPanel1, "SubPanel1", AttachedAncestorProperties.SetterAProperty);
            AncestorPropertyHelper.AddAncestorProperty(this.SubPanel2, "SubPanel1", AttachedAncestorProperties.SetterBProperty);
        }

        private void RemoveSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub AttachedAncestorProperty");
            AncestorPropertyHelper.RemoveAncestorProperty(this.SubPanel1, AttachedAncestorProperties.SetterAProperty);
            AncestorPropertyHelper.RemoveAncestorProperty(this.SubPanel2, AttachedAncestorProperties.SetterBProperty);
        }
    }
}
