using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using JetBrains.Annotations;
using OpenQA.Selenium.Appium.Windows;

namespace Anori.WPF.Testing
{
    public static class WindowsElementExtensions
    {
        public static IReadOnlyList<string> Texts([NotNull] this IReadOnlyCollection<WindowsElement> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            return elements.Select(i => i.Text).ToList();
        }

        public static ToggleState? ToggleState([NotNull] this WindowsElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var value = element.GetAttribute("Toggle.ToggleState");
            if (value == null)
            {
                return null;
            }

            if (!Enum.TryParse(value, true, out ToggleState state))
            {
                return null;
            }

            return state;
        }
    }
}
