using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Anori.WPF.Testing
{
    public class TestSession
    {
        public ITestHarness Harness { get; }
        public WindowsDriver<WindowsElement> WindowsDriver { get; }

        internal TestSession(ITestHarness testHarness, WindowsDriver<WindowsElement> windowsWindowsDriver)
        {
            this.Harness = testHarness;
            this.WindowsDriver = windowsWindowsDriver;
        }

        public void Quit() => this.WindowsDriver?.Quit();

        public WindowsElement FindElementByAccessibilityId(string selector) => this.WindowsDriver?.FindElementByAccessibilityId(selector);
        public IReadOnlyCollection<WindowsElement> FindElementsByAccessibilityId(string selector) => this.WindowsDriver?.FindElementsByAccessibilityId(selector);
        public IReadOnlyCollection<WindowsElement> FindElementsByClassName(string className) => this.WindowsDriver?.FindElementsByClassName(className);
        //public IReadOnlyCollection<WindowsElement> FindElementsByClassName(string className) => this.WindowsDriver?.FindElements(By.ClassName(className));
    }
}
