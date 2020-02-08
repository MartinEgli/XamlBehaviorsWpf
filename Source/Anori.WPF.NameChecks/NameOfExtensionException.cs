// -----------------------------------------------------------------------
// <copyright file="NameOfExtensionException.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.NameChecks
{
    using System;

    /// <summary>
    /// Class NameOfExtensionException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NameOfExtensionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameOfExtensionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NameOfExtensionException(string message) : base(message) { }
    }
}
