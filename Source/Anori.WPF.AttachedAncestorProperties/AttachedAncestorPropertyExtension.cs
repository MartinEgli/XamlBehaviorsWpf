// -----------------------------------------------------------------------
// <copyright file="AttachedAncestorPropertyExtension.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedAncestorProperties
{
    public static class AttachedAncestorPropertyExtension
    {







        /// <summary>
        ///     TryGetFunc
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public delegate bool TryGetFunc<TResult>(in DependencyObject dependencyObject, out TResult result);

        /// <summary>
        /// Gets the attached property fork.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="shadowProperty">The shadow property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">target</exception>
        [CanBeNull]
        public static DependencyObject GetAncestor(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] DependencyProperty shadowProperty)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (setterProperty == null)
            {
                throw new ArgumentNullException(nameof(setterProperty));
            }

            if (shadowProperty == null)
            {
                throw new ArgumentNullException(nameof(shadowProperty));
            }

            var ancestor = target.GetAncestor(
                setterProperty,
                shadowProperty,
                out var s,
                out var h);

            return ancestor;
        }

        /// <summary>
        ///     Adds the value changed action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     property
        ///     or
        ///     valueChangedAction
        /// </exception>
        public static void AddValueChangedAction<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] EventHandler valueChangedAction)

        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (valueChangedAction == null)
            {
                throw new ArgumentNullException(nameof(valueChangedAction));
            }

            target?.AddValueChanged(property, valueChangedAction);
        }

        /// <summary>
        ///     Gets the attached host object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="shadowProperty">The shadow property.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="shadow">The shadow.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     SetterProperty
        ///     or
        ///     shadowProperty
        /// </exception>
        [CanBeNull]
        public static DependencyObject GetAncestor(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] DependencyProperty shadowProperty,
            [CanBeNull] out object setter,
            [CanBeNull] out object shadow)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (setterProperty == null)
            {
                throw new ArgumentNullException(nameof(setterProperty));
            }

            if (shadowProperty == null)
            {
                throw new ArgumentNullException(nameof(shadowProperty));
            }

            target.WalkTreeUpAttachedPropertyAncestor(setterProperty, shadowProperty, out DependencyObject ancestor,
                out setter, out shadow);
            return ancestor;
        }

        /// <summary>
        ///     Gets the attached host object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="shadowProperty">The shadow property.</param>
        /// <param name="setter">The setter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        ///     target
        ///     or
        ///     SetterProperty
        ///     or
        ///     shadowProperty
        /// </exception>
        [CanBeNull]
        public static DependencyObject GetAncestor(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] DependencyProperty shadowProperty,
            [CanBeNull] out object setter)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (setterProperty == null)
            {
                throw new ArgumentNullException(nameof(setterProperty));
            }

            if (shadowProperty == null)
            {
                throw new ArgumentNullException(nameof(shadowProperty));
            }

            target.WalkTreeUpAttachedPropertyAncestor(setterProperty, shadowProperty, out DependencyObject ancestor,
                out setter, out _);
            return ancestor;
        }

        /// <summary>
        ///     Gets the attached property host.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     property
        /// </exception>
        [CanBeNull]
        public static DependencyObject GetAncestor(
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

            target.WalkTreeUpAttachedPropertyAncestor(property, out DependencyObject ancestor);
            return ancestor;
        }

        /// <summary>
        ///     Gets the ancestor in the visual or logical tree.
        /// </summary>
        /// <param name="target">The dependency object.</param>
        /// <param name="isVisualTree">True for visual tree, false for logical tree.</param>
        /// <returns>The ancestor, if available.</returns>
        [CanBeNull]
        public static DependencyObject GetParent(
            [NotNull] this DependencyObject target,
            bool isVisualTree)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target.CheckAccess())
            {
                return GetParentInternal(target, isVisualTree);
            }

            return target.Dispatcher?.Invoke(() => GetParentInternal(target, isVisualTree));
        }

        /// <summary>
        ///     Tries to get a value that is stored somewhere in the visual tree above this <see cref="DependencyObject" />.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="target">The <see cref="DependencyObject" />.</param>
        /// <param name="getFunction">The function that gets the value from a <see cref="DependencyObject" />.</param>
        /// <returns>The value, if possible.</returns>
        public static T GetValue<T>(
            [NotNull] this DependencyObject target,
            [NotNull] Func<DependencyObject, T> getFunction)
        {
            T result = default;
            if (target == null)
            {
                return result;
            }

            if (getFunction == null)
            {
                return result;
            }

            DependencyObject depObj = target;

            while (result == null)
            {
                // Try to get the value using the provided GetFunction.
                result = getFunction(depObj);

                // Try to get the ancestor using the visual tree helper. This may fail on some occations.
                if (!(depObj is Visual) && !(depObj is Visual3D) && !(depObj is FrameworkContentElement))
                {
                    break;
                }

                DependencyObject depObjParent;

                if (depObj is FrameworkContentElement element)
                {
                    depObjParent = element.Parent;
                } else
                {
                    try
                    {
                        depObjParent = depObj.GetParent(true);
                    } catch
                    {
                        break;
                    }
                }

                // If this failed, try again using the Parent property (sometimes this is not covered by the VisualTreeHelper class :-P.
                if (depObjParent == null && depObj is FrameworkElement frameworkElement)
                {
                    depObjParent = frameworkElement.Parent;
                }

                if (result == null && depObjParent == null)
                {
                    break;
                }

                // Assign the ancestor to the current DependencyObject and start the next iteration.
                depObj = depObjParent;
            }

            return result;
        }

        /// <summary>
        ///     Tries to get a value that is stored somewhere in the visual tree above this <see cref="DependencyObject" />.
        ///     <para>If this is not available, it will register a <see cref="ParentChangedNotifier" /> on the last element.</para>
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="target">The <see cref="DependencyObject" />.</param>
        /// <param name="getFunc">The function that gets the value from a <see cref="DependencyObject" />.</param>
        /// <param name="parentChangedAction">The notification action on the change event of the Parent property.</param>
        /// <param name="parentNotifiers">A dictionary of already registered notifiers.</param>
        /// <returns>
        ///     The value, if possible.
        /// </returns>
        [CanBeNull]
        public static T GetValueOrRegisterParentNotifier<T>(
            [NotNull] this DependencyObject target,
            [NotNull] TryGetFunc<T> getFunc,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers)
        {
            return GetValueOrRegisterParentNotifier(target, getFunc, parentChangedAction, parentNotifiers, out _);
        }

        /// <summary>
        ///     Gets the value or register ancestor notifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="tryGetPropertyValue">The get function.</param>
        /// <param name="parentChangedAction">The ancestor changed action.</param>
        /// <param name="parentNotifiers">The ancestor notifiers.</param>
        /// <param name="ancestor">The dependency object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     tryGetPropertyValue
        ///     or
        ///     parentChangedAction
        ///     or
        ///     parentNotifiersRegister
        /// </exception>
        [CanBeNull]
        public static T GetValueOrRegisterParentNotifier<T>(
            [NotNull] this DependencyObject target,
            [NotNull] TryGetFunc<T> tryGetPropertyValue,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject ancestor)
        {
            T result = default;
            ancestor = target;
            if (ancestor == null)
            {
                return result;
            }

            if (tryGetPropertyValue == null)
            {
                throw new ArgumentNullException(nameof(tryGetPropertyValue));
            }

            if (parentChangedAction == null)
            {
                throw new ArgumentNullException(nameof(parentChangedAction));
            }

            if (parentNotifiers == null)
            {
                throw new ArgumentNullException(nameof(parentNotifiers));
            }

            return WalkTreeUp(target, tryGetPropertyValue, parentChangedAction, parentNotifiers, out ancestor);
        }

        /// <summary>
        ///     Gets the value or register parent notifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The parent changed action.</param>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="ancestor">The ancestor object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     parentChangedAction
        ///     or
        ///     parentNotifiers
        /// </exception>
        [CanBeNull]
        public static T GetValueOrRegisterParentNotifier<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject ancestor)
        {
            T result = default;
            ancestor = target;
            if (ancestor == null)
            {
                return result;
            }

            if (parentChangedAction == null)
            {
                throw new ArgumentNullException(nameof(parentChangedAction));
            }

            if (parentNotifiers == null)
            {
                throw new ArgumentNullException(nameof(parentNotifiers));
            }

            return WalkTreeUp<T>(target, property, parentChangedAction, parentNotifiers, out ancestor);
            //            return WalkTreeUp(target, property, parentChangedAction, parentNotifiersRegister, out ancestor);
        }

        /// <summary>
        ///     Gets the value or register ancestor notifier x.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The ancestor changed action.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        /// <param name="parentNotifiers">The ancestor notifiers.</param>
        /// <returns></returns>
        [CanBeNull]
        public static T GetValueOrRegisterParentNotifier<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] EventHandler valueChangedAction,
            [NotNull] ParentNotifiers parentNotifiers)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (parentChangedAction == null)
            {
                throw new ArgumentNullException(nameof(parentChangedAction));
            }

            if (valueChangedAction == null)
            {
                throw new ArgumentNullException(nameof(valueChangedAction));
            }

            if (parentNotifiers == null)
            {
                throw new ArgumentNullException(nameof(parentNotifiers));
            }

            bool onParentChanged(in DependencyObject dependencyObject, out T result)
            {
                return dependencyObject.TryGetValueSync(property, out result);
            }

            T value = target.GetValueOrRegisterParentNotifier(
                (TryGetFunc<T>)onParentChanged,
                parentChangedAction,
                parentNotifiers,
                out DependencyObject sourceObject);

            sourceObject?.UpdateValueChanged(property, valueChangedAction);
            return value;
        }

        /// <summary>
        ///     Checks the type.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        private static bool CheckType(
            [NotNull] this DependencyObject dependencyObject)
        {
            if (dependencyObject is ToolTip)
            {
                return true;
            }

            if (!(dependencyObject is Visual) && !(dependencyObject is Visual3D)
                                              && !(dependencyObject is FrameworkContentElement))
            {
                return true;
            }

            if (dependencyObject is Window)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Gets the ancestor internal.
        /// </summary>
        /// <param name="target">The dep object.</param>
        /// <param name="isVisualTree">if set to <c>true</c> [is visual tree].</param>
        /// <returns></returns>
        [CanBeNull]
        private static DependencyObject GetParentInternal([NotNull] DependencyObject target, bool isVisualTree)
        {
            return isVisualTree ? VisualTreeHelper.GetParent(target) : LogicalTreeHelper.GetParent(target);
        }

        /// <summary>
        ///     Registers the ancestor notifier.
        /// </summary>
        /// <param name="parentNotifiers">The ancestor notifiers.</param>
        /// <param name="target">The target.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="parentChangedAction">The ancestor changed action.</param>
        private static void RegisterParentNotifier(
            [NotNull] this ParentNotifiers parentNotifiers,
            [NotNull] DependencyObject target,
            [NotNull] DependencyObject dependencyObject,
            [NotNull] Action<DependencyObject> parentChangedAction)
        {
            if (!(dependencyObject is FrameworkElement frameworkElement) || parentNotifiers.ContainsKey(target))
            {
                return;
            }

            WeakReference weakTarget = new WeakReference(target);

            void onParentChangedHandler()
            {
                DependencyObject localTarget = (DependencyObject)weakTarget.Target;
                if (!weakTarget.IsAlive)
                {
                    return;
                }

                parentChangedAction(localTarget);
                parentNotifiers.UnregisterParentNotifier(localTarget);

                //    if (parentNotifiers.ContainsKey(localTarget))
                //    {
                //        parentNotifiers.Remove(localTarget);
                //    }
            }

            ParentChangedNotifier changedNotifier = new ParentChangedNotifier(frameworkElement, onParentChangedHandler);

            parentNotifiers.Add(target, changedNotifier);
        }

        /// <summary>
        ///     Tries the get ancestor.
        /// </summary>
        /// <param name="target">The dependency object.</param>
        /// <param name="parent">The ancestor.</param>
        /// <returns></returns>
        private static bool TryGetParent(
            [NotNull] this DependencyObject target,
            [CanBeNull] out DependencyObject parent)
        {
            if (target is FrameworkContentElement element)
            {
                parent = element.Parent;
            } else
            {
                try
                {
                    parent = target.GetParent(false);
                } catch
                {
                    parent = null;
                }
            }

            if (parent == null)
            {
                try
                {
                    parent = target.GetParent(true);
                } catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Unregisters the parent notifier.
        /// </summary>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="target">The target.</param>
        private static void UnregisterParentNotifier(
            [NotNull] this ParentNotifiers parentNotifiers,
            [NotNull] DependencyObject target)
        {
            if (parentNotifiers.ContainsKey(target))
            {
                parentNotifiers.Remove(target);
            }
        }

        /// <summary>
        ///     Loops the specified target.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="tryGetPropertyValue">The get function.</param>
        /// <param name="parentChangedAction">The ancestor changed action.</param>
        /// <param name="parentNotifiers">The ancestor notifiers.</param>
        /// <param name="ancestor">The ancestor.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] TryGetFunc<T> tryGetPropertyValue,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject ancestor)
        {
            T result;
            ancestor = null;
            DependencyObject dependencyObject = target;

            do
            {
                DebugLogWalk(dependencyObject);
                bool hasResult = tryGetPropertyValue(dependencyObject, out result);

                if (hasResult && result != null)
                {
                    parentNotifiers.UnregisterParentNotifier(target);
                }

                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    ancestor = dependencyObject;
                    break;
                }

                if (parent == null)
                {
                    parentNotifiers.RegisterParentNotifier(target, dependencyObject, parentChangedAction);
                    break;
                }

                dependencyObject = parent;
            } while (true);

            return result;
        }

        [Conditional("DEBUG")]
        private static void DebugLogWalk(DependencyObject dependencyObject)
        {
            if (dependencyObject is FrameworkElement frameworkElement)
            {
                if (!string.IsNullOrEmpty(frameworkElement.Name))
                {
                    Debug.WriteLine("Walk item name {1} type {0}", dependencyObject.GetType(), frameworkElement.Name);
                }
                else
                {
                    Debug.WriteLine("Walk item type {0}", dependencyObject.GetType());
                }
            }
            else
            {
                Debug.WriteLine("Walk item type {0}", dependencyObject.GetType());
            }
        }

        /// <summary>
        ///     Walks the tree up.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The parent changed action.</param>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="ancestor">The dependency object.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject ancestor)
        {
            ancestor = target;

            do
            {
                DebugLogWalk(ancestor);

                bool hasResult = ancestor.HasDependencyProperty(property);

                if (hasResult)
                {
                    parentNotifiers.UnregisterParentNotifier(target);
                }

                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                if (!ancestor.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    break;
                }

                if (parent == null)
                {
                    parentNotifiers.RegisterParentNotifier(target, ancestor, parentChangedAction);
                    break;
                }

                ancestor = parent;
            } while (true);

            return ancestor.GetValueSync<T>(property);
        }

        /// <summary>
        ///     Loops the tree up.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentNotifiersRegister">The parent notifiers register.</param>
        /// <param name="ancestor">The dependency object.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] INotifierRegister parentNotifiersRegister,
            [CanBeNull] out DependencyObject ancestor)
        {
            ancestor = target;

            do
            {
                bool hasResult = ancestor.HasDependencyProperty(property);

                if (hasResult)
                {
                    parentNotifiersRegister.Remove(target);
                }

                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                if (!ancestor.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    break;
                }

                if (parent == null)
                {
                    parentNotifiersRegister.Add(target, ancestor);
                    break;
                }

                ancestor = parent;
            } while (true);

            return ancestor.GetValueSync<T>(property);
        }

        /// <summary>
        ///     Walks the tree up attached property object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        private static void WalkTreeUpAttachedProperty(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [CanBeNull] out object value)
        {
            DependencyObject dependencyObject = target;
            do
            {
                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                if (dependencyObject.TryGetAttachedProperty(property, out value))
                {
                    break;
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (parent == null)
                {
                    break;
                }

                dependencyObject = parent;
            } while (true);
        }

        /// <summary>
        ///     Walks the tree up attached property host.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="ancestor">The attached property host.</param>
        /// <returns></returns>
        private static bool WalkTreeUpAttachedPropertyAncestor(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [CanBeNull] out DependencyObject ancestor)
        {
            DependencyObject dependencyObject = target;
            ancestor = null;
            do
            {
                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                bool hasResult = dependencyObject.HasDependencyProperty(property);
                if (hasResult)
                {
                    if (target != dependencyObject)
                    {
                        ancestor = dependencyObject;
                        return true;
                    }
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (parent == null)
                {
                    break;
                }

                dependencyObject = parent;
            } while (true);

            return false;
        }

        /// <summary>
        ///     Walks the tree up attached property setter host.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="setterProperty">The setter property.</param>
        /// <param name="shadowProperty">The shadow property.</param>
        /// <param name="ancestor">The attached property host.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="shadow">The shadow.</param>
        /// <returns></returns>
        private static bool WalkTreeUpAttachedPropertyAncestor(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty setterProperty,
            [NotNull] DependencyProperty shadowProperty,
            [CanBeNull] out DependencyObject ancestor,
            [CanBeNull] out object setter,
            [CanBeNull] out object shadow)
        {
            DependencyObject dependencyObject = target;
            ancestor = null;
            do
            {
                DebugLogWalk(dependencyObject);

                //if (ancestor.CheckType())
                //{
                //    break;
                //}

                bool hasResult = dependencyObject.HasDependencyProperty(setterProperty, out setter);
                if (hasResult)
                {
                    if (setter != null)
                    {
                        if (target != dependencyObject)
                        {
                            ancestor = dependencyObject;
                            shadow = dependencyObject.GetValue(shadowProperty);
                            return true;
                        }
                    } else
                    {
                        shadow = dependencyObject.GetValue(shadowProperty);
                        if (shadow != null)
                        {
                            if (target != dependencyObject)
                            {
                                ancestor = dependencyObject;
                                return true;
                            }
                        }
                    }
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (parent == null)
                {
                    break;
                }

                dependencyObject = parent;
            } while (true);

            shadow = null;
            setter = null;
            return false;
        }
    }
}
