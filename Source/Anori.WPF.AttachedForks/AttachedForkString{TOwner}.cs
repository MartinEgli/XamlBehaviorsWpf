// -----------------------------------------------------------------------
// <copyright file="AttachedForkString.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedForkString<TOwner> : AttachedFork<string, TOwner>
        where TOwner : AttachedForkString<TOwner>
    {
    }
}
