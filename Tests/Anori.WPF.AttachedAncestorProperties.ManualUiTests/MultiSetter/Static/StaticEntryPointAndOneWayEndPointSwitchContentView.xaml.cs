// -----------------------------------------------------------------------
// <copyright file="StaticEntryPointAndOneWayEndPointSwitchContentView.xaml.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Static
{
    using System.Windows;
    using System.Windows.Controls;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Controls;

    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StaticEntryPointAndOneWayEndPointSwitchContentView : UserControl
    {
        private readonly FrameworkElement control;

        public StaticEntryPointAndOneWayEndPointSwitchContentView()
        {
            InitializeComponent();
            this.control = new AttachedOneWayTextUserControl2();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Control2.Content = null;
            this.Control1.Content = this.control;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Control1.Content = null;
            this.Control2.Content = this.control;
        }
    }
}
