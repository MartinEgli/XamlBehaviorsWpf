// -----------------------------------------------------------------------
// <copyright file="StaticMultiEntryPointsAndBoundEndPointsBoolControlUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    using System.Threading.Tasks;
    using System.Windows.Automation;

    using Anori.WPF.AttachedAncestorProperties.GuiTest;
    using Anori.WPF.Testing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WPF.AttachedAncestorProperties.UnitTest.UiTestSessionBase" />
    [TestClass]
    public class StaticMultiEntryPointsAndBoundEndPointsBoolControlUiTests : UiTestSessionBase
    {
        private const string EndPointA = "EndPointA";

        private const string EndPointB = "EndPointB";

        private const string EndPointC = "EndPointC";

        private const string EntryPointA = "EntryPointA";

        private const string EntryPointB = "EntryPointB";

        private const string EntryPointC = "EntryPointC";

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
        public async Task AttachedAncestorProperty_CheckText_EndPoint_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var entryPointC = Session.FindElementByAccessibilityId(EntryPointC);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);
            var endPointC   = Session.FindElementByAccessibilityId(EndPointC);

            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            endPointA.Click();
            Assert.AreEqual(ToggleState.On,  entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            endPointA.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            endPointB.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.On,  entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            endPointB.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            endPointC.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.On,  entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointC.ToggleState());

            endPointC.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var entryPointC = Session.FindElementByAccessibilityId(EntryPointC);
            var endPointA   = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB   = Session.FindElementByAccessibilityId(EndPointB);
            var endPointC   = Session.FindElementByAccessibilityId(EndPointC);

            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            entryPointA.Click();
            Assert.AreEqual(ToggleState.On,  entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            entryPointA.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            entryPointB.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.On,  entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            entryPointB.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());

            entryPointC.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.On,  entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.On,  endPointC.ToggleState());

            entryPointC.Click();
            Assert.AreEqual(ToggleState.Off, entryPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, entryPointC.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointA.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointB.ToggleState());
            Assert.AreEqual(ToggleState.Off, endPointC.ToggleState());
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticMultiEntryPointsAndBoundEndPointsBoolControlView());
    }
}
