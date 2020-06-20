// -----------------------------------------------------------------------
// <copyright file="AttachedStringGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class AttachedStringGetterExtensionBase<TOwner> : AttachedGetterExtension<TOwner, string>
    where TOwner : AttachedAncestorPropertyStringBase<TOwner>
    {
    }
}
