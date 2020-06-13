using System;
using System.Windows;
using System.Windows.Interop;
using JetBrains.Annotations;

namespace Anori.WPF.Testing
{
    public static class WindowExtensions
    {
        public static string AppTopLevelWindow([NotNull] this Window window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            if(window.Dispatcher.CheckAccess())
            {
                return new WindowInteropHelper(window).Handle.ToString("X");

            } else
            {
               return window.Dispatcher.Invoke(() => new WindowInteropHelper(window).Handle.ToString("X"));
            }
        }
    }
}
