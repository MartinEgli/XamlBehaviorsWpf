// -----------------------------------------------------------------------
// <copyright file="NameOfElementExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.NameChecks
{
    #region

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Xaml;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     Class NameOfElementExtension.
    ///     Implements the <see cref="System.Windows.Markup.MarkupExtension" />
    /// </summary>
    /// <seealso cref="System.Windows.Markup.MarkupExtension" />
    [MarkupExtensionReturnType(typeof(string))]
    [ContentProperty(nameof(Element))]
    public class NameOfElementExtension : MarkupExtension
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfElementExtension" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        public NameOfElementExtension([NotNull] string element)
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfElementExtension" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">element</exception>
        /// <exception cref="ArgumentNullException">type</exception>
        public NameOfElementExtension([NotNull] string element, [NotNull] Type type)
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfElementExtension" /> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">binding</exception>
        /// <exception cref="ArgumentNullException">type</exception>
        public NameOfElementExtension([NotNull] Binding binding, [NotNull] Type type)
        {
            this.Binding = binding ?? throw new ArgumentNullException(nameof(binding));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfElementExtension" /> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <exception cref="ArgumentNullException">binding</exception>
        public NameOfElementExtension([NotNull] Binding binding)
        {
            this.Binding = binding ?? throw new ArgumentNullException(nameof(binding));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameOfElementExtension" /> class.
        /// </summary>
        public NameOfElementExtension()
        {
        }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [ConstructorArgument("type")]
        public object Type { get; set; }

        /// <summary>
        ///     Gets or sets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        public Type ElementType { get; set; }

        /// <summary>
        ///     Gets or sets the element name.
        /// </summary>
        /// <value>The element.</value>
        [ConstructorArgument("element")]
        public string Element { get; set; }

        /// <summary>
        ///     Gets or sets the binding.
        /// </summary>
        /// <value>The binding.</value>
        [ConstructorArgument("binding")]
        public Binding Binding { get; set; }

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        /// <exception cref="ArgumentNullException">serviceProvider</exception>
        /// <exception cref="ArgumentException">No element name</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOfElement is Type={x:Type [className]}</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOfElement is Type={x:Type [className]} Element=[Name]</exception>
        /// <exception cref="ArgumentException">No element found for {Element} in {Type}</exception>
        /// <exception cref="ArgumentException">Syntax for x:NameOfElement is Type={x:Type [className]} Element=[Name]</exception>
        /// <exception cref="Exception">Element Type is " + Type.GetRuntimeFields().First(i => i.Name == Element).FieldType.Name</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (this.Binding != null)
            {
                string elementName = this.Binding.ElementName;

                this.Element = elementName ?? throw new NameOfExtensionException("BindingHasNoElementName");
            }

            if (this.Type == null)
            {
                if (!(serviceProvider.GetService(typeof(IRootObjectProvider)) is IRootObjectProvider rootProvider))
                {
                    throw new ArgumentException("Syntax for x:NameOfElement is Type={x:Type [className]}");
                }

                this.Type = rootProvider.RootObject.GetType();
            }

            if (!(this.Type is Type type))
            {
                throw new ArgumentException($"Type is not a Type {this.Type.GetType()}");
            }

            if (string.IsNullOrEmpty(this.Element))
            {
                throw new ArgumentException("Syntax for x:NameOfElement is Type={x:Type [className]} Element=[Name]");
            }

            if (this.Element.Contains("."))
            {
                throw new ArgumentException("Syntax for x:NameOfElement is Type={x:Type [className]} Element=[Name]");
            }

            if (type.GetRuntimeFields().All(i => i.Name != this.Element))
            {
                throw new ArgumentException($"No element found for {this.Element} in {this.Type}");
            }

            if (this.ElementType == null)
            {
                return this.Element;
            }

            if (this.ElementType.IsAssignableFrom(type.GetRuntimeFields().First(i => i.Name == this.Element).FieldType))
            {
                return this.Element;
            }

            throw new Exception(
                "Element Type is " + type.GetRuntimeFields().First(i => i.Name == this.Element).FieldType.Name);
        }
    }
}
