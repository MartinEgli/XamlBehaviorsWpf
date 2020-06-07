﻿namespace Anori.WPF.Behaviors.Extensions
{
    /// <summary>
    ///     Represents a trigger that can listen to an element other than its AssociatedObject.
    /// </summary>
    /// <typeparam name="T">The type that this trigger can be associated with.</typeparam>
    /// <remarks>
    ///     EventTriggerBase extends TriggerBase to add knowledge of another object than the one it is attached to.
    ///     This allows a user to attach a Trigger/Action pair to one element and invoke the Action in response to a
    ///     change in another object somewhere else. Override OnSourceChanged to hook or unhook handlers on the source
    ///     element, and OnAttached/OnDetaching for the associated element. The type of the Source element can be
    ///     constrained by the generic type parameter. If you need control over the type of the
    ///     AssociatedObject, set a TypeConstraintAttribute on your derived type.
    /// </remarks>
    public abstract class DependencyPropertyChangedEventTriggerBase<T> : DependencyPropertyChangedEventTriggerBase
        where T : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangedEventTriggerBase{T}.
        /// </summary>
        protected DependencyPropertyChangedEventTriggerBase()
            : base(typeof(T))
        {
        }

        /// <summary>
        ///     Gets the resolved source. If <c ref="SourceName" /> is not set or cannot be resolved, defaults to AssociatedObject.
        /// </summary>
        /// <value>The resolved source object.</value>
        /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
        public new T Source
        {
            get
            {
                return (T)base.Source;
            }
        }

        /// <summary>
        /// Called when the source changes.
        /// </summary>
        /// <param name="oldSource">The old source.</param>
        /// <param name="newSource">The new source.</param>
        /// <remarks>
        /// This function should be overridden in derived classes to hook functionality to and unhook functionality from
        /// the changing source objects.
        /// </remarks>
        internal sealed override void OnSourceChangedImpl(object oldSource, object newSource)
        {
            base.OnSourceChangedImpl(oldSource, newSource);
            this.OnSourceChanged(oldSource as T, newSource as T);
        }

        /// <summary>
        ///     Called when the source property changes.
        /// </summary>
        /// <remarks>
        ///     Override this to hook functionality to and unhook functionality from the specified source, rather than the
        ///     AssociatedObject.
        /// </remarks>
        /// <param name="oldSource">The old source.</param>
        /// <param name="newSource">The new source.</param>
        protected virtual void OnSourceChanged(T oldSource, T newSource)
        {
        }
    }
}
