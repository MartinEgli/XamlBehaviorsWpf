// -----------------------------------------------------------------------
// <copyright file="AttachedAncestorProperties.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter
{
    using System.Windows;

    public class AttachedAncestorProperties : AncestorPropertiesBase<AttachedAncestorProperties>
    {
        public static readonly DependencyProperty SetterAProperty = DependencyProperty.RegisterAttached(
            "SetterA",
            typeof(string),
            typeof(AttachedAncestorProperties),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SetterBProperty = DependencyProperty.RegisterAttached(
            "SetterB",
            typeof(string),
            typeof(AttachedAncestorProperties),
            new PropertyMetadata(default(string)));

        public static string GetSetterA(DependencyObject element)
        {
            return (string)element.GetValue(SetterAProperty);
        }

        public static string GetSetterB(DependencyObject element)
        {
            return (string)element.GetValue(SetterBProperty);
        }

        public static void SetSetterA(DependencyObject element, string value)
        {
            element.SetValue(SetterAProperty, value);
        }

        public static void SetSetterB(DependencyObject element, string value)
        {
            element.SetValue(SetterBProperty, value);
        }
    }
}
