// -----------------------------------------------------------------------
// <copyright file="OneWayGetterExtensionBase{TOwner,TValue}.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class OneWayGetterExtensionBase<TOwner, TValue> : OneWayGetterExtensionBase
        where TOwner : AncestorPropertyBase<TOwner, TValue>

    {
        protected OneWayGetterExtensionBase()
            : base(AncestorPropertyBase<TOwner, TValue>.SetterProperty)
        {
        }
    }
}
