// -----------------------------------------------------------------------
// <copyright file="DataBindingHelper.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Conditions
{
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    ///     Helper class for managing binding expressions on dependency objects.
    /// </summary>
    public static class DataBindingHelper
    {
        /// <summary>
        ///     The dependencies property cache
        /// </summary>
        private static readonly Dictionary<Type, IList<DependencyProperty>> DependenciesPropertyCache =
            new Dictionary<Type, IList<DependencyProperty>>();

        /// <summary>
        ///     Ensure that all DP on an action with binding expressions are
        ///     up to date. DataTrigger fires during data binding phase. Since
        ///     actions are children of the trigger, any bindings on the action
        ///     may not be up-to-date. This routine is called before the action
        ///     is invoked in order to guarantee that all bindings are up-to-date
        ///     with the most current data.
        /// </summary>
        /// <param name="target">The property object.</param>
        /// <exception cref="ArgumentNullException">target is null.</exception>
        public static void EnsureDataBindingUpToDateOnMembers([NotNull] DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (!DependenciesPropertyCache.TryGetValue(target.GetType(), out var properties))
            {
                properties = new List<DependencyProperty>();
                var type = target.GetType();

                while (type != null)
                {
                    var fieldInfos = type.GetFields();

                    foreach (var fieldInfo in fieldInfos)
                    {
                        if (!fieldInfo.IsPublic || fieldInfo.FieldType != typeof(DependencyProperty))
                        {
                            continue;
                        }

                        if (fieldInfo.GetValue(null) is DependencyProperty property)
                        {
                            properties.Add(property);
                        }
                    }

                    type = type.BaseType;
                }

                // Cache the list of DP for performance gain
                DependenciesPropertyCache[target.GetType()] = properties;
            }

            if (properties == null)
            {
                return;
            }

            foreach (var property in properties)
            {
                EnsureBindingUpToDate(target, property);
            }
        }

        ///// <summary>
        /////     Ensures that all binding expression on actions are up to date
        ///// </summary>
        ///// <param name="trigger">The trigger.</param>
        ///// <exception cref="ArgumentNullException">trigger is null.</exception>
        // public static void EnsureDataBindingOnActionsUpToDate([NotNull] TriggerBase trigger)
        // {
        // if (trigger == null)
        // {
        // throw new ArgumentNullException(nameof(trigger));
        // }

        // // Update the bindings on the actions.
        // foreach (var action in trigger.Actions)
        // {
        // EnsureDataBindingUpToDateOnMembers(action);
        // }
        // }

        /// <summary>
        ///     This helper function ensures that, if a dependency property on a dependency object
        ///     has a binding expression, the binding expression is up-to-date.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     property
        /// </exception>
        public static void EnsureBindingUpToDate(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var binding = BindingOperations.GetBindingExpression(target, property);
            binding?.UpdateTarget();
        }
    }
}
