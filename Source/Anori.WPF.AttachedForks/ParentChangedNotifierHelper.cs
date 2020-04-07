// -----------------------------------------------------------------------
// <copyright file="ParentChangedNotifierHelper.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Anori.WPF.Extensions;
using JetBrains.Annotations;

namespace Anori.WPF.AttachedForks
{
    public static class ParentChangedNotifierHelper
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
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public delegate bool TryGetFunc<T1, TResult>(
            in DependencyObject dependencyObject,
            in T1 parameter1,
            out TResult result);

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
        public static T GetValueOrRegisterParentNotifier<T>(
            this DependencyObject target,
            TryGetFunc<T> getFunc,
            Action<DependencyObject> parentChangedAction,
            ParentNotifiers parentNotifiers)
        {
            return GetValueOrRegisterParentNotifier(target, getFunc, parentChangedAction, parentNotifiers, out _);
        }

        /// <summary>
        ///     Checks the type.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        private static bool CheckType(this DependencyObject dependencyObject)
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
        ///     Gets the value or register sourceObject notifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="tryGetPropertyValue">The get function.</param>
        /// <param name="parentChangedAction">The sourceObject changed action.</param>
        /// <param name="parentNotifiers">The sourceObject notifiers.</param>
        /// <param name="sourceObject">The dependency object.</param>
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
            [CanBeNull] out DependencyObject sourceObject)
        {
            T result = default(T);
            sourceObject = target;
            if (sourceObject == null)
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

            return WalkTreeUp(target, tryGetPropertyValue, parentChangedAction, parentNotifiers, out sourceObject);
        }

        /// <summary>
        ///     Gets the value or register parent notifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The parent changed action.</param>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="sourceObject">The source object.</param>
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
            [CanBeNull] out DependencyObject sourceObject)
        {
            T result = default(T);
            sourceObject = target;
            if (sourceObject == null)
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

            return WalkTreeUp<T>(target, property, parentChangedAction, parentNotifiers, out sourceObject);
            //            return WalkTreeUp(target, property, parentChangedAction, parentNotifiersRegister, out sourceObject);
        }

        /// <summary>
        ///     Loops the specified target.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="tryGetPropertyValue">The get function.</param>
        /// <param name="parentChangedAction">The sourceObject changed action.</param>
        /// <param name="parentNotifiers">The sourceObject notifiers.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] TryGetFunc<T> tryGetPropertyValue,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject source)
        {
            T result;
            source = null;
            DependencyObject dependencyObject = target;

            do
            {
                bool hasResult = tryGetPropertyValue(dependencyObject, out result);

                if (hasResult && result != null)
                {
                    parentNotifiers.UnregisterParentNotifier(target);
                }

                if (dependencyObject.CheckType())
                {
                    break;
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    source = dependencyObject;
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

        /// <summary>
        ///     Walks the tree up.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The parent changed action.</param>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] Action<DependencyObject> parentChangedAction,
            [NotNull] ParentNotifiers parentNotifiers,
            [CanBeNull] out DependencyObject dependencyObject)
        {
            dependencyObject = target;

            do
            {
                bool hasResult = dependencyObject.HasDependencyProperty(property);

                if (hasResult)
                {
                    parentNotifiers.UnregisterParentNotifier(target);
                }

                if (dependencyObject.CheckType())
                {
                    break;
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    break;
                }

                if (parent == null)
                {
                    parentNotifiers.RegisterParentNotifier(target, dependencyObject, parentChangedAction);
                    break;
                }

                dependencyObject = parent;
            } while (true);

            return dependencyObject.GetValueSync<T>(property);
        }

        /// <summary>
        ///     Loops the tree up.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentNotifiersRegister">The parent notifiers register.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        [CanBeNull]
        private static T WalkTreeUp<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] INotifierRegister parentNotifiersRegister,
            [CanBeNull] out DependencyObject dependencyObject)
        {
            dependencyObject = target;

            do
            {
                bool hasResult = dependencyObject.HasDependencyProperty(property);

                if (hasResult)
                {
                    parentNotifiersRegister.Remove(target);
                }

                if (dependencyObject.CheckType())
                {
                    break;
                }

                if (!dependencyObject.TryGetParent(out DependencyObject parent))
                {
                    break;
                }

                if (hasResult)
                {
                    break;
                }

                if (parent == null)
                {
                    parentNotifiersRegister.Add(target, dependencyObject);
                    break;
                }

                dependencyObject = parent;
            } while (true);

            return dependencyObject.GetValueSync<T>(property);
        }

        /// <summary>
        ///     Unregisters the parent notifier.
        /// </summary>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <param name="target">The target.</param>
        private static void UnregisterParentNotifier([NotNull] this ParentNotifiers parentNotifiers,
            [NotNull] DependencyObject target)
        {
            if (parentNotifiers.ContainsKey(target))
            {
                parentNotifiers.Remove(target);
            }
        }

        /// <summary>
        ///     Registers the sourceObject notifier.
        /// </summary>
        /// <param name="parentNotifiers">The sourceObject notifiers.</param>
        /// <param name="target">The target.</param>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="parentChangedAction">The sourceObject changed action.</param>
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

            void OnParentChangedHandler()
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

            ParentChangedNotifier changedNotifier = new ParentChangedNotifier(frameworkElement, OnParentChangedHandler);

            parentNotifiers.Add(target, changedNotifier);
        }

        /// <summary>
        ///     Tries the get sourceObject.
        /// </summary>
        /// <param name="target">The dependency object.</param>
        /// <param name="parent">The sourceObject.</param>
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
        ///     Tries to get a value that is stored somewhere in the visual tree above this <see cref="DependencyObject" />.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="target">The <see cref="DependencyObject" />.</param>
        /// <param name="getFunction">The function that gets the value from a <see cref="DependencyObject" />.</param>
        /// <returns>The value, if possible.</returns>
        public static T GetValue<T>(this DependencyObject target, Func<DependencyObject, T> getFunction)
        {
            T result = default(T);

            if (target == null)
            {
                return result;
            }

            DependencyObject depObj = target;

            while (result == null)
            {
                // Try to get the value using the provided GetFunction.
                result = getFunction(depObj);

                // Try to get the sourceObject using the visual tree helper. This may fail on some occations.
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

                // Assign the sourceObject to the current DependencyObject and start the next iteration.
                depObj = depObjParent;
            }

            return result;
        }

        /// <summary>
        ///     Tries to get a value from a <see cref="DependencyProperty" /> that is stored somewhere in the visual tree above
        ///     this <see cref="DependencyObject" />.
        ///     If this is not available, it will register a <see cref="ParentChangedNotifier" /> on the last element.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="target">The <see cref="DependencyObject" />.</param>
        /// <param name="property">A <see cref="DependencyProperty" /> that will be read out.</param>
        /// <param name="parentChangedAction">The notification action on the change event of the Parent property.</param>
        /// <param name="parentNotifiers">A dictionary of already registered notifiers.</param>
        /// <returns>The value, if possible.</returns>
        [CanBeNull]
        public static T GetValueOrRegisterParentNotifier<T>(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [NotNull] Action<DependencyObject> parentChangedAction,
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

            if (parentNotifiers == null)
            {
                throw new ArgumentNullException(nameof(parentNotifiers));
            }

            return target.GetValueOrRegisterParentNotifier<T>(
                property,
                parentChangedAction,
                parentNotifiers,
                out _);
            //return target.GetValueOrRegisterParentNotifier(
            //    (in DependencyObject target, out T result) =>
            //        target.TryGetValueSync(property, out result),
            //    parentChangedAction,
            //    parentNotifiersRegister);
        }

        /// <summary>
        ///     Gets the value or register sourceObject notifier x.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The sourceObject changed action.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        /// <param name="parentNotifiers">The sourceObject notifiers.</param>
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

            bool OnParentChanged(in DependencyObject dependencyObject, out T result)
            {
                return dependencyObject.TryGetValueSync(property, out result);
            }

            T value = target.GetValueOrRegisterParentNotifier(
                (TryGetFunc<T>)OnParentChanged,
                parentChangedAction,
                parentNotifiers,
                out DependencyObject sourceObject);

            sourceObject?.AddValueChanged(property, valueChangedAction);
            return value;
        }

        /// <summary>
        ///     Gets the parent dependency object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentChangedAction">The parent changed action.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        /// <param name="parentNotifiers">The parent notifiers.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     property
        ///     or
        ///     parentChangedAction
        ///     or
        ///     valueChangedAction
        ///     or
        ///     parentNotifiers
        /// </exception>
        [CanBeNull]
        public static DependencyObject GetAttachedPropertyObject<T>(
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

            bool OnParentChanged(in DependencyObject dependencyObject, out T result)
            {
                return dependencyObject.TryGetValueSync(property, out result);
            }

            target.GetValueOrRegisterParentNotifier(
                (TryGetFunc<T>)OnParentChanged,
                parentChangedAction,
                parentNotifiers,
                out DependencyObject sourceObject);

            sourceObject?.AddValueChanged(property, valueChangedAction);
            return sourceObject;
        }

        /// <summary>
        ///     Gets the parent dependency object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     property
        ///     or
        ///     parentChangedAction
        ///     or
        ///     parentNotifiers
        /// </exception>
        [CanBeNull]
        public static object GetAttachedPropertyObject(
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

            target.WalkTreeUpAttachedPropertyObject(property, out object attachedProperty);
            return attachedProperty;
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
        public static DependencyObject GetAttachedPropertyHost(
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

            target.WalkTreeUpAttachedPropertyHost(property, out DependencyObject attachedProperty);
            return attachedProperty;
        }

        /// <summary>
        ///     Walks the tree up attached property object.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="attachedProperty">The attached property.</param>
        private static void WalkTreeUpAttachedPropertyObject(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [CanBeNull] out object attachedProperty)
        {
            DependencyObject dependencyObject = target;
            attachedProperty = null;
            do
            {
                if (dependencyObject.CheckType())
                {
                    break;
                }

                if (dependencyObject.TryGetAttachedProperty(property, out attachedProperty))
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
        /// <param name="attachedPropertyHost">The attached property host.</param>
        /// <returns></returns>
        private static bool WalkTreeUpAttachedPropertyHost(
            [NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property,
            [CanBeNull] out DependencyObject attachedPropertyHost)
        {
            DependencyObject dependencyObject = target;
            attachedPropertyHost = null;
            do
            {
                if (dependencyObject.CheckType())
                {
                    break;
                }

                bool hasResult = dependencyObject.HasDependencyProperty(property);
                if (hasResult)
                {
                    if (target != dependencyObject)
                    {
                        attachedPropertyHost = dependencyObject;
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
        ///     Gets the sourceObject in the visual or logical tree.
        /// </summary>
        /// <param name="target">The dependency object.</param>
        /// <param name="isVisualTree">True for visual tree, false for logical tree.</param>
        /// <returns>The sourceObject, if available.</returns>
        [CanBeNull]
        public static DependencyObject GetParent([NotNull] this DependencyObject target, bool isVisualTree)
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
        ///     Gets the sourceObject internal.
        /// </summary>
        /// <param name="target">The dep object.</param>
        /// <param name="isVisualTree">if set to <c>true</c> [is visual tree].</param>
        /// <returns></returns>
        [CanBeNull]
        private static DependencyObject GetParentInternal([NotNull] DependencyObject target, bool isVisualTree)
        {
            return isVisualTree ? VisualTreeHelper.GetParent(target) : LogicalTreeHelper.GetParent(target);
        }
    }
}
