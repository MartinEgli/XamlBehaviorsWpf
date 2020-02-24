// -----------------------------------------------------------------------
// <copyright file="IItem.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System.Windows.Input;

    public interface IItem
    {
        string Name { get; }

        ICommand Command { get; }
    }
}
