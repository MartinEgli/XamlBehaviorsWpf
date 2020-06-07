// -----------------------------------------------------------------------
// <copyright file="StyleObservableTest1ViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using Anori.WPF.Behaviors.Core;
    using JetBrains.Annotations;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    internal class StyleObservableTest1ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The text
        /// </summary>
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleObservableTest1ViewModel"/> class.
        /// </summary>
        /// <param name="newDataContextCommand">The new data context command.</param>
        public StyleObservableTest1ViewModel(ICommand newDataContextCommand)
        {
            this.NewDataContextCommand = newDataContextCommand;
            RunGarbageCollectorCommand = new ActionCommand(this.RunGarbageCollector);

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

        /// <summary>
        /// Finalizes an instance of the <see cref="StyleObservableTest1ViewModel" /> class.
        /// </summary>
        ~StyleObservableTest1ViewModel()
        {
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Creates new datacontextcommand.
        /// </summary>
        /// <value>
        ///     The new data context command.
        /// </value>
        public ICommand NewDataContextCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the run garbage collector command.
        /// </summary>
        /// <value>
        ///     The run garbage collector command.
        /// </value>
        public ICommand RunGarbageCollectorCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        public Subject<string> Subject { get; } = new Subject<string>();

        /// <summary>
        ///     Gets the set next command.
        /// </summary>
        /// <value>
        ///     The set next command.
        /// </value>
        public ICommand SetNextCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the set completed command.
        /// </summary>
        /// <value>
        ///     The set completed command.
        /// </value>
        public ICommand SetCompletedCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the set error command.
        /// </summary>
        /// <value>
        ///     The set error command.
        /// </value>
        public ICommand SetErrorCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the on next command.
        /// </summary>
        /// <value>
        ///     The on next command.
        /// </value>
        public ICommand OnNextCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the on completed command.
        /// </summary>
        /// <value>
        ///     The on completed command.
        /// </value>
        public ICommand OnCompletedCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the on error command.
        /// </summary>
        /// <value>
        ///     The on error command.
        /// </value>
        public ICommand OnErrorCommand
        {
            get;
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text
        {
            get
            {
                return this.text;
            }
            private set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Runs the garbage collector.
        /// </summary>
        private void RunGarbageCollector()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForFullGCComplete();
        }

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        private void OnCompleted()
        {
            this.Text = "Completed";
        }

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="o">The o.</param>
        private void OnError(object o)
        {
            this.Text = "Error";
        }

        /// <summary>
        ///     Called when [next].
        /// </summary>
        /// <param name="obj">The object.</param>
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
