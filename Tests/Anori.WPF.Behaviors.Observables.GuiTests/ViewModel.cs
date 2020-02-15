// -----------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    using Anori.WPF.Behaviors.Core;

    using JetBrains.Annotations;

    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The identifier source
        /// </summary>
        private static int idSource;

        /// <summary>
        ///     The hash seed
        /// </summary>
        private static readonly HashCode hashSeed = HashCode.Start.HashFromTypeName(typeof(ViewModel));

        /// <summary>
        ///     The hash
        /// </summary>
        private readonly int hash;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModel" /> class.
        /// </summary>
        /// <param name="newDataContextCommand">The new data context command.</param>
        public ViewModel(ICommand newDataContextCommand)
        {
            this.hash = hashSeed.Hash(this.Id);
            this.NewDataContextCommand = newDataContextCommand;
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

        public ICommand MyCommand
        {
            get;
        }

        public int MyHashCode => this.GetHashCode();

        public ICommand NewDataContextCommand
        {
            get;
        }

        public override int GetHashCode() => hash;

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
