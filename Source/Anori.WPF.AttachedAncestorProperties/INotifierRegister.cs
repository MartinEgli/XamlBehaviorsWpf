// -----------------------------------------------------------------------
// <copyright file="INotifierRegister.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;

namespace Anori.WPF.AttachedAncestorProperties
{
    public interface INotifierRegister
    {
        void Remove(DependencyObject target);

        void Add(DependencyObject target, DependencyObject dependencyObject);
    }
}
