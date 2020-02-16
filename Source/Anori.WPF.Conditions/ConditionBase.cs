// -----------------------------------------------------------------------
// <copyright file="ConditionBase.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    ///     Condition base class
    /// </summary>
    public abstract class ConditionBase : Freezable
    {
        /// <summary>
        ///     The inverted property
        /// </summary>
        public static readonly DependencyProperty InvertedProperty = DependencyProperty.Register(
            nameof(Inverted),
            typeof(bool),
            typeof(ConditionBase),
            new PropertyMetadata(false));

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="ConditionBase" /> is inverted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if inverted; otherwise, <c>false</c>.
        /// </value>
        public bool Inverted
        {
            get => (bool)this.GetValue(InvertedProperty);
            set => this.SetValue(InvertedProperty, value);
        }

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns>The result.</returns>
        public abstract bool Evaluate();
    }
}
