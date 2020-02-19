// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using Anori.WPF.Behaviors.Core;

    using JetBrains.Annotations;

    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.OpenObservableTestCommand = new ActionCommand(this.OpenObservableTest);
            this.RunGarbageCollectorCommand = new ActionCommand(this.RunGarbageCollector);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RunGarbageCollectorCommand
        {
            get;
        }

        public ICommand OpenObservableTestCommand
        {
            get;
        }

        private void OpenObservableTest()
        {
            (new ObservableTest1Window()).ShowDialog();
        }

        private void RunGarbageCollector()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForFullGCComplete();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
