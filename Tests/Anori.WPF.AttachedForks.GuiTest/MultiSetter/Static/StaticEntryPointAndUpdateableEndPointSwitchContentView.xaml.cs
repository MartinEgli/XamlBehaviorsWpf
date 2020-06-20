// -----------------------------------------------------------------------
// <copyright file="StaticEntryPointAndUpdateableEndPointSwitchContentView.xaml.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter
{
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StaticEntryPointAndUpdateableEndPointSwitchContentView : UserControl
    {
        private readonly FrameworkElement control;

        public StaticEntryPointAndUpdateableEndPointSwitchContentView()
        {
            InitializeComponent();
            this.control = new AttachedUpdateableTextUserControl2();
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
