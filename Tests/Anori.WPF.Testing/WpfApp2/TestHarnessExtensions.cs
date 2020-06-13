using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Anori.WPF.Testing
{
    public static class TestHarnessExtensions
    {
        public static TestSession CreateSession(this ITestHarness testHarness, string windowsApplicationDriverUrl)
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("appTopLevelWindow", testHarness.AppTopLevelWindow);
            return new TestSession(testHarness,
                new WindowsDriver<WindowsElement>(new Uri(windowsApplicationDriverUrl), appCapabilities));
        }
    }
}
