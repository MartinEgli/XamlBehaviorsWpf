﻿// -----------------------------------------------------------------------
// <copyright file="AttachedStringGetterExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedForks
{
    public abstract class AttachedStringGetterExtensionBase<TOwner> : AttachedGetterExtension<TOwner ,string>
    where TOwner : AttachedForkStringBase<TOwner>
    {
    }
}
