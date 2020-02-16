// -----------------------------------------------------------------------
// <copyright file="DataBindingHelper.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    ///     Helper class for managing binding expressions on dependency objects.
    /// </summary>
    internal static class DataBindingHelper
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
        public static void EnsureDataBindingUpToDateOnMembers(DependencyObject dependencyObject)
        {
            if (!DependenciesPropertyCache.TryGetValue(
                    dependencyObject.GetType(),
                    out IList<DependencyProperty> dependencyProperties))
            {
                dependencyProperties = new List<DependencyProperty>();
                Type type = dependencyObject.GetType();

                while (type != null)
                {
                    FieldInfo[] fieldInfos = type.GetFields();

                    foreach (FieldInfo fieldInfo in fieldInfos)
                    {
                        if (!fieldInfo.IsPublic || fieldInfo.FieldType != typeof(DependencyProperty))
                        {
                            continue;
                        }

                        if (fieldInfo.GetValue(null) is DependencyProperty property)
                        {
                            dependencyProperties.Add(property);
                        }
                    }

                    type = type.BaseType;
                }

                // Cache the list of DP for performance gain
                DependenciesPropertyCache[dependencyObject.GetType()] = dependencyProperties;
            }

            if (dependencyProperties == null)
            {
                return;
            }

            foreach (DependencyProperty property in dependencyProperties)
            {
                EnsureBindingUpToDate(dependencyObject, property);
            }
        }

        /// <summary>
        ///     Ensures that all binding expression on actions are up to date
        /// </summary>
        public static void EnsureDataBindingOnActionsUpToDate(TriggerBase<DependencyObject> trigger)
        {
            // Update the bindings on the actions.
            foreach (TriggerAction action in trigger.Actions)
            {
                EnsureDataBindingUpToDateOnMembers(action);
            }
        }

        /// <summary>
        ///     This helper function ensures that, if a dependency property on a dependency object
        ///     has a binding expression, the binding expression is up-to-date.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dependencyProperty"></param>
        public static void EnsureBindingUpToDate(DependencyObject target, DependencyProperty dependencyProperty)
        {
            BindingExpression binding = BindingOperations.GetBindingExpression(target, dependencyProperty);
            binding?.UpdateTarget();
        }
    }
}
