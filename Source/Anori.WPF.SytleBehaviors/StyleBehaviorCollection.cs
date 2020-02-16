// -----------------------------------------------------------------------
// <copyright file="StyleBehaviorCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System.Collections.ObjectModel;

    using Anori.WPF.Behaviors;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{IBehaviorCreator}" />
    public sealed class StyleBehaviorCollection : Collection<IBehaviorCreator>
    {
    }
}
