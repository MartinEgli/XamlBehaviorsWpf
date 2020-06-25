// -----------------------------------------------------------------------
// <copyright file="DependencyObjectExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Markup.Primitives;

namespace Anori.WPF.Extensions
{
    #region

    using JetBrains.Annotations;
    using System;
    using System.Windows;
    using System.Windows.Media;

    #endregion

    /// <summary>
    ///     Dependency Object Extensions
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        ///     Gets the root.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        public static UIElement GetRoot([NotNull] this DependencyObject current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            UIElement uiElement = null;
            for (; current != null; current = VisualTreeHelper.GetParent(current))
            {
                uiElement = current as UIElement;
            }

            return uiElement;
        }

        /// <summary>
        ///     Gets the value thread-safe.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The value.
        /// </returns>
        public static bool TryGetValueSync<T>(this DependencyObject obj, DependencyProperty property, out T value)
        {
            value = default;
            if (!obj.HasDependencyProperty(property))
            {
                return false;
            }

            value = obj.GetValueSync<T>(property);
            if (value != null)
            {
                return true;
            }
            return true;

           // return false;
        }

        /// <summary>
        ///     Gets the value synchronize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static T GetValueSync<T>(this DependencyObject obj, DependencyProperty property)
        {
            if (!obj.CheckAccess())
            {
                return (T)obj.Dispatcher.Invoke(() => obj.GetValue(property));
            }

            var value = obj.GetValue(property);
            return (T)value;

        }

        /// <summary>
        /// Gets the property object synchronize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static T GetPropertyObjectSync<T>(this DependencyObject obj, DependencyProperty property)
        {
            if (obj.CheckAccess())
            {
                var p = obj.FindDependencyProperty(property.Name + "Property");
                if (p != null)
                {
                    return (T)obj.GetValue(p);
                }
                return (T)obj.GetValue(property);
            }

            return (T)obj.Dispatcher.Invoke(() =>
            {
                var p = obj.FindDependencyProperty(property.Name + "Property");
                if (p != null)
                {
                    return (T)obj.GetValue(p);
                }
                return (T)obj.GetValue(property);
            });
        }

        /// <summary>
        ///     Sets the value thread-safe.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        public static void SetValueSync<T>(this DependencyObject obj, DependencyProperty property, T value)
        {
            if (obj.CheckAccess())
            {
                obj.SetValue(property, value);
            } else
            {
                obj.Dispatcher.Invoke(() => obj.SetValue(property, value));
            }
        }

        /// <summary>
        ///     Gets the window.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">current</exception>
        public static Window GetWindow([NotNull] this DependencyObject current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            return Window.GetWindow(current);
        }

        /// <summary>
        ///     Adds the value changed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="property">The property.</param>
        /// <param name="valueChangedAction">The value changed action.</param>
        public static void AddValueChanged<T>(
            [NotNull] this DependencyObject sourceObject,
            [NotNull] DependencyProperty property,
            [NotNull] Action<T> valueChangedAction)
        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException(nameof(sourceObject));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (valueChangedAction == null)
            {
                throw new ArgumentNullException(nameof(valueChangedAction));
            }

            void ValueChangedHandler(object sender, EventArgs args) =>
                valueChangedAction(((DependencyObject)sender).GetValueSync<T>(property));

            var desc = DependencyPropertyDescriptor.FromProperty(property, property.OwnerType);
            desc?.AddValueChanged(sourceObject, ValueChangedHandler);
        }

        public static void UpdateValueChanged(
            [NotNull] this DependencyObject sourceObject,
            [NotNull] DependencyProperty property,
            [NotNull] EventHandler valueChangedHandler)
        {
            RemoveValueChanged(sourceObject, property, valueChangedHandler);
            AddValueChanged(sourceObject, property, valueChangedHandler);
        }


        /// <summary>
        /// Adds the value changed.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="property">The property.</param>
        /// <param name="valueChangedHandler">The value changed handler.</param>
        /// <exception cref="ArgumentNullException">
        /// sourceObject
        /// or
        /// property
        /// or
        /// valueChangedHandler
        /// </exception>
        public static void AddValueChanged(
            [NotNull] this DependencyObject sourceObject,
            [NotNull] DependencyProperty property,
            [NotNull] EventHandler valueChangedHandler)
        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException(nameof(sourceObject));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (valueChangedHandler == null)
            {
                throw new ArgumentNullException(nameof(valueChangedHandler));
            }

            var desc = DependencyPropertyDescriptor.FromProperty(property, property.OwnerType);
            if (desc != null)
            {
                desc.AddValueChanged(sourceObject, valueChangedHandler);
                Debug.WriteLine("Add value changed handler form {0}", (object)desc.DisplayName);
            } else
            {
                throw new Exception("DependencyPropertyDescriptor is null!");
            }
        }

        /// <summary>
        /// Removes the value changed.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="property">The property.</param>
        /// <param name="valueChangedHandler">The value changed handler.</param>
        /// <exception cref="ArgumentNullException">
        /// target
        /// or
        /// property
        /// or
        /// valueChangedHandler
        /// </exception>
        public static void RemoveValueChanged(
            [NotNull] this DependencyObject sourceObject,
            [NotNull] DependencyProperty property,
            [NotNull] EventHandler valueChangedHandler)

        {
            if (sourceObject == null)
            {
                throw new ArgumentNullException(nameof(sourceObject));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (valueChangedHandler == null)
            {
                throw new ArgumentNullException(nameof(valueChangedHandler));
            }

            var desc = DependencyPropertyDescriptor.FromProperty(property, property.OwnerType);
            if (desc != null)
            {
                desc.RemoveValueChanged(sourceObject, valueChangedHandler);
                Debug.WriteLine("Remove value changed handler form {0}", (object)desc.DisplayName);
            } else
            {
                throw new Exception("DependencyPropertyDescriptor is null!");
            }
        }

        /// <summary>
        ///     Finds the dependency property.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     target
        ///     or
        ///     propertyName
        /// </exception>
        [CanBeNull]
        public static DependencyProperty FindDependencyProperty(
            [NotNull] this DependencyObject target,
            [NotNull] string propertyName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var type = target.GetType();
            var attribute = BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public;
            var info = type.GetField(propertyName, attribute);

            if (info == null)
            {
                info = type.GetField(propertyName + "Property", attribute);
                if (info == null)
                {
                    return null;
                }
            }

            return (DependencyProperty)info.GetValue(null);
        }

        [CanBeNull]
        public static DependencyProperty FindAttachedProperty(
            [NotNull] this DependencyObject target,
            [NotNull] string propertyName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            var attachedProperties = GetAttachedProperties(target);
            return attachedProperties.FirstOrDefault(a => a.Name == propertyName);
        }

        /// <summary>
        ///     Gets the dependency properties.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        [NotNull]
        [ItemNotNull]
        public static IEnumerable<DependencyProperty> GetDependencyProperties([NotNull] object element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return MarkupWriter.GetMarkupObjectFor(element)
                .Properties.Where(mp => mp.DependencyProperty != null)
                .Select(mp => mp.DependencyProperty)
                .ToList();
        }

        /// <summary>
        /// Gets the attached properties.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">element</exception>
        [NotNull]
        [ItemNotNull]
        public static IEnumerable<DependencyProperty> GetAttachedProperties([NotNull] object element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return MarkupWriter.GetMarkupObjectFor(element)
                .Properties.Where(mp => mp.IsAttached)
                .Select(mp => mp.DependencyProperty);
        }

        /// <summary>
        ///     Determines whether [has dependency property] [the specified property name].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns>
        ///     <c>true</c> if [has dependency property] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDependencyProperty(this DependencyObject target, string propName)
        {
            return FindDependencyProperty(target, propName) != null;
        }

        /// <summary>
        ///     Determines whether [has dependency property] [the specified property].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        ///     <c>true</c> if [has dependency property] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDependencyProperty([NotNull] this DependencyObject target,
            [NotNull] DependencyProperty property)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (property == null) throw new ArgumentNullException(nameof(property));
            var attachedProperties = GetAttachedProperties(target);
            var value = target.ReadLocalValue(property);
            return value != DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Determines whether [has dependency property] [the specified property].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [has dependency property] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// target
        /// or
        /// property
        /// </exception>
        public static bool HasDependencyProperty([NotNull] this DependencyObject target, [NotNull] DependencyProperty property, out object value)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (property == null) throw new ArgumentNullException(nameof(property));
            var obj = target.ReadLocalValue(property);
            if (obj != DependencyProperty.UnsetValue)
            {
                value = obj;
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// Tries the get attached properties.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The attached properties.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// target
        /// or
        /// property
        /// </exception>
        public static bool TryGetAttachedProperty([NotNull]this DependencyObject target, [NotNull]DependencyProperty property, [NotNull] out object value)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            value = target.ReadLocalValue(property);
            return value != null && value != DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Tries the get attached properties.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="attachedProperties">The attached properties.</param>
        /// <returns></returns>
        public static bool TryGetAttachedProperties([NotNull] this DependencyObject target, [NotNull, ItemNotNull] out IEnumerable<DependencyProperty> attachedProperties)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            attachedProperties = GetAttachedProperties(target);
            return attachedProperties.Any();
        }
    }
}
