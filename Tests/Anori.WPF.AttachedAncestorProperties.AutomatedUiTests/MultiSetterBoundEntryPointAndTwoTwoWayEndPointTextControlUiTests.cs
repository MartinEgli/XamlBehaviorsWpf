﻿// -----------------------------------------------------------------------
// <copyright file="SingleSetterBoundEntryPointAndTwoTwoWayEndPointTextMvvmUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Element;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    [TestFixture, UserInterface]
    public class MultiSetterBoundEntryPointAndTwoTwoWayEndPointTextControlUiTests : UiTestSessionBase
    {
        /// <summary>
        /// The end point a1
        /// </summary>
        private const string EndPointA1 = "EndPointA1";
        /// <summary>
        /// The end point b1
        /// </summary>
        private const string EndPointB1 = "EndPointB1";

        /// <summary>
        /// The end point a2
        /// </summary>
        private const string EndPointA2 = "EndPointA2";
        /// <summary>
        /// The end point b2
        /// </summary>
        private const string EndPointB2 = "EndPointB2";

        /// <summary>
        /// The entry point a
        /// </summary>
        private const string EntryPointA = "EntryPointA";
        /// <summary>
        /// The entry point b
        /// </summary>
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
        ///     Attacheds the ancestor property check text end point1 test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EndPoint1_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA1  = Session.FindElementByAccessibilityId(EndPointA1);
            var endPointB1 = Session.FindElementByAccessibilityId(EndPointB1);
            var endPointA2  = Session.FindElementByAccessibilityId(EndPointA2);
            var endPointB2 = Session.FindElementByAccessibilityId(EndPointB2);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            endPointA1.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("Test", endPointA1.Text);
            Assert.AreEqual("Test", endPointA2.Text);

            endPointA1.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("Test Text", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            endPointA1.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text end point2 test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EndPoint2_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA1 = Session.FindElementByAccessibilityId(EndPointA1);
            var endPointB1 = Session.FindElementByAccessibilityId(EndPointB1);
            var endPointA2 = Session.FindElementByAccessibilityId(EndPointA2);
            var endPointB2 = Session.FindElementByAccessibilityId(EndPointB2);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            endPointA2.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("Test", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            endPointA2.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("Test Text", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            endPointA2.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPointA = Session.FindElementByAccessibilityId(EntryPointA);
            var entryPointB = Session.FindElementByAccessibilityId(EntryPointB);
            var endPointA1 = Session.FindElementByAccessibilityId(EndPointA1);
            var endPointB1 = Session.FindElementByAccessibilityId(EndPointB1);
            var endPointA2 = Session.FindElementByAccessibilityId(EndPointA2);
            var endPointB2 = Session.FindElementByAccessibilityId(EndPointB2);

            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            entryPointA.SendKeys("Test");
            Assert.AreEqual("Test", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("Test", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            entryPointA.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("Test Text", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("Test Text", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);

            entryPointA.Clear();
            Assert.AreEqual("", entryPointA.Text);
            Assert.AreEqual("", entryPointB.Text);
            Assert.AreEqual("", endPointA1.Text);
            Assert.AreEqual("", endPointB1.Text);
            Assert.AreEqual("", endPointA2.Text);
            Assert.AreEqual("", endPointB2.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() =>
            SetContent(
                () => new BoundEntryPointAndTwoTwoWayEndPointTextControlView());
    }
}