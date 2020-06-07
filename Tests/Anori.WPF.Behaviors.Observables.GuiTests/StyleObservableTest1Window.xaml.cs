// -----------------------------------------------------------------------
// <copyright file="StyleObservableTest1Window.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Input;

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
                    this.DataContext = new StyleObservableTest1ViewModel(newDataContextCommand);
                });
            DataContext = new StyleObservableTest1ViewModel(newDataContextCommand);
        }
    }
}
