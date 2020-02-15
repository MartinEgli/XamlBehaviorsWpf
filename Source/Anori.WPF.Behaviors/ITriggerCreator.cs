// -----------------------------------------------------------------------
// <copyright file="ITriggerCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    /// <summary>
    /// </summary>
    public interface ITriggerCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        TriggerBase Create();
    }
}
