// -----------------------------------------------------------------------
// <copyright file="OneWayGetterExtensionBase{TOwner}.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class OneWayGetterExtensionBase<TOwner> : OneWayGetterExtensionBase
        where TOwner : AncestorPropertyBase<TOwner>

    {
        protected OneWayGetterExtensionBase()
            : base(AncestorPropertyBase<TOwner>.SetterProperty)
        {
        }
    }
}
