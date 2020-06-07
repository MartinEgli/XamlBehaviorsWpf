// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreatorWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using Anori.WPF.Behaviors.Core;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    ///     StyleInteraction logic for InvokeCommandActionCreatorWindow.xaml
    /// </summary>
    public partial class InvokeCommandActionCreatorWindow : Window
    {
        public InvokeCommandActionCreatorWindow()
        {
            InitializeComponent();
            ICommand newDataContextCommand = null;
            newDataContextCommand = new ActionCommand(
                () =>
                {
                    this.DataContext = new InvokeCommandActionCreatorViewModel(newDataContextCommand);
                });
            this.DataContext = new InvokeCommandActionCreatorViewModel(newDataContextCommand);
        }
    }
}
