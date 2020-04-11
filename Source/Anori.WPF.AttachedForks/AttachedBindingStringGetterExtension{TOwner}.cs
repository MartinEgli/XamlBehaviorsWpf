// -----------------------------------------------------------------------
// <copyright file="AttachedBindingStringGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedBindingStringGetterExtensionBase<TOwner> : AttachedBindingGetterExtension<TOwner, string>
       where TOwner : AttachedForkStringBase<TOwner>

    {
    }
}
