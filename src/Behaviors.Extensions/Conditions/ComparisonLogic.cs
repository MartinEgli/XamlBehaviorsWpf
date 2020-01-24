// -----------------------------------------------------------------------
// <copyright file="ComparisonLogic.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bfa.Common.WPF.Conditions
{
    using System;
    using System.Globalization;

    using Bfa.Common.WPF.Converters;
    using Bfa.Common.WPF.Resources;

    using JetBrains.Annotations;

    using Microsoft.Xaml.Behaviors.Core;

    /// <summary>
    ///     Comparison Logic
    /// </summary>
    public static class ComparisonLogic
    {
        /// <summary>
        ///     This method evaluates operands.
        /// </summary>
        /// <param name="leftOperand">Left operand from the LeftOperand property.</param>
        /// <param name="operatorType">Operator from Operator property.</param>
        /// <param name="rightOperand">Right operand from the RightOperand property.</param>
        /// <returns>
        ///     Returns true if the condition is met; otherwise, returns false.
        /// </returns>
        /// <exception cref="ArgumentException">Compare Parameter Exception</exception>
        public static bool Evaluate(
            [CanBeNull] object leftOperand,
            ComparisonConditionType operatorType,
            [CanBeNull] object rightOperand)
        {
            if (leftOperand != null)
            {
                var leftType = leftOperand.GetType();

                if (rightOperand != null)
                {
                    var typeConverter = leftType.GetTypeConverter();
                    rightOperand = typeConverter.DoConversionFrom(rightOperand);
                }
            }

            var leftComparableOperand = leftOperand as IComparable;
            var rightComparableOperand = rightOperand as IComparable;

            // If both operands are comparable, use arithmetic comparison
            if (leftComparableOperand != null && rightComparableOperand != null)
            {
                return EvaluateComparable(leftComparableOperand, operatorType, rightComparableOperand);
            }

            switch (operatorType)
            {
                case ComparisonConditionType.Equal:
                    return Equals(leftOperand, rightOperand);

                case ComparisonConditionType.NotEqual:
                    return !Equals(leftOperand, rightOperand);

                default:
                    switch (leftComparableOperand)
                    {
                        case null when rightComparableOperand == null:
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    ExceptionStrings.InvalidOperands,
                                    leftOperand != null ? leftOperand.GetType().Name : "null",
                                    rightOperand != null ? rightOperand.GetType().Name : "null",
                                    operatorType.ToString()));
                        case null:
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    ExceptionStrings.InvalidLeftOperand,
                                    leftOperand != null ? leftOperand.GetType().Name : "null",
                                    operatorType.ToString()));
                        default:
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    ExceptionStrings.InvalidRightOperand,
                                    rightOperand != null ? rightOperand.GetType().Name : "null",
                                    operatorType.ToString()));
                    }
            }
        }

        /// <summary>
        ///     Evaluates both operands that implement the IComparable interface.
        /// </summary>
        /// <param name="leftOperand">Left operand from the LeftOperand property.</param>
        /// <param name="operatorType">Operator from Operator property.</param>
        /// <param name="rightOperand">Right operand from the RightOperand property.</param>
        /// <returns>Returns true if the condition is met; otherwise, returns false.</returns>
        private static bool EvaluateComparable(
            IComparable leftOperand,
            ComparisonConditionType operatorType,
            IComparable rightOperand)
        {
            object convertedOperand = null;

            try
            {
                convertedOperand = Convert.ChangeType(rightOperand, leftOperand.GetType(), CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                // FormatException: Convert.ChangeType("hello", typeof(double), ...);
            }
            catch (InvalidCastException)
            {
                // InvalidCastException: Convert.ChangeType(4.0d, typeof(Rectangle), ...);
            }

            if (convertedOperand == null)
            {
                return operatorType == ComparisonConditionType.NotEqual;
            }

            var comparison = leftOperand.CompareTo((IComparable)convertedOperand);
            var result = false;

            switch (operatorType)
            {
                case ComparisonConditionType.Equal:
                    result = comparison == 0;
                    break;

                case ComparisonConditionType.GreaterThan:
                    result = comparison > 0;
                    break;

                case ComparisonConditionType.GreaterThanOrEqual:
                    result = comparison >= 0;
                    break;

                case ComparisonConditionType.LessThan:
                    result = comparison < 0;
                    break;

                case ComparisonConditionType.LessThanOrEqual:
                    result = comparison <= 0;
                    break;

                case ComparisonConditionType.NotEqual:
                    result = comparison != 0;
                    break;
            }

            return result;
        }
    }
}