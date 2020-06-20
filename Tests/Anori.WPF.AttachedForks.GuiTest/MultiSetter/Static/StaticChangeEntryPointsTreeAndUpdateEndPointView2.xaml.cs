// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class StaticChangeEntryPointsTreeAndUpdateEndPointView2 : UserControl
    {
        public StaticChangeEntryPointsTreeAndUpdateEndPointView2()
        {
            this.InitializeComponent();
        }

        private void AddPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");
            AncestorProperty.AddAncestorProperty(this.Panel, "Panel", AttachedAncestorProperties.SetterAProperty);
        }

        private void RemovePanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");
            AncestorProperty.RemoveAncestorProperty(this.Panel, AttachedAncestorProperties.SetterAProperty);
        }

        private void AddSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub AttachedAncestorProperty");
            AncestorProperty.AddAncestorProperty(this.SubPanel1, "SubPanel1", AttachedAncestorProperties.SetterAProperty);
            AncestorProperty.AddAncestorProperty(this.SubPanel2, "SubPanel1", AttachedAncestorProperties.SetterBProperty);
        }

        private void RemoveSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub AttachedAncestorProperty");
            AncestorProperty.RemoveAncestorProperty(this.SubPanel1, AttachedAncestorProperties.SetterAProperty);
            AncestorProperty.RemoveAncestorProperty(this.SubPanel2, AttachedAncestorProperties.SetterBProperty);
        }
    }
}
