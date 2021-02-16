// -----------------------------------------------------------------------
// <copyright file="BoundEntryPointAndBoundEndPointTextControlUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Element;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    /// <summary>
    /// </summary>
    /// <seealso cref="UiTestSessionBase" />
    [TestFixture, UserInterface]
    public class SingleSetterBoundEntryPointAndTwoWayEndPointTextControlUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     The end point
        /// </summary>
        private const string EndPoint = "EndPoint";

        /// <summary>
        ///     The entry point
        /// </summary>
        private const string EntryPoint = "EntryPoint";

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
        public async Task AttachedAncestorProperty_CheckText_EndPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(EntryPoint);
            var endPoint   = Session.FindElementByAccessibilityId(EndPoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);

            endPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);

            endPoint.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint.Text);

            endPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(EntryPoint);
            var endPoint   = Session.FindElementByAccessibilityId(EndPoint);

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
