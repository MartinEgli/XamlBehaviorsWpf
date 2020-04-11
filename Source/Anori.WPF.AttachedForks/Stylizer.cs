using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Anori.WPF.Behaviors;
using Anori.WPF.Extensions;

namespace Anori.WPF.AttachedForks
{
    public static class Stylizer
    {
        /// <summary>
        ///     The setters property
        /// </summary>
        public static readonly DependencyProperty SettersProperty = DependencyProperty.RegisterAttached(
            "Setters",
            typeof(SetterCollection),
            typeof(Stylizer),
            new FrameworkPropertyMetadata(OnSettersChanged));


        /// <summary>
        ///     Gets or sets a value indicating whether to run as if in design mode.
        /// </summary>
        /// <value>
        ///     <c>True</c> if [should run in design mode]; otherwise, <c>False</c>.
        /// </value>
        /// <remarks>Not to be used outside unit tests.</remarks>
        internal static bool ShouldRunInDesignMode { get; set; }

        private static void OnSettersChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var oldCollection = (SetterCollection) args.OldValue;
            var newCollection = (SetterCollection) args.NewValue;

            if (oldCollection != newCollection)
            {
                if (oldCollection == newCollection) return;

                var style = GetStyle(dependencyObject);

                if (!(newCollection is SetterCollection setterCollection))
                {
                    if (style == null) return;

                    //  style.Setters.Add(new AttachedForkSetter());
                    return;
                }

                foreach (var creator in setterCollection)
                {
                    creator.Create(dependencyObject);
                }
            }
        }

        private static Style GetStyle(DependencyObject obj)
        {
            return ((FrameworkElement)obj).Style;
        }

        public static SetterCollection GetSetters(DependencyObject obj)
        {
            return (SetterCollection) obj.GetValue(SettersProperty);
        }

        public static void SetSetters(DependencyObject element, SetterCollection value)
        {
            element.SetValue(SettersProperty, value);
        }
    }

    /// <summary>
    ///     Represents a collection of IAttachedObject with a shared AssociatedObject and provides change notifications to its
    ///     contents when that AssociatedObject changes.
    /// </summary>
    public abstract class AttachableCollection<T> : FreezableCollection<T>, IAttachedObject
        where T : DependencyObject, IAttachedObject
    {
        private DependencyObject associatedObject;

        private Collection<T> snapshot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttachableCollection&lt;T&gt;" /> class.
        /// </summary>
        /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
        internal AttachableCollection()
        {
            INotifyCollectionChanged notifyCollectionChanged = this;
            notifyCollectionChanged.CollectionChanged += this.OnCollectionChanged;

            this.snapshot = new Collection<T>();
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
        ///     Called immediately after the collection is attached to an AssociatedObject.
        /// </summary>
        protected abstract void OnAttached();

        /// <summary>
        ///     Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected abstract void OnDetaching();

        /// <summary>
        ///     Called when a new item is added to the collection.
        /// </summary>
        /// <param name="item">The new item.</param>
        internal abstract void ItemAdded(T item);

        /// <summary>
        ///     Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The removed item.</param>
        internal abstract void ItemRemoved(T item);

        [Conditional("DEBUG")]
        private void VerifySnapshotIntegrity()
        {
            var isValid = this.Count == this.snapshot.Count;
            if (isValid)
                for (var i = 0; i < this.Count; i++)
                    if (this[i] != this.snapshot[i])
                    {
                        isValid = false;
                        break;
                    }

            Debug.Assert(isValid, "ReferentialCollection integrity has been compromised.");
        }

        /// <exception cref="InvalidOperationException">Cannot add the instance to a collection more than once.</exception>
        private void VerifyAdd(T item)
        {
            if (this.snapshot.Contains(item))
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "DuplicateItemInCollectionExceptionMessage",
                        typeof(T).Name,
                        this.GetType().Name));
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                // We fix the snapshot to mirror the structure, even if an exception is thrown. This may not be desirable.
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                        }
                        finally
                        {
                            this.snapshot.Insert(this.IndexOf(item), item);
                        }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }

                    foreach (T item in e.NewItems)
                        try
                        {
                            this.VerifyAdd(item);
                            this.ItemAdded(item);
                        }
                        finally
                        {
                            this.snapshot.Insert(this.IndexOf(item), item);
                        }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        this.ItemRemoved(item);
                        this.snapshot.Remove(item);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (var item in this.snapshot) this.ItemRemoved(item);

                    this.snapshot = new Collection<T>();
                    foreach (var item in this)
                    {
                        this.VerifyAdd(item);
                        this.ItemAdded(item);
                    }

                    break;

                case NotifyCollectionChangedAction.Move:
                default:
                    Debug.Fail("Unsupported collection operation attempted.");
                    break;
            }
#if DEBUG
            this.VerifySnapshotIntegrity();
#endif
        }

        #region IAttachedObject Members

        /// <summary>
        ///     Gets the associated object.
        /// </summary>
        /// <value>The associated object.</value>
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
                if (this.AssociatedObject != null) throw new InvalidOperationException();

                if (Stylizer.ShouldRunInDesignMode
                    || !(bool) this.GetValue(DesignerProperties.IsInDesignModeProperty))
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

        #endregion IAttachedObject Members
    }
}
