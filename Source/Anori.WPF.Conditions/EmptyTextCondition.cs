﻿// -----------------------------------------------------------------------
// <copyright file="EmptyTextCondition.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    using System.Windows;

    /// <summary>
    ///     Not Empty Text Condition
    /// </summary>
    /// <seealso cref="Anori.WPF.Conditions.ConditionBase" />
    /// <inheritdoc />
    public class EmptyTextCondition : ConditionBase
    {
        /// <summary>
        ///     The left operand property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(EmptyTextCondition),
            new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Evaluates this instance.
        /// </summary>
        /// <returns>The result.</returns>
        /// <inheritdoc />
        public override bool Evaluate()
        {
            this.EnsureBindingUpToDate();
            return string.IsNullOrEmpty(Text);
        }

        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>
        ///     The new instance.
        /// </returns>
        /// <inheritdoc />
        /// ReSharper disable once StyleCop.SA1502
        protected override Freezable CreateInstanceCore() => new EmptyTextCondition { Text = this.Text };

        /// <summary>
        ///     Ensure that any binding on DP operands are up-to-date.
        /// </summary>
        private void EnsureBindingUpToDate() => this.EnsureBindingUpToDate(TextProperty);
    }
}