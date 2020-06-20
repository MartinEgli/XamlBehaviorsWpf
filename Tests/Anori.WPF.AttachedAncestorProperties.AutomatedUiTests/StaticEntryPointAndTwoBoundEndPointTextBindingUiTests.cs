// -----------------------------------------------------------------------
// <copyright file="StaticEntryPointAndTwoBoundEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StaticEntryPointAndTwoBoundEndPointTextBindingUiTests : UiTestSessionBase
    {
        private const string Endpoint1 = "EndPoint1";

        private const string Endpoint2 = "EndPoint2";

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

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            var endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);
            Assert.AreEqual("Attached Text", endPoint1.Text);
            Assert.AreEqual("Attached Text", endPoint2.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_ClearAndAddText_Test()
        {
            var endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);

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

        [TestMethod]
        public async Task AttachedAncestorProperty_ClearText_Test()
        {
            var endPoint1 = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2 = Session.FindElementByAccessibilityId(Endpoint2);
            Assert.AreEqual("Attached Text", endPoint1.Text);
            Assert.AreEqual("Attached Text", endPoint2.Text);

            endPoint1.Clear();
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndTwoBoundEndPointTextView());
    }
}
