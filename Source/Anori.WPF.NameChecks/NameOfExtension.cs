// -----------------------------------------------------------------------
// <copyright file="NameOfExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.NameChecks
{
    #region

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     Class NameOfExtension.
    ///     Implements the <see cref="System.Windows.Markup.MarkupExtension" />
    /// </summary>
    /// <seealso cref="System.Windows.Markup.MarkupExtension" />
    [MarkupExtensionReturnType(typeof(string))]
    [ContentProperty(nameof(Member))]
    public class NameOfExtension : MarkupExtension
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfExtension" /> class.
        /// </summary>
        /// <param name="member">The member.</param>
        public NameOfExtension([NotNull] string member)
        {
            this.Member = member ?? throw new ArgumentNullException(nameof(member));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfExtension" /> class.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">member</exception>
        /// <exception cref="ArgumentNullException">type</exception>
        public NameOfExtension([NotNull] string member, [NotNull] Type type)
        {
            this.Member = member ?? throw new ArgumentNullException(nameof(member));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfExtension" /> class.
        /// </summary>
        public NameOfExtension()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfExtension" /> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        public NameOfExtension([NotNull] Binding binding)
        {
            this.Binding = binding ?? throw new ArgumentNullException(nameof(binding));
        }

        /// <summary>
        ///     Gets or sets the binding.
        /// </summary>
        /// <value>The binding.</value>
        [ConstructorArgument("binding")]
        public Binding Binding { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [throw runtime exception].
        /// </summary>
        /// <value><c>true</c> if [throw runtime exception]; otherwise, <c>false</c>.</value>
        public bool ThrowRuntimeException { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [ConstructorArgument("type")]
        public Type Type { get; set; }

        /// <summary>
        ///     Gets or sets the member.
        /// </summary>
        /// <value>The member.</value>
        [ConstructorArgument("member")]
        public string Member { get; set; }

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        /// <exception cref="ArgumentNullException">serviceProvider</exception>
        /// <exception cref="ArgumentException">No property path</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOf is Type={x:Type [className]}</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]</exception>
        /// <exception cref="ArgumentException">No member found for {this.Member} in {this.Type}</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (!this.ThrowRuntimeException)
            {
                IProvideValueTarget target =
                    (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                if (!(target.TargetObject is DependencyObject dependencyObject
                      && System.ComponentModel.DesignerProperties.GetIsInDesignMode(dependencyObject)))
                {
                    return this.Member;
                }
            }

            if (this.Binding != null)
            {
                PropertyPath path = this.Binding.Path;
                if (path == null)
                {
                    throw new ArgumentException("No property path");
                }

                int indexOfLastVariableName = path.Path.LastIndexOf('.');
                return path.Path.Substring(indexOfLastVariableName + 1);
            }

            if (this.Type == null)
            {
                throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]}");
            }

            if (string.IsNullOrEmpty(this.Member))
            {
                throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]");
            }

            if (this.Member.Contains("."))
            {
                throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]");
            }

            if (this.Type.GetRuntimeProperties().Any(i => i.Name == this.Member))
            {
                return this.Member;
            }

            if (this.Type.GetRuntimeFields().Any(i => i.Name == this.Member))
            {
                return this.Member;
            }

            if (this.Type.GetRuntimeEvents().Any(i => i.Name == this.Member))
            {
                return this.Member;
            }

            if (this.Type.GetRuntimeMethods().Any(i => i.Name == this.Member))
            {
                return this.Member;
            }

            throw new ArgumentException($"No member found for {this.Member} in {this.Type}");
        }
    }
}
