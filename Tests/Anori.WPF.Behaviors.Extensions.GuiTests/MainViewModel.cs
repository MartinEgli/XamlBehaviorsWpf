// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using Anori.WPF.Behaviors.Core;
    using JetBrains.Annotations;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            OpenToggleEnabledCommand = new ActionCommand(this.OpenToggleEnabled);
            OpenTwoToggleEnabledCommand = new ActionCommand(this.OpenTwoToggleEnabled);
            OpenInvokeCommandActionCreatorCommand = new ActionCommand(OpenInvokeCommandActionCreator);
            RunGarbageCollectorCommand = new ActionCommand(this.RunGarbageCollector);
            OpenStyleTriggerTestCommand = new ActionCommand(this.OpenStyleTriggerTest);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OpenToggleEnabledCommand
        {
            get;
        }

        public ICommand RunGarbageCollectorCommand
        {
            get;
        }

        public ICommand OpenStyleTriggerTestCommand
        {
            get;
        }

        public ICommand OpenTwoToggleEnabledCommand
        {
            get;
        }

        public ICommand OpenInvokeCommandActionCreatorCommand
        {
            get;
        }

        private void OpenInvokeCommandActionCreator()
        {
            new InvokeCommandActionCreatorWindow().ShowDialog();
        }

        private void OpenTwoToggleEnabled()
        {
            (new TwoToggleEnabledTargetedTriggerActionWindow()).ShowDialog();
        }

        private void OpenStyleTriggerTest()
        {
            (new StyleTriggerTestWindow()).ShowDialog();
        }

        private void RunGarbageCollector()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForFullGCComplete();
        }

        private void OpenToggleEnabled()
        {
            (new ToggleEnabledTargetedTriggerActionWindow()).ShowDialog();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
