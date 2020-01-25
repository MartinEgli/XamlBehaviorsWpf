using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Microsoft.Xaml.Behaviors.Core;

namespace Behaviors.Extensions.GuiTests
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            this.Observable = new Subject<string>();
            releaseObservableCommand = new ActionCommand(() => this.Observable.Publish(""));
        }

        public ISubject<string> Observable { get; set; }

        private ICommand releaseObservableCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ReleaseObservableCommand
            => releaseObservableCommand;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
