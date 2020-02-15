// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Input;

using Anori.WPF.Behaviors.Core;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ICommand newDataContextCommand = null;
            newDataContextCommand = new ActionCommand(() => DataContext = new ViewModel(newDataContextCommand));
            DataContext = new ViewModel(newDataContextCommand);
        }
    }
}
