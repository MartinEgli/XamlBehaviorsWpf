// -----------------------------------------------------------------------
// <copyright file="StyleObservableTest1Window.xaml.cs" company="Anori Soft">
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
using System.Windows.Shapes;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using Anori.WPF.Behaviors.Core;

    /// <summary>
    ///     Interaction logic for ObservableTest1Window.xaml
    /// </summary>
    public partial class StyleObservableTest1Window : Window
    {
        public StyleObservableTest1Window()
        {
            InitializeComponent();
            ICommand newDataContextCommand = null;
            newDataContextCommand = new ActionCommand(
                () =>
                {
                    this.DataContext = new ObservableTest1ViewModel(newDataContextCommand);
                });
            DataContext = new ObservableTest1ViewModel(newDataContextCommand);
        }
    }
}
