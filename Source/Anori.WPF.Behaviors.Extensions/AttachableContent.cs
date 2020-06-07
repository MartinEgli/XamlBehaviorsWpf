// -----------------------------------------------------------------------
// <copyright file="AttachableCollection.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

namespace Anori.WPF.Behaviors.Extensions
{
    /// <summary>
    ///     Represents a collection of IAttachedObject with a shared AssociatedObject and provides change notifications to its
    ///     contents when that AssociatedObject changes.
    /// </summary>
    ///
    [ContentProperty("Content")]
    public abstract class AttachableContent<T> : Freezable, IAttachedObject, INotifyPropertyChanged
           where T : DependencyObject, IAttachedObject
    {
        /// <summary>
        /// The associated object
        /// </summary>
        private DependencyObject associatedObject;

        /// <summary>
        /// The content
        /// </summary>
        private T content;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public T Content
        {
            get => this.content;
            set
            {
                if (EqualityComparer<T>.Default.Equals(value, this.content))
                {
                    return;
                }

                var oldValue = this.Content;
                this.content = value;
                this.Update(value, oldValue);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The object on which the collection is hosted.
        /// </summary>
        protected DependencyObject AssociatedObject
        {
            get
            {
                this.ReadPreamble();
                return this.associatedObject;
            }
        }

        /// <summary>
        /// Called immediately after the collection is attached to an AssociatedObject.
        /// </summary>
        protected virtual void OnAttached()
        {
            T content = this.Content;
            {
                content.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected virtual void OnDetaching()
        {
            T content = this.Content;
            {
                content.Detach();
            }
        }

        /// <summary>
        /// Called when a new item is added to the collection.
        /// </summary>
        /// <param name="newContent">The new content.</param>
        /// <param name="oldContent">The old content.</param>
        internal virtual void Update([CanBeNull] T newContent, [CanBeNull] T oldContent)
        {
            if (oldContent?.AssociatedObject != null)
            {
                oldContent.Detach();
            }

            if (newContent != null && this.AssociatedObject != null)
            {
                newContent.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        internal virtual void Clear()
        {
            if (this.content?.AssociatedObject != null)
            {
                this.content.Detach();
            }

            this.content = null;
        }

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        /// <value>
        /// The associated object.
        /// </value>
        /// <remarks>
        /// Represents the object the instance is attached to.
        /// </remarks>
        DependencyObject IAttachedObject.AssociatedObject => this.AssociatedObject;

        /// <summary>
        ///     Attaches to the specified object.
        /// </summary>
        /// <param name="dependencyObject">The object to attach to.</param>
        /// <exception cref="InvalidOperationException">The IAttachedObject is already attached to a different object.</exception>
        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException();
                }

                if (!(bool)this.GetValue(DesignerProperties.IsInDesignModeProperty))
                {
                    this.WritePreamble();

                    this.associatedObject = dependencyObject;
                    this.WritePostscript();
                }

                this.OnAttached();
            }
        }

        /// <summary>
        ///     Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            this.OnDetaching();
            this.WritePreamble();
            this.associatedObject = null;
            this.WritePostscript();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
