// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventToTargetBehavior{T}.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    /// <summary>
    ///     DependencyPropertyChangedEventToTargetBehavior class
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <seealso cref="Anori.WPF.Behaviors.DependencyPropertyChangedEventToTargetBehavior" />
    public abstract class
        DependencyPropertyChangedEventToTargetBehavior<T> : DependencyPropertyChangedEventToTargetBehavior
        where T : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangedEventToTargetBehavior{T}" /> class.
        /// </summary>
        protected DependencyPropertyChangedEventToTargetBehavior()
            : base(typeof(T))
        {
        }

        /// <summary>
        ///     Gets the target object. If TargetName is not set or cannot be resolved, defaults to the AssociatedObject.
        /// </summary>
        /// <value>The target.</value>
        /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
        protected new T Target => (T)base.Target;

        /// <summary>
        ///     Called when the target changes.
        /// </summary>
        /// <param name="oldTarget">The old target.</param>
        /// <param name="newTarget">The new target.</param>
        /// <remarks>
        ///     This function should be overriden in derived classes to hook and unhook functionality from the changing source
        ///     objects.
        /// </remarks>
        protected override sealed void OnTargetChangedImpl(object oldTarget, object newTarget)
        {
            base.OnTargetChangedImpl(oldTarget, newTarget);
            this.OnTargetChanged(oldTarget as T, newTarget as T);
        }

        /// <summary>
        ///     Called when the target property changes.
        /// </summary>
        /// <remarks>Override this to hook and unhook functionality on the specified Target, rather than the AssociatedObject.</remarks>
        /// <param name="oldTarget">The old target.</param>
        /// <param name="newTarget">The new target.</param>
        protected virtual void OnTargetChanged(T oldTarget, T newTarget)
        {
        }
    }
}