// -----------------------------------------------------------------------
// <copyright file="Getter.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Binding")]
    public class Getter : AttachedAncestorPropertyGetterBase
    {
        
        protected override Freezable CreateInstanceCore() => new Getter();
    }
}
