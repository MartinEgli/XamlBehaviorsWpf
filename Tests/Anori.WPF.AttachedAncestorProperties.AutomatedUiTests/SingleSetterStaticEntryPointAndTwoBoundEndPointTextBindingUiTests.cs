// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndTwoBoundEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using System.Threading.Tasks;

    using OpenQA.Selenium.Appium.Windows;

    [TestFixture]
    [UserInterface]
    public class SingleSetterStaticEntryPointAndTwoBoundEndPointTextBindingUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     The endpoint1
        /// </summary>
        private const string Endpoint1 = "EndPoint1";

        /// <summary>
        ///     The endpoint2
        /// </summary>
        private const string Endpoint2 = "EndPoint2";

        /// <summary>
        ///     Classes the cleanup.
        /// </summary>
        [OneTimeTearDown]
        public static void ClassCleanup() => TearDown();

        /// <summary>
        ///     Classes the initialize.
        /// </summary>
        /// <param name="context">The context.</param>
        [OneTimeSetUp]
        public static void ClassInitialize() => Setup();

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            WindowsElement endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            WindowsElement endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);
            Assert.AreEqual("Attached Text", endPoint1.Text);
            Assert.AreEqual("Attached Text", endPoint2.Text);
        }

        [Test]
        public async Task AttachedAncestorProperty_ClearAndAddText_Test()
        {
            WindowsElement endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            WindowsElement endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);

            Assert.AreEqual("Attached Text", endPoint1.Text);
            Assert.AreEqual("Attached Text", endPoint2.Text);

            endPoint1.Clear();
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);

            endPoint1.SendKeys("AAA");
            Assert.AreEqual("AAA", endPoint1.Text);
            Assert.AreEqual("AAA", endPoint2.Text);

            endPoint2.Clear();
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);

            endPoint2.SendKeys("BBB");
            Assert.AreEqual("BBB", endPoint1.Text);
            Assert.AreEqual("BBB", endPoint2.Text);
        }

        [Test]
        public async Task AttachedAncestorProperty_ClearText_Test()
        {
            WindowsElement endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            WindowsElement endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);
            Assert.AreEqual("Attached Text", endPoint1.Text);
            Assert.AreEqual("Attached Text", endPoint2.Text);

            endPoint1.Clear();
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndTwoTwoWayEndPointTextView());
    }
}
