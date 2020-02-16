// -----------------------------------------------------------------------
// <copyright file="StyleTriggerCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.StyleBehaviors
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{ITriggerCreator}" />
    public sealed class StyleTriggerCollection : Collection<ITriggerCreator>
    {
    }
}
