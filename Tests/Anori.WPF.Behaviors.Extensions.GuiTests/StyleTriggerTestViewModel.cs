// -----------------------------------------------------------------------
// <copyright file="StyleTriggerTestViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Extensions.GuiTests
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    using Anori.WPF.Behaviors.Core;
    using Anori.WPF.Behaviors.Observables.GuiTests;

    using JetBrains.Annotations;

    public class StyleTriggerTestViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The identifier source
        /// </summary>
        private static int idSource;

        /// <summary>
        ///     The hash seed
        /// </summary>
        private static readonly HashCode hashSeed = HashCode.Start.HashFromTypeName(typeof(StyleTriggerTestViewModel));

        /// <summary>
        ///     The hash
        /// </summary>
        private readonly int hash;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StyleTriggerTestViewModel" /> class.
        /// </summary>
        /// <param name="newDataContextCommand">The new data context command.</param>
        public StyleTriggerTestViewModel(ICommand newDataContextCommand)
        {
            this.hash = hashSeed.Hash(this.Id);
            this.NewDataContextCommand = newDataContextCommand;
            this.MyCommand = new ActionCommand(this.MyAction);
            RunGarbageCollectorCommand = new ActionCommand(this.RunGarbageCollector);
        }

        ~StyleTriggerTestViewModel()
        {
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

        public ICommand MyCommand
        {
            get;
        }

        public int MyHashCode => this.GetHashCode();

        public ICommand NewDataContextCommand
        {
            get;
        }

        public ICommand RunGarbageCollectorCommand
        {
            get;
        }

        private void RunGarbageCollector()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForFullGCComplete();
        }

        public override int GetHashCode() => this.hash;

        private void MyAction()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
