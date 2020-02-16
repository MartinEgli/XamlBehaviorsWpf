// -----------------------------------------------------------------------
// <copyright file="BooleanCondition.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    using System.Windows;

    /// <summary>
    /// Boolean Condition
    /// </summary>
    /// <seealso cref="Anori.WPF.Conditions.ConditionBase" />
    /// <inheritdoc />
    public class BooleanCondition : ConditionBase
    {
        /// <summary>
        /// The boolean property
        /// </summary>
        public static readonly DependencyProperty BooleanProperty = DependencyProperty.Register(
            nameof(Boolean),
            typeof(bool),
            typeof(BooleanCondition),
            new PropertyMetadata(true));

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="BooleanCondition" /> is boolean.
        /// </summary>
        /// <value>
        ///     <c>true</c> if boolean; otherwise, <c>false</c>.
        /// </value>
        public bool Boolean
        {
            get => (bool)this.GetValue(BooleanProperty);
            set => this.SetValue(BooleanProperty, value);
        }

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns>The result.</returns>
        /// <inheritdoc />
        public override bool Evaluate()
        {
            this.EnsureBindingUpToDate();
            return this.Boolean;
        }

        /// <inheritdoc />
        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>
        ///     The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore() => new BooleanCondition();

        /// <summary>
        ///     Ensure that any binding on DP operands are up-to-date.
        /// </summary>
        private void EnsureBindingUpToDate() => this.EnsureBindingUpToDate(BooleanProperty);
    }
}
