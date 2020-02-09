// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WPF.Behaviors
{
    using System.Windows.Markup;

    /// <summary>
    /// </summary>
    public class Unset​ValueExtension : MarkupExtension
    {
        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return System.Windows.DependencyProperty.UnsetValue;
        }
    }
}
