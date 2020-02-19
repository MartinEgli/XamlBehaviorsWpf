// -----------------------------------------------------------------------
// <copyright file="ObservableTest1ViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using Anori.WPF.Behaviors.Core;

    using JetBrains.Annotations;

    internal class ObservableTest1ViewModel : INotifyPropertyChanged
    {
        public ObservableTest1ViewModel()
        {
            NextCommand = new ActionCommand(this.OnNext);
            OnNextCommand = new ActionCommand(s => this.ReceiveNext((string)s));
            this.CompletedCommand = new ActionCommand(this.OnCompleted);
            ExceptionCommand = new ActionCommand(OnException);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Subject<string> Subject { get; } = new Subject<string>();

        public ICommand NextCommand
        {
            get;
        }

        public ICommand CompletedCommand
        {
            get;
        }

        public ICommand ExceptionCommand
        {
            get;
        }

        public ICommand OnNextCommand
        {
            get;
        }

        private void ReceiveNext(string str)
        {
        }

        private void OnException()
        {
            Subject.OnError(new Exception("Test"));
        }

        private void OnCompleted()
        {
            Subject.OnCompleted();
        }

        private void OnNext()
        {
            Subject.OnNext(DateTime.Now.ToString());
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
