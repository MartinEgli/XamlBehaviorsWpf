// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndBoundEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System.Threading.Tasks;

    using OpenQA.Selenium.Appium.Windows;

    [TestClass]
    public class SingleSetterStaticEntryPointAndBoundEndPointTextBindingUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     Classes the cleanup.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup() => TearDown();

        /// <summary>
        ///     Classes the initialize.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Setup(context);

        /// <summary>
        ///     Attacheds the ancestor property check text test.
        /// </summary>
        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            WindowsElement endpoint = Session.FindElementByAccessibilityId("EndPoint1");
            Assert.AreEqual("Attached Text", endpoint.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndBoundEndPointTextView());
    }
}
