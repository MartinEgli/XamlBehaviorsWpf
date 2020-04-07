// -----------------------------------------------------------------------
// <copyright file="AttachedStringGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedStringGetterExtension<TOwner> : AttachedGetterExtension<string, TOwner>
        where TOwner : AttachedForkString<TOwner>

    {
    }
}
