using System;
using System.Reflection;
using System.Windows;

namespace Anori.WPF.Extensions
{
    public static class DependencyPropertyExtensions
    {
        public static DependencyProperty GetDependencyProperty(this Type type, String name)
        {
            while (type != null)
            {
                FieldInfo fieldInfo = type.GetField(name);
                if (fieldInfo == null)
                {
                    type = type.BaseType;
                    continue;
                }

                if (!fieldInfo.IsPublic || fieldInfo.FieldType != typeof(DependencyProperty))
                {
                    type = type.BaseType;
                    continue;
                }

                if (fieldInfo.GetValue(null) is DependencyProperty property)
                {
                    return property;
                }

                type = type.BaseType;
            }

            return null;
        }

        public static DependencyProperty GetInternalDependencyProperty(this Type type, String name)
        {
            const BindingFlags bindingFlags = BindingFlags.Static |
                                              BindingFlags.NonPublic |
                                              BindingFlags.DeclaredOnly;
            while (type != null)
            {
                var fieldInfo = type.GetField(name, bindingFlags);
                if (fieldInfo == null)
                {
                    type = type.BaseType;
                    continue;
                }

                if (fieldInfo.IsPublic || fieldInfo.FieldType != typeof(DependencyProperty))
                {
                    type = type.BaseType;
                    continue;
                }

                if (fieldInfo.GetValue(null) is DependencyProperty property)
                {
                    return property;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
