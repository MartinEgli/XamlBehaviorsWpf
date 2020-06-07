// -----------------------------------------------------------------------
// <copyright file="InvokeCommandActionCreatorViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using Anori.WPF.Behaviors.Core;
    using Anori.WPF.Behaviors.Observables.GuiTests;
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class InvokeCommandActionCreatorViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The identifier source
        /// </summary>
        private static int idSource;

        /// <summary>
        ///     The hash seed
        /// </summary>
        private static readonly HashCode hashSeed = HashCode.Start.HashFromTypeName(typeof(InvokeCommandActionCreatorViewModel));

        /// <summary>
        ///     The hash
        /// </summary>
        private readonly int hash;

        /// <summary>
        ///     The text
        /// </summary>
        private object text;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvokeCommandActionCreatorViewModel" /> class.
        /// </summary>
        /// <param name="newDataContextCommand">The new data context command.</param>
        public InvokeCommandActionCreatorViewModel(ICommand newDataContextCommand)
        {
            this.hash = hashSeed.Hash(this.Id);
            this.NewDataContextCommand = newDataContextCommand;
            this.RunGarbageCollectorCommand = new ActionCommand(this.RunGarbageCollector);

            this.MyCommand = new ActionCommand(this.MyAction);
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; } = Interlocked.Increment(ref idSource);

        /// <summary>
        ///     Gets my command.
        /// </summary>
        /// <value>
        ///     My command.
        /// </value>
        public ICommand MyCommand
        {
            get;
        }

        /// <summary>
        ///     Gets my hash code.
        /// </summary>
        /// <value>
        ///     My hash code.
        /// </value>
        public int MyHashCode => this.GetHashCode();

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
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public object Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (Equals(value, this.text))
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
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
        ///     Runs the garbage collector.
        /// </summary>
        private void RunGarbageCollector()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForFullGCComplete();
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => this.hash;

        /// <summary>
        ///     My Action.
        /// </summary>
        private void MyAction()
        {
            this.Text = DateTime.Now.ToFileTime();
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
