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
        private string text;

        public ObservableTest1ViewModel()
        {
            this.SetNextCommand = new ActionCommand(this.OnSetNext);
            this.OnNextCommand = new ActionCommand(
                s =>
                {
                    this.OnNext((string)s);
                });
            this.SetCompletedCommand = new ActionCommand(this.OnSetCompleted);
            this.OnCompletedCommand = new ActionCommand(this.OnCompleted);
            this.SetErrorCommand = new ActionCommand(this.OnSetError);
            this.OnErrorCommand = new ActionCommand(this.OnError);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Subject<string> Subject { get; } = new Subject<string>();

        public ICommand SetNextCommand
        {
            get;
        }

        public ICommand SetCompletedCommand
        {
            get;
        }

        public ICommand SetErrorCommand
        {
            get;
        }

        public ICommand OnNextCommand
        {
            get;
        }

        public ICommand OnCompletedCommand
        {
            get;
        }

        public ICommand OnErrorCommand
        {
            get;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
        }

        private void OnCompleted()
        {
            this.Text = "Completed";
        }

        private void OnError(object o)
        {
            this.Text = "Error";
        }

        private void OnNext(string obj)
        {
            this.Text = obj;
        }

        private void OnSetError()
        {
            try
            {
                Subject.OnError(new Exception("Test"));
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        ///     Called when [set completed].
        /// </summary>
        private void OnSetCompleted()
        {
            Subject.OnCompleted();
        }

        /// <summary>
        ///     Called when [set next].
        /// </summary>
        private void OnSetNext()
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
