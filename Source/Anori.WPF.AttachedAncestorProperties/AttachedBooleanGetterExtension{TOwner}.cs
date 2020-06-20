// -----------------------------------------------------------------------
// <copyright file="AttachedBooleanGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WPF.AttachedAncestorProperties;

namespace Anori.WPF.AttachedAncestorProperties
{
    /// <summary>
    /// Generic Attached Boolean Getter Markup Extension.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="AttachedGetterExtension{TOwner, bool}" />
    public abstract class AttachedBooleanGetterExtensionBase<TOwner> : AttachedGetterExtension<TOwner, bool>
    where TOwner : AttachedAncestorPropertyBooleanBase<TOwner>

    {
    }
}
