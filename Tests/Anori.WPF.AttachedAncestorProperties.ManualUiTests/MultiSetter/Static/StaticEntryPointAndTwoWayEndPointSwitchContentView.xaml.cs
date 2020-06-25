// -----------------------------------------------------------------------
// <copyright file="StaticEntryPointAndTwoWayEndPointSwitchContentView.xaml.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Controls;

    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StaticEntryPointAndTwoWayEndPointSwitchContentView : UserControl
    {
        private readonly FrameworkElement control;

        public StaticEntryPointAndTwoWayEndPointSwitchContentView()
        {
            InitializeComponent();
            this.control = new AttachedTwoWayTextUserControl2();
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
