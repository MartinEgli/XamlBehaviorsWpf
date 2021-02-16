// -----------------------------------------------------------------------
// <copyright file="AncestorPropertyBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSelf">The type of the owner.</typeparam>
    public abstract class AncestorPropertyBase<TSelf> : AncestorPropertyBase<TSelf, object>
        where TSelf : AncestorPropertyBase<TSelf, object>
    {
    }
}
