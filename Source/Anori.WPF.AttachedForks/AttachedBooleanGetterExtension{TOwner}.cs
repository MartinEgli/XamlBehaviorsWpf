// -----------------------------------------------------------------------
// <copyright file="AttachedBooleanGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    /// <summary>
    /// Generic Attached Boolean Getter Markup Extension.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WPF.AttachedForks.AttachedGetterExtension{System.Boolean, TOwner}" />
    public abstract class AttachedBooleanGetterExtensionBase<TOwner> : AttachedGetterExtension<TOwner ,bool>
    where TOwner : AttachedForkBooleanBase<TOwner>

    {
    }
}
