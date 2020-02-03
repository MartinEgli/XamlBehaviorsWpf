// -----------------------------------------------------------------------
// <copyright file="NameResolver.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;

    /// <summary>
    ///     Helper class to handle the logic of resolving a TargetName into a Target element
    ///     based on the context provided by a host element.
    /// </summary>
    public sealed class NameResolver
    {
        /// <summary>
        ///     The name
        /// </summary>
        private string name;

        /// <summary>
        ///     The name scope reference element
        /// </summary>
        private FrameworkElement nameScopeReferenceElement;

        /// <summary>
        ///     Occurs when the resolved element has changed.
        /// </summary>
        public event EventHandler<NameResolvedEventArgs> ResolvedElementChanged;

        /// <summary>
        ///     Gets or sets the name of the element to attempt to resolve.
        /// </summary>
        /// <value>The name to attempt to resolve.</value>
        public string Name
        {
            get => this.name;

            set
            {
                // because the TargetName influences that Target returns, need to
                // store the Target value before we change it so we can detect if
                // it has actually changed
                var oldObject = this.Object;
                this.name = value;
                this.UpdateObjectFromName(oldObject);
            }
        }

        /// <summary>
        ///     Gets or sets the reference element from which to perform the name resolution.
        /// </summary>
        /// <value>The reference element.</value>
        public FrameworkElement NameScopeReferenceElement
        {
            get => this.nameScopeReferenceElement;

            set
            {
                var oldHost = this.NameScopeReferenceElement;
                this.nameScopeReferenceElement = value;
                this.OnNameScopeReferenceElementChanged(oldHost);
            }
        }

        /// <summary>
        ///     Gets the resolved object. Will return the reference element if TargetName is null or empty, or if a resolve has not
        ///     been
        ///     attempted.
        /// </summary>
        /// <value>
        ///     The object.
        /// </value>
        public DependencyObject Object
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name) && this.HasAttempedResolve)
                {
                    return this.NameScopeReferenceElement;
                }

                return this.ResolvedObject;
            }
        }

        /// <summary>
        ///     Gets the actual name scope reference element.
        /// </summary>
        /// <value>
        ///     The actual name scope reference element.
        /// </value>
        private FrameworkElement ActualNameScopeReferenceElement
        {
            get
            {
                if (this.NameScopeReferenceElement == null || !IsElementLoaded(this.NameScopeReferenceElement))
                {
                    return null;
                }

                return this.GetActualNameScopeReference(this.NameScopeReferenceElement);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has attemped resolve.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has attemped resolve; otherwise, <c>false</c>.
        /// </value>
        private bool HasAttempedResolve { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the reference element load is pending.
        /// </summary>
        /// <value>
        ///     <c>True</c> if [pending reference element load]; otherwise, <c>False</c>.
        /// </value>
        /// <remarks>
        ///     If the Host has not been loaded, the name will not be resolved.
        ///     In that case, delay the resolution and track that fact with this property.
        /// </remarks>
        private bool PendingReferenceElementLoad { get; set; }

        /// <summary>
        ///     Gets or sets the resolved object.
        /// </summary>
        /// <value>
        ///     The resolved object.
        /// </value>
        private DependencyObject ResolvedObject { get; set; }

        /// <summary>
        ///     Determines whether [is element loaded] [the specified element].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///     <c>true</c> if [is element loaded] [the specified element]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsElementLoaded(FrameworkElement element)
        {
            return element.IsLoaded;
        }

        /// <summary>
        ///     Gets the actual name scope reference.
        /// </summary>
        /// <param name="initialReferenceElement">The initial reference element.</param>
        /// <returns>The name scope reference.</returns>
        private FrameworkElement GetActualNameScopeReference(FrameworkElement initialReferenceElement)
        {
            var nameScopeReference = initialReferenceElement;

            if (this.IsNameScope(initialReferenceElement))
            {
                nameScopeReference = initialReferenceElement.Parent as FrameworkElement ?? nameScopeReference;
            }

            return nameScopeReference;
        }

        /// <summary>
        ///     Determines whether [is name scope] [the specified framework element].
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns>
        ///     <c>true</c> if [is name scope] [the specified framework element]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsNameScope(FrameworkElement frameworkElement)
        {
            var parentElement = frameworkElement.Parent as FrameworkElement;
            var resolvedInParentScope = parentElement?.FindName(this.Name);
            return resolvedInParentScope != null;
        }

        /// <summary>
        ///     Called when [name scope reference element changed].
        /// </summary>
        /// <param name="oldNameScopeReference">The old name scope reference.</param>
        private void OnNameScopeReferenceElementChanged(FrameworkElement oldNameScopeReference)
        {
            if (this.PendingReferenceElementLoad)
            {
                oldNameScopeReference.Loaded -= this.OnNameScopeReferenceLoaded;
                this.PendingReferenceElementLoad = false;
            }

            this.HasAttempedResolve = false;
            this.UpdateObjectFromName(this.Object);
        }

        /// <summary>
        ///     Called when [name scope reference loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnNameScopeReferenceLoaded(object sender, RoutedEventArgs e)
        {
            this.PendingReferenceElementLoad = false;
            this.NameScopeReferenceElement.Loaded -= this.OnNameScopeReferenceLoaded;
            this.UpdateObjectFromName(this.Object);
        }

        /// <summary>
        ///     Called when [object changed].
        /// </summary>
        /// <param name="oldTarget">The old target.</param>
        /// <param name="newTarget">The new target.</param>
        private void OnObjectChanged(DependencyObject oldTarget, DependencyObject newTarget) =>
            this.ResolvedElementChanged?.Invoke(this, new NameResolvedEventArgs(oldTarget, newTarget));

        /// <summary>
        ///     Attempts to update the resolved object from the name within the context of the namescope reference element.
        /// </summary>
        /// <param name="oldObject">The old resolved object.</param>
        /// <remarks>
        ///     Resets the existing target and attempts to resolve the current TargetName from the
        ///     context of the current Host. If it cannot resolve from the context of the Host, it will
        ///     continue up the visual tree until it resolves. If it has not resolved it when it reaches
        ///     the root, it will set the Target to null and write a warning message to Debug output.
        /// </remarks>
        private void UpdateObjectFromName(DependencyObject oldObject)
        {
            DependencyObject newObject = null;

            // clear the cache
            this.ResolvedObject = null;

            if (this.NameScopeReferenceElement != null)
            {
                if (!IsElementLoaded(this.NameScopeReferenceElement))
                {
                    // We had a debug message here, but it seems like too common a scenario
                    this.NameScopeReferenceElement.Loaded += this.OnNameScopeReferenceLoaded;
                    this.PendingReferenceElementLoad = true;
                    return;
                }

                // update the target
                if (!string.IsNullOrEmpty(this.Name))
                {
                    var nameScopeElement = this.ActualNameScopeReferenceElement;
                    if (nameScopeElement != null)
                    {
                        newObject = nameScopeElement.FindName(this.Name) as DependencyObject;
                    }

                    if (newObject == null)
                    {
                        Debug.WriteLine(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "Unable To Resolve Target Name {0} Warning Message",
                                this.Name));
                    }
                }
            }

            this.HasAttempedResolve = true;

            // note that this.Object will be null if it doesn't resolve
            this.ResolvedObject = newObject;
            if (oldObject != this.Object)
            {
                // this.Source may not be newTarget, if TargetName is null or empty
                this.OnObjectChanged(oldObject, this.Object);
            }
        }
    }
}