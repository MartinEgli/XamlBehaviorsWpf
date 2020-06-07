// -----------------------------------------------------------------------
// <copyright file="BindingExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Anori.WPF.Behaviors
{
    /// <summary>
    /// </summary>
    public static class BindingExtensions
    {
        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="bindingBase">The binding base.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     bindingBase
        ///     or
        ///     source
        /// </exception>
        /// <exception cref="NotSupportedException">Failed to clone binding</exception>
        public static BindingBase CloneBindingBase([NotNull] this BindingBase bindingBase, [NotNull] object source)
        {
            if (bindingBase == null)
            {
                throw new ArgumentNullException(nameof(bindingBase));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (bindingBase)
            {
                case Binding binding:
                    {
                        return CloneBindingWithSourceImplementation(binding, source);
                    }
                case MultiBinding multiBinding:
                    {
                        return CloneMultiBindingWithSourceImplementation(multiBinding, source);
                    }
                case PriorityBinding priorityBinding:
                    {
                        return ClonePriorityBindingWithSourceImplementation(priorityBinding, source);
                    }
                default:
                    throw new NotSupportedException("Failed to clone binding");
            }
        }

        public static BindingBase CloneBindingBase([NotNull] this BindingBase bindingBase)
        {
            if (bindingBase == null)
            {
                throw new ArgumentNullException(nameof(bindingBase));
            }

            switch (bindingBase)
            {
                case Binding binding:
                    {
                        return CloneBindingImplementation(binding);
                    }
                case MultiBinding multiBinding:
                    {
                        return CloneMultiBindingImplementation(multiBinding);
                    }
                case PriorityBinding priorityBinding:
                    {
                        return ClonePriorityBindingImplementation(priorityBinding);
                    }
                default:
                    throw new NotSupportedException("Failed to clone binding");
            }
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     binding
        ///     or
        ///     source
        /// </exception>
        public static Binding CloneBinding([NotNull] this Binding binding, [NotNull] object source)
        {
            if (binding == null)
            {
                throw new ArgumentNullException(nameof(binding));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return CloneBindingWithSourceImplementation(binding, source);
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">binding</exception>
        public static Binding CloneBinding([NotNull] this Binding binding)
        {
            if (binding == null)
            {
                throw new ArgumentNullException(nameof(binding));
            }

            if (binding.Source != null && binding.Source != DependencyProperty.UnsetValue)
            {
                return CloneBindingWithSourceImplementation(binding, binding.Source);
            }

            if (binding.ElementName != null && !string.IsNullOrWhiteSpace(binding.ElementName))
            {
                return CloneBindingWithElementNameImplementation(binding, binding.ElementName);
            }

            if (binding.RelativeSource != null)
            {
                return CloneBindingWithRelativeSourceImplementation(binding, binding.RelativeSource);
            }

            return CloneBindingWithSourceImplementation(binding, DependencyProperty.UnsetValue);
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="relativeSource">The relative source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     binding
        ///     or
        ///     relativeSource
        /// </exception>
        public static Binding CloneBinding([NotNull] this Binding binding, [NotNull] RelativeSource relativeSource)
        {
            if (binding == null)
            {
                throw new ArgumentNullException(nameof(binding));
            }

            if (relativeSource == null)
            {
                throw new ArgumentNullException(nameof(relativeSource));
            }

            return CloneBindingWithRelativeSourceImplementation(binding, relativeSource);
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     binding
        ///     or
        ///     elementName
        /// </exception>
        public static Binding CloneBinding([NotNull] this Binding binding, [NotNull] string elementName)
        {
            if (binding == null)
            {
                throw new ArgumentNullException(nameof(binding));
            }

            if (elementName == null || string.IsNullOrWhiteSpace(elementName))
            {
                throw new ArgumentNullException(nameof(elementName));
            }

            return CloneBindingWithElementNameImplementation(binding, elementName);
        }

        /// <summary>
        ///     Clones the priority binding.
        /// </summary>
        /// <param name="priorityBinding">The priority binding.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static BindingBase ClonePriorityBindingWithSourceImplementation(PriorityBinding priorityBinding,
            object source)
        {
            PriorityBinding result = new PriorityBinding
            {
                BindingGroupName = priorityBinding.BindingGroupName,
                FallbackValue = priorityBinding.FallbackValue,
                StringFormat = priorityBinding.StringFormat,
                TargetNullValue = priorityBinding.TargetNullValue
            };

            foreach (BindingBase childBinding in priorityBinding.Bindings)
            {
                result.Bindings.Add(CloneBindingBase(childBinding, source));
            }

            return result;
        }

        private static BindingBase ClonePriorityBindingImplementation(PriorityBinding priorityBinding)
        {
            PriorityBinding result = new PriorityBinding
            {
                BindingGroupName = priorityBinding.BindingGroupName,
                FallbackValue = priorityBinding.FallbackValue,
                StringFormat = priorityBinding.StringFormat,
                TargetNullValue = priorityBinding.TargetNullValue
            };

            foreach (BindingBase childBinding in priorityBinding.Bindings)
            {
                result.Bindings.Add(CloneBindingBase(childBinding));
            }

            return result;
        }

        /// <summary>
        ///     Clones the multi binding.
        /// </summary>
        /// <param name="multiBinding">The multi binding.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static BindingBase CloneMultiBindingWithSourceImplementation(MultiBinding multiBinding, object source)
        {
            MultiBinding result = CloneMultiBindingSkeleton(multiBinding);

            foreach (ValidationRule validationRule in multiBinding.ValidationRules)
            {
                result.ValidationRules.Add(validationRule);
            }

            foreach (BindingBase childBinding in multiBinding.Bindings)
            {
                result.Bindings.Add(CloneBindingBase(childBinding, source));
            }

            return result;
        }

        private static BindingBase CloneMultiBindingImplementation(MultiBinding multiBinding)
        {
            MultiBinding result = CloneMultiBindingSkeleton(multiBinding);

            foreach (ValidationRule validationRule in multiBinding.ValidationRules)
            {
                result.ValidationRules.Add(validationRule);
            }

            foreach (BindingBase childBinding in multiBinding.Bindings)
            {
                result.Bindings.Add(CloneBindingBase(childBinding, multiBinding));
            }

            return result;
        }

        /// <summary>
        ///     Clones the multi binding skeleton.
        /// </summary>
        /// <param name="multiBinding">The multi binding.</param>
        /// <returns></returns>
        private static MultiBinding CloneMultiBindingSkeleton(MultiBinding multiBinding)
        {
            return new MultiBinding
            {
                BindingGroupName = multiBinding.BindingGroupName,
                Converter = multiBinding.Converter,
                ConverterCulture = multiBinding.ConverterCulture,
                ConverterParameter = multiBinding.ConverterParameter,
                FallbackValue = multiBinding.FallbackValue,
                Mode = multiBinding.Mode,
                NotifyOnSourceUpdated = multiBinding.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = multiBinding.NotifyOnTargetUpdated,
                NotifyOnValidationError = multiBinding.NotifyOnValidationError,
                StringFormat = multiBinding.StringFormat,
                TargetNullValue = multiBinding.TargetNullValue,
                UpdateSourceExceptionFilter = multiBinding.UpdateSourceExceptionFilter,
                UpdateSourceTrigger = multiBinding.UpdateSourceTrigger,
                ValidatesOnDataErrors = multiBinding.ValidatesOnDataErrors,
                ValidatesOnExceptions = multiBinding.ValidatesOnDataErrors
            };
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static Binding CloneBindingWithSourceImplementation(Binding binding, object source)
        {
            Binding result = CloneBindingSkeleton(binding);
            result.Source = source;

            foreach (ValidationRule validationRule in binding.ValidationRules)
            {
                result.ValidationRules.Add(validationRule);
            }

            return result;
        }

        /// <summary>
        ///     Clones the binding with element name implementation.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        private static Binding CloneBindingWithElementNameImplementation(Binding binding, string elementName)
        {
            Binding result = CloneBindingSkeleton(binding);
            result.ElementName = elementName;

            foreach (ValidationRule validationRule in binding.ValidationRules)
            {
                result.ValidationRules.Add(validationRule);
            }

            return result;
        }

        /// <summary>
        ///     Clones the binding with relative source implementation.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="relativeSource">The relative source.</param>
        /// <returns></returns>
        private static Binding CloneBindingWithRelativeSourceImplementation(
            Binding binding,
            RelativeSource relativeSource)
        {
            Binding result = CloneBindingSkeleton(binding);
            result.RelativeSource = relativeSource;

            foreach (ValidationRule validationRule in binding.ValidationRules)
            {
                result.ValidationRules.Add(validationRule);
            }

            return result;
        }

        /// <summary>
        ///     Creates the binding skeleton.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <returns></returns>
        private static Binding CloneBindingSkeleton(Binding binding)
        {
            return new Binding
            {
                //Source = source,
                AsyncState = binding.AsyncState,
                BindingGroupName = binding.BindingGroupName,
                BindsDirectlyToSource = binding.BindsDirectlyToSource,
                Converter = binding.Converter,
                ConverterCulture = binding.ConverterCulture,
                ConverterParameter = binding.ConverterParameter,
                //ElementName = binding.ElementName,
                FallbackValue = binding.FallbackValue,
                IsAsync = binding.IsAsync,
                Mode = binding.Mode,
                NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
                NotifyOnValidationError = binding.NotifyOnValidationError,
                Path = binding.Path,
                //RelativeSource = binding.RelativeSource,
                StringFormat = binding.StringFormat,
                TargetNullValue = binding.TargetNullValue,
                UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
                UpdateSourceTrigger = binding.UpdateSourceTrigger,
                ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
                ValidatesOnExceptions = binding.ValidatesOnExceptions,
                XPath = binding.XPath
            };
        }

        /// <summary>
        ///     Clones the binding.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <returns></returns>
        public static BindingBase CloneBindingImplementation(Binding binding)
        {
            return CloneBindingWithSourceImplementation(binding, binding.Source ?? DependencyProperty.UnsetValue);
        }
    }
}
