// -----------------------------------------------------------------------
// <copyright file="StyleTriggerCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{Anori.WPF.Behaviors.ITriggerCreator}" />
    public sealed class StyleTriggerCollection : Collection<ITriggerCreator>
    {
    }
}
