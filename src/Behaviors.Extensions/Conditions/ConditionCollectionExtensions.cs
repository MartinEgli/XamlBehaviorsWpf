// -----------------------------------------------------------------------
// <copyright file="ConditionCollectionExtensions.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    /// <summary>
    ///     Condition Collection Extensions Class
    /// </summary>
    public static class ConditionCollectionExtensions
    {
        /// <summary>
        ///     Is any of the conditions true or false.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="parameter">if set to <c>true</c> [parameter].</param>
        /// <returns>
        ///     The result.
        /// </returns>
        public static bool IsAny(
            this Microsoft.Xaml.Behaviors.Core.ConditionCollection conditions,
            bool parameter = true)
        {
            var any = false;
            foreach (var condition in conditions)
            {
                if (condition.Evaluate() != parameter)
                {
                    continue;
                }

                any = true;
                break;
            }

            return any;
        }

        /// <summary>
        ///     Is all of the conditions true or false.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="parameter">if set to <c>true</c> [parameter].</param>
        /// <returns>
        ///     The result.
        /// </returns>
        public static bool AreAll(
            this Microsoft.Xaml.Behaviors.Core.ConditionCollection conditions,
            bool parameter = true)
        {
            var all = true;
            foreach (var condition in conditions)
            {
                if (condition.Evaluate() == parameter)
                {
                    continue;
                }

                all = false;
                break;
            }

            return all;
        }
    }
}