// -----------------------------------------------------------------------
// <copyright file="TwoBoundEntryPointAndBoundEndPointTextControlUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TwoBoundEntryPointAndBoundEndPointTextControlUiTests : UiTestSessionBase
    {
        private const string EndPointA = "EndPointA";

        private const string EndPointB = "EndPointB";

        private const string EntryPointA = "EntryPointA";

        private const string EntryPointB = "EntryPointB";

        [ClassCleanup]
        public static void ClassCleanup() => TearDown();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) => Setup(context);

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EndPointA_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual(entryPointA.Text, "");
            Assert.AreEqual(entryPointB.Text, "");
            Assert.AreEqual(endPointA.Text,   "");
            Assert.AreEqual(endPointB.Text,   "");

            endPointA.SendKeys("Test");
            Assert.AreEqual(entryPointA.Text, "Test");
            Assert.AreEqual(entryPointB.Text, "");
            Assert.AreEqual(endPointA.Text,   "Test");
            Assert.AreEqual(endPointB.Text,   "");

            endPointA.SendKeys(" Text");
            Assert.AreEqual(entryPointA.Text, "Test Text");
            Assert.AreEqual(entryPointB.Text, "");
            Assert.AreEqual(endPointA.Text,   "Test Text");
            Assert.AreEqual(endPointB.Text,   "");

            endPointA.Clear();
            Assert.AreEqual(entryPointA.Text, "");
            Assert.AreEqual(entryPointB.Text, "");
            Assert.AreEqual(endPointA.Text,   "");
            Assert.AreEqual(endPointB.Text,   "");
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EndPointAAndB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("",     entryPointB.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("",     endPointB.Text);

            endPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("",          entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("",          endPointB.Text);

            endPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointB.SendKeys("Test");
            Assert.AreEqual("",     entryPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("",     endPointA.Text);
            Assert.AreEqual("Test", endPointB.Text);

            endPointB.SendKeys(" Text");
            Assert.AreEqual("",          entryPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("",          endPointA.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            endPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EndPointB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointB.SendKeys("Test");
            Assert.AreEqual("",     entryPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("",     endPointA.Text);
            Assert.AreEqual("Test", endPointB.Text);

            endPointB.SendKeys(" Text");
            Assert.AreEqual("",          entryPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("",          endPointA.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            endPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPointA_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("",     entryPointB.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("",     endPointB.Text);

            entryPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("",          entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("",          endPointB.Text);

            entryPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPointAAndB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("",     entryPointB.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("",     endPointB.Text);

            entryPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("",          entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("",          endPointB.Text);

            entryPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointB.SendKeys("Test");
            Assert.AreEqual("",     entryPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("",     endPointA.Text);
            Assert.AreEqual("Test", endPointB.Text);

            entryPointB.SendKeys(" Text");
            Assert.AreEqual("",          entryPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("",          endPointA.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            entryPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPointB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointB.SendKeys("Test");
            Assert.AreEqual("",     entryPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("",     endPointA.Text);
            Assert.AreEqual("Test", endPointB.Text);

            entryPointB.SendKeys(" Text");
            Assert.AreEqual("",          entryPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("",          endPointA.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            entryPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new TwoBoundEntryPointAndBoundEndPointTextControlView());
    }
}
