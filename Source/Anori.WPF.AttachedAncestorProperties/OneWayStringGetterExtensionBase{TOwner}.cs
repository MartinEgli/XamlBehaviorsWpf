// -----------------------------------------------------------------------
// <copyright file="OneWayStringGetterExtensionBase.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    public abstract class OneWayStringGetterExtensionBase<TOwner> : OneWayGetterExtensionBase<TOwner, string>
        where TOwner : AncestorPropertyStringBase<TOwner>

    {
        
    }


    public abstract class OneWayBooleanGetterExtensionBase<TOwner> : OneWayGetterExtensionBase<TOwner, bool>
        where TOwner : AncestorPropertyBooleanBase<TOwner>

    {

    }
}
