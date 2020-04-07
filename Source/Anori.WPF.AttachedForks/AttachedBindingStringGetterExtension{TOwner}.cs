// -----------------------------------------------------------------------
// <copyright file="AttachedBindingStringGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedBindingStringGetterExtension<TOwner> : AttachedBindingGetterExtension<string, TOwner>
        where TOwner : AttachedForkString<TOwner>

    {
    }
}
