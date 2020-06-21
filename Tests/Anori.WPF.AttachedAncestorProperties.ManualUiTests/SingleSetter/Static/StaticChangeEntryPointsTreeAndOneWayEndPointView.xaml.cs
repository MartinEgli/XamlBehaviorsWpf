// -----------------------------------------------------------------------
// <copyright file="SwitchContentWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     Interaction logic for SwitchContentWindow.xaml
    /// </summary>
    public partial class StaticChangeEntryPointsTreeAndOneWayEndPointView : UserControl
    {
        public StaticChangeEntryPointsTreeAndOneWayEndPointView()
        {
            this.InitializeComponent();
        }

        private void AddPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add AttachedAncestorProperty");
           AncestorPropertyHelper.AddAncestorProperty(this.Panel, "Panel",AncestorStringProperty.SetterProperty);
        }

        private void AddSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Add Sub AttachedAncestorProperty");
            AncestorPropertyHelper.AddAncestorProperty(this.SubPanel1, "SubPanel1", AncestorStringProperty.SetterProperty);
        }

        private void RemovePanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove AttachedAncestorProperty");
            AncestorPropertyHelper.RemoveAncestorProperty(this.Panel, AncestorStringProperty.SetterProperty);
        }
        private void RemoveSubPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("--> Remove Sub AttachedAncestorProperty");
            AncestorPropertyHelper.RemoveAncestorProperty(this.SubPanel1, AncestorStringProperty.SetterProperty);
        }
    }
}
