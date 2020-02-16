// -----------------------------------------------------------------------
// <copyright file="ComparisonCondition.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    ///     Represents one ternary condition.
    /// </summary>
    public class ComparisonCondition : ConditionBase
    {
        /// <summary>
        ///     The left operand property
        /// </summary>
        public static readonly DependencyProperty LeftOperandProperty = DependencyProperty.Register(
            nameof(LeftOperand),
            typeof(object),
            typeof(ComparisonCondition),
            new PropertyMetadata(null));

        /// <summary>
        ///     The operator property
        /// </summary>
        public static readonly DependencyProperty OperatorProperty = DependencyProperty.Register(
            nameof(Operator),
            typeof(ComparisonConditionType),
            typeof(ComparisonCondition),
            new PropertyMetadata(ComparisonConditionType.Equal));

        /// <summary>
        ///     The right operand property
        /// </summary>
        public static readonly DependencyProperty RightOperandProperty = DependencyProperty.Register(
            nameof(RightOperand),
            typeof(object),
            typeof(ComparisonCondition),
            new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the left operand.
        /// </summary>
        /// <value>
        ///     The left operand.
        /// </value>
        public object LeftOperand
        {
            get => this.GetValue(LeftOperandProperty);
            set => this.SetValue(LeftOperandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the comparison operator.
        /// </summary>
        /// <value>
        ///     The operator.
        /// </value>
        public ComparisonConditionType Operator
        {
            get => (ComparisonConditionType)this.GetValue(OperatorProperty);
            set => this.SetValue(OperatorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the right operand.
        /// </summary>
        /// <value>
        ///     The right operand.
        /// </value>
        public object RightOperand
        {
            get => this.GetValue(RightOperandProperty);
            set => this.SetValue(RightOperandProperty, value);
        }

        /// <summary>
        ///     Method that evaluates the condition. Note that this method can throw ArgumentException if the operator is
        ///     incompatible with the type. For instance, operators LessThan, LessThanOrEqual, GreaterThan, and GreaterThanOrEqual
        ///     require both operators to implement IComparable.
        /// </summary>
        /// <returns>
        ///     Returns true if the condition has been met; otherwise, returns false.
        /// </returns>
        /// <inheritdoc />
        public override bool Evaluate()
        {
            this.EnsureBindingUpToDate();
            return ComparisonLogic.Evaluate(this.LeftOperand, this.Operator, this.RightOperand);
        }

        /// <inheritdoc />
        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>
        ///     The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore() => new ComparisonCondition();

        /// <summary>
        ///     Ensure that any binding on DP operands are up-to-date.
        /// </summary>
        private void EnsureBindingUpToDate()
        {
            this.EnsureBindingUpToDate(LeftOperandProperty);
            this.EnsureBindingUpToDate(OperatorProperty);
            this.EnsureBindingUpToDate(RightOperandProperty);
        }
    }
}
