// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyStringBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class AncestorPropertyStringBase<TSelf> : AncestorPropertyBase<TSelf, string>
        where TSelf : AncestorPropertyStringBase<TSelf>
    {
    }

    public abstract class AncestorPropertyBooleanBase<TSelf> : AncestorPropertyBase<TSelf, bool>
        where TSelf : AncestorPropertyBooleanBase<TSelf>
    {
    }
}
