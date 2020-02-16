// -----------------------------------------------------------------------
// <copyright file="ConditionCollectionExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
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
        public static bool IsAny(this ConditionCollection conditions, bool parameter = true)
        {
            bool any = false;
            foreach (ConditionBase condition in conditions)
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
        public static bool AreAll(this ConditionCollection conditions, bool parameter = true)
        {
            bool all = true;
            foreach (ConditionBase condition in conditions)
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
