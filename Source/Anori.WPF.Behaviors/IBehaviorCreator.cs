// -----------------------------------------------------------------------
// <copyright file="IBehaviorCreator.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System.Windows.Data;

    /// <summary>
    /// </summary>
    public interface IBehaviorCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        Behavior Create();
    }

    public interface IBindingCreator
    {
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        Binding Create();
    }
}
