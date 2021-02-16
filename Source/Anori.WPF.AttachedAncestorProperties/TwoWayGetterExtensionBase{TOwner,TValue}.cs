// -----------------------------------------------------------------------
// <copyright file="TwoWayGetterExtension.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties
{
    using System.Windows;

    using JetBrains.Annotations;

    public abstract class TwoWayGetterExtensionBase<TOwner, TValue> : TwoWayGetterExtensionBase
        where TOwner : AncestorPropertyBase<TOwner, TValue>

    {
        protected TwoWayGetterExtensionBase()
            : base(AncestorPropertyBase<TOwner, TValue>.SetterProperty)
        {
        }
    }

    public abstract class TwoWayBooleanGetterExtensionBase<TOwner> : TwoWayGetterExtensionBase<TOwner, bool>
        where TOwner : AncestorPropertyBase<TOwner, bool>

    {
       
    }

    public abstract class TwoWayStringGetterExtensionBase<TOwner> : TwoWayGetterExtensionBase<TOwner, string>
        where TOwner : AncestorPropertyBase<TOwner, string>

    {

    }
}
