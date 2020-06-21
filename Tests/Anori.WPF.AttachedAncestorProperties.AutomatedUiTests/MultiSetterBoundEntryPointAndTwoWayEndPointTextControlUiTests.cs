// -----------------------------------------------------------------------
// <copyright file="BoundEntryPointAndBoundEndPointTextControlUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Element;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

    /// <summary>
    /// </summary>
    /// <seealso cref="UiTestSessionBase" />
    [TestFixture, UserInterface]
    public class MultiSetterBoundEntryPointAndTwoWayEndPointTextControlUiTests : UiTestSessionBase
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
        /// <param name="context">The context.</param>
        [OneTimeSetUp]
        public static void ClassInitialize(TestContext context) => Setup(context);

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
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            endPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
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
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("Test", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);


            endPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointB.Text);

            
            endPointB.SendKeys("Test");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);


            endPointB.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("Test Text", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointB.Text);

            endPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
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
            Assert.AreEqual("Test", entryPointB.Text);
            Assert.AreEqual("Test", endPointB.Text);


            endPointB.SendKeys(" Text");
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", endPointA.Text);
            Assert.AreEqual("Test Text", entryPointB.Text);
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
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(EntryPointA);
            var endPoint   = Session.FindElementByAccessibilityId(EndPointA);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);

            entryPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);

            entryPoint.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint.Text);

            entryPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new BoundEntryPointAndTwoWayEndPointTextControlView());
    }
}
