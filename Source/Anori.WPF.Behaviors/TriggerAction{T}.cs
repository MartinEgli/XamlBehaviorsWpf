// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// </summary>
    /// <typeparam name="T">The type to which this action can be attached.</typeparam>
    /// <seealso cref="Anori.WPF.Behaviors.TriggerAction" />
    public abstract class TriggerAction<T> : TriggerAction where T : DependencyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerAction{T}"/> class.
        /// </summary>
        protected TriggerAction()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Gets the object to which this <see cref="Anori.WPF.Behaviors.TriggerAction&lt;T&gt;"/> is attached.
        /// </summary>
        /// <value>The associated object.</value>
        protected new T AssociatedObject
        {
            get
            {
                return (T)base.AssociatedObject;
            }
        }

        /// <summary>
        /// Gets the associated object type constraint.
        /// </summary>
        /// <value>The associated object type constraint.</value>
        protected sealed override Type AssociatedObjectTypeConstraint
        {
            get
            {
                return base.AssociatedObjectTypeConstraint;
            }
        }
    }
}
