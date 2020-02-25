// -----------------------------------------------------------------------
// <copyright file="CommandWindowItem.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors.Observables.GuiTests
{
    using System;
    using System.Windows;

    /// <summary>
    ///     CommandWindowItem Class
    /// </summary>
    /// <seealso cref="Anori.WPF.Behaviors.Observables.GuiTests.CommandItem" />
    public class CommandWindowItem : CommandItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandItem" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="window">The window.</param>
        public CommandWindowItem(string name, Func<Window> window)
            : base(name, () => window().ShowDialog())
        {
        }
    }

    public class GarbageCollectorItem : CommandItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandItem" /> class.
        /// </summary>
        public GarbageCollectorItem()
            : base(
                "Run Garbage Collector",
                () =>
                {
                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForFullGCComplete();
                })
        {
        }
    }
}
