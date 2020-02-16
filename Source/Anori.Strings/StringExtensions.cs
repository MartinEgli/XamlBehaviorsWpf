// -----------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="Anori Soft">
//      Copyright (c) Anori Soft Martin Egli. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Anori.Strings
{
    #region

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Determines whether [is null or white space].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or white space] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
