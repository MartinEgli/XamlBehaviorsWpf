// -----------------------------------------------------------------------
// <copyright file="AttachedForkBoolean.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WPF.AttachedForks.AttachedFork{System.Boolean, TOwner}" />
    public abstract class AttachedForkBoolean<TOwner> : AttachedFork<bool, TOwner>
        where TOwner : AttachedForkBoolean<TOwner>

    {
    }
}
