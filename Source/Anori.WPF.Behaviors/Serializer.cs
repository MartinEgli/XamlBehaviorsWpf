// -----------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Annotations;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;

    internal sealed class Serializer
    {
        // We can't mark the class as static, because XmlSerializer refuses to serialize a class
        // that is nested inside a static class.  Instead, we mark the constructor as private,
        // so nobody can instantiate Serializer.
        private Serializer() { }

        /// <summary>
        ///     Hexadecimals the color of the string to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Serializer.HexStringToColor requires input of a 8-character
        ///     hexadecimal string, but received '" + value + "'.
        /// </exception>
        public static Color HexStringToColor(string value)
        {
            if (value.Length != 8)
            {
                throw new InvalidOperationException(
                    "Serializer.HexStringToColor requires input of a 8-character hexadecimal string, but received '"
                    + value + "'.");
            }

            byte a = byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte r = byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte g = byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte b = byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        ///     Colors to hexadecimal string.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static string ColorToHexString(Color color)
        {
            string a = color.A.ToString("X2", CultureInfo.InvariantCulture);
            string r = color.R.ToString("X2", CultureInfo.InvariantCulture);
            string g = color.G.ToString("X2", CultureInfo.InvariantCulture);
            string b = color.B.ToString("X2", CultureInfo.InvariantCulture);
            return a + r + g + b;
        }

        /// <summary>
        ///     Serializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="stream">The stream.</param>
        public static void Serialize(Data data, Stream stream)
        {
            // Ensure that the schema version entry is set correctly.
            data.SchemaVersion = Data.CurrentSchemaVersion;

            XmlWriterSettings settings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        ///     Deserializes the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static Data Deserialize(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Deserialize(stream);
            }
        }

        /// <summary>
        ///     Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static Data Deserialize(Stream stream)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                return serializer.Deserialize(stream) as Data;
            } catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the schema version.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static int? GetSchemaVersion(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    // Look for the "Data" element
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element
                            && StringComparer.InvariantCultureIgnoreCase.Equals(reader.LocalName, "Data"))
                        {
                            reader.MoveToAttribute("SchemaVersion");
                            break;
                        }
                    }

                    int? schemaVersion = null;
                    if (!reader.EOF)
                    {
                        int value;
                        if (Int32.TryParse(reader.Value, out value))
                        {
                            schemaVersion = value;
                        }
                    }

                    return schemaVersion;
                }
            }
        }

        [SuppressMessage(
            "Microsoft.Naming",
            "CA1724:TypeNamesShouldNotMatchNamespaces",
            Justification = "This usage does not seem confusing in context.")]
        [SuppressMessage(
            "Microsoft.Design",
            "CA1034:NestedTypesShouldNotBeVisible",
            Justification = "The use of visible nested classes for serialization seems reasonable.")]
        public class Data
        {
            /// <summary>
            ///     The current version of the flow file schema.
            ///     This number should be incremented whenever:
            ///     A new _required_ field is added.
            ///     The data type of a field is changed.
            ///     The semantic interpretation of a field is changed.
            ///     When upgrading the current schema number, you'll also need to take into account
            ///     migration/upgrade strategies, and mechanisms for deserializing older schemas.
            ///     In some cases, the same serializer data structure may suffice by applying different
            ///     parsing validation rules.  In other cases, a new data structure may be needed to
            ///     deserialize the old format from disk.
            /// </summary>
            public static readonly int CurrentSchemaVersion = 2;

            // If the SchemaVersion attribute is missing, we assume it's v1.
            /// <summary>
            ///     The default schema version
            /// </summary>
            public static readonly int DefaultSchemaVersion = 1;

            /// <summary>
            ///     The minimum valid schema version
            /// </summary>
            public static readonly int MinValidSchemaVersion = 1;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Data" /> class.
            /// </summary>
            public Data()
            {
                // When deserializing, if the SchemaVersion is not included in the file, we default to v1.
                this.SchemaVersion = DefaultSchemaVersion;

                this.RuntimeOptions = new RuntimeOptionsData();
                this.ViewState = new ViewStateData();
                this.VisualTags = new List<VisualTag>();
                this.Screens = new List<Screen>();
            }

            /// <summary>
            ///     Gets or sets the schema version.
            /// </summary>
            /// <value>
            ///     The schema version.
            /// </value>
            [XmlAttribute]
            public int SchemaVersion { get; set; }

            /// <summary>
            ///     Gets or sets the sketch flow unique identifier.
            /// </summary>
            /// <value>
            ///     The sketch flow unique identifier.
            /// </value>
            public Guid SketchFlowGuid { get; set; }

            /// <summary>
            ///     Gets or sets the start screen.
            /// </summary>
            /// <value>
            ///     The start screen.
            /// </value>
            public string StartScreen { get; set; }

            /// <summary>
            ///     Gets or sets the screens.
            /// </summary>
            /// <value>
            ///     The screens.
            /// </value>
            [SuppressMessage(
                "Microsoft.Usage",
                "CA2227:CollectionPropertiesShouldBeReadOnly",
                Justification =
                    "These collections are not part of a 'public' API, and it's just too handy to be able to replace the whole list.")]
            [SuppressMessage(
                "Microsoft.Design",
                "CA1002:DoNotExposeGenericLists",
                Justification =
                    "These generic lists are fine in their context. There is no need to listen to individual changes and they are more performant.")]
            public List<Screen> Screens { get; set; }

            /// <summary>
            ///     Gets or sets the share point document library.
            /// </summary>
            /// <value>
            ///     The share point document library.
            /// </value>
            [SuppressMessage(
                "Microsoft.Usage",
                "CA2227:CollectionPropertiesShouldBeReadOnly",
                Justification =
                    "These collections are not part of a 'public' API, and it's just too handy to be able to replace the whole list.")]
            [SuppressMessage(
                "Microsoft.Design",
                "CA1002:DoNotExposeGenericLists",
                Justification =
                    "These generic lists are fine in their context. There is no need to listen to individual changes and they are more performant.")]
            public string SharePointDocumentLibrary { get; set; }

            /// <summary>
            ///     Gets or sets the name of the share point project.
            /// </summary>
            /// <value>
            ///     The name of the share point project.
            /// </value>
            public string SharePointProjectName { get; set; }

            /// <summary>
            ///     Gets or sets the prototype revision.
            /// </summary>
            /// <value>
            ///     The prototype revision.
            /// </value>
            public int PrototypeRevision { get; set; }

            /// <summary>
            ///     Gets or sets the branding text.
            /// </summary>
            /// <value>
            ///     The branding text.
            /// </value>
            public string BrandingText { get; set; }

            /// <summary>
            ///     Gets or sets the runtime options.
            /// </summary>
            /// <value>
            ///     The runtime options.
            /// </value>
            public RuntimeOptionsData RuntimeOptions { get; set; }

            /// <summary>
            ///     Gets or sets the visual tags.
            /// </summary>
            /// <value>
            ///     The visual tags.
            /// </value>
            [SuppressMessage(
                "Microsoft.Usage",
                "CA2227:CollectionPropertiesShouldBeReadOnly",
                Justification =
                    "These collections are not part of a 'public' API, and it's just too handy to be able to replace the whole list.")]
            [SuppressMessage(
                "Microsoft.Design",
                "CA1002:DoNotExposeGenericLists",
                Justification =
                    "These generic lists are fine in their context. There is no need to listen to individual changes and they are more performant.")]
            public List<VisualTag> VisualTags { get; set; }

            /// <summary>
            ///     Gets or sets the state of the view.
            /// </summary>
            /// <value>
            ///     The state of the view.
            /// </value>
            public ViewStateData ViewState { get; set; }

            /// <summary>
            /// </summary>
            [SuppressMessage(
                "Microsoft.Design",
                "CA1034:NestedTypesShouldNotBeVisible",
                Justification = "The use of visible nested classes for serialization seems reasonable.")]
            public class RuntimeOptionsData
            {
                public bool HideNavigation { get; set; }

                public bool HideAnnotationAndInk { get; set; }

                public bool DisableInking { get; set; }

                public bool HideDesignTimeAnnotations { get; set; }

                public bool ShowDesignTimeAnnotationsAtStart { get; set; }
            }

            /// <summary>
            /// </summary>
            [SuppressMessage(
                "Microsoft.Design",
                "CA1034:NestedTypesShouldNotBeVisible",
                Justification = "The use of visible nested classes for serialization seems reasonable.")]
            public class ViewStateData
            {
                public double Zoom { get; set; }

                public Point? Center { get; set; }
            }

            /// <summary>
            /// </summary>
            [SuppressMessage(
                "Microsoft.Design",
                "CA1034:NestedTypesShouldNotBeVisible",
                Justification = "The use of visible nested classes for serialization seems reasonable.")]
            public class Screen
            {
                /// <summary>
                ///     Initializes a new instance of the <see cref="Screen" /> class.
                /// </summary>
                public Screen()
                {
                    this.Annotations = new List<Annotation>();
                }

                /// <summary>
                ///     Gets or sets the type.
                /// </summary>
                /// <value>
                ///     The type.
                /// </value>
                [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
                public ScreenType Type { get; set; }

                /// <summary>
                ///     Gets or sets the name of the class.
                /// </summary>
                /// <value>
                ///     The name of the class.
                /// </value>
                public string ClassName { get; set; }

                /// <summary>
                ///     Gets or sets the display name.
                /// </summary>
                /// <value>
                ///     The display name.
                /// </value>
                public string DisplayName { get; set; }

                /// <summary>
                ///     Gets or sets the name of the file.
                /// </summary>
                /// <value>
                ///     The name of the file.
                /// </value>
                public string FileName { get; set; }

                /// <summary>
                ///     Gets or sets the annotations.
                /// </summary>
                /// <value>
                ///     The annotations.
                /// </value>
                [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
                [SuppressMessage(
                    "Microsoft.Design",
                    "CA1002:DoNotExposeGenericLists",
                    Justification =
                        "These generic lists are fine in their context. There is no need to listen to individual changes and they are more performant.")]
                public List<Annotation> Annotations { get; set; }

                /// <summary>
                ///     Gets or sets the position.
                /// </summary>
                /// <value>
                ///     The position.
                /// </value>
                public Point Position { get; set; }

                /// <summary>
                ///     Gets or sets the visual tag.
                /// </summary>
                /// <value>
                ///     The visual tag.
                /// </value>
                public int? VisualTag { get; set; }
            }

            /// <summary>
            /// VisualTag
            /// </summary>
            [SuppressMessage(
                "Microsoft.Design",
                "CA1034:NestedTypesShouldNotBeVisible",
                Justification = "The use of visible nested classes for serialization seems reasonable.")]
            public class VisualTag
            {
                /// <summary>
                ///     Gets or sets the name.
                /// </summary>
                /// <value>
                ///     The name.
                /// </value>
                public string Name { get; set; }

                /// <summary>
                ///     Gets or sets the color.
                /// </summary>
                /// <value>
                ///     The color.
                /// </value>
                public string Color { get; set; }
            }
        }
    }
}
