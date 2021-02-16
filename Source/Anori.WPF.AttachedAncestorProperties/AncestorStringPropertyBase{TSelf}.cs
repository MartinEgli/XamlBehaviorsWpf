// -----------------------------------------------------------------------
// <copyright file="AncestorStringPropertyBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class AncestorStringPropertyBase<TSelf> : AncestorPropertyBase<TSelf, string>
        where TSelf : AncestorPropertyBase<TSelf, string>
    {
    }
}
