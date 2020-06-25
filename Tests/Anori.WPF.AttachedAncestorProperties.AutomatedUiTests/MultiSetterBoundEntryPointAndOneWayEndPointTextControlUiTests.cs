// -----------------------------------------------------------------------
// <copyright file="BoundEntryPointAndBoundEndPointTextControlUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Element;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    /// <summary>
    /// </summary>
    /// <seealso cref="UiTestSessionBase" />
    [TestFixture, UserInterface]
    public class MultiSetterBoundEntryPointAndOneWayEndPointTextControlUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     The end point
        /// </summary>
        private const string EndPointA = "EndPointA";
        private const string EndPointB = "EndPointB";

        /// <summary>
        ///     The entry point
        /// </summary>
        private const string EntryPointA = "EntryPointA";
        private const string EntryPointB = "EntryPointB";

        /// <summary>
        ///     Classes the cleanup.
        /// </summary>
        [OneTimeTearDown]
        public static void ClassCleanup() => TearDown();

        /// <summary>
        ///     Classes the initialize.
        /// </summary>
        [OneTimeSetUp]
        public static void ClassInitialize() => Setup();

        /// <summary>
        ///     Attacheds the ancestor property check text end point test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EndPointA_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointA.SendKeys("Test");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            endPointA.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            endPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);
        }
        [Test]

        public async Task AttachedAncestorProperty_CheckText_EndPointAAndB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointA.SendKeys("Test");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            endPointA.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            
            endPointB.SendKeys("Test");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);


            endPointB.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            endPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);


            endPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        [Test]
        public async Task AttachedAncestorProperty_CheckText_EndPointB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            endPointB.SendKeys("Test");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);


            endPointB.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);


            endPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPointA_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            entryPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);
        }


        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPointB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointB.SendKeys("Test");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);

            entryPointB.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            entryPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
        }

        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPointAAndB_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA = Session.FindElementByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementByAccessibilityId(EndPointB);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            entryPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            entryPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            entryPointB.SendKeys("Test");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);

            entryPointB.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            entryPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            entryPointB.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new BoundEntryPointAndOneWayEndPointTextControlView2());
    }
}
