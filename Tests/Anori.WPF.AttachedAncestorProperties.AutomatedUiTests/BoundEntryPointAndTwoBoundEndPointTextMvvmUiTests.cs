// -----------------------------------------------------------------------
// <copyright file="BoundEntryPointAndTwoBoundEndPointTextMvvmUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTest
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Mvvm;
    using Anori.WPF.AttachedAncestorProperties.AutomatedUiTests;
    using NUnit.Framework;

    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class BoundEntryPointAndTwoBoundEndPointTextMvvmUiTests : UiTestSessionBase
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
        ///     The entrypoint
        /// </summary>
        private const string Entrypoint = "EntryPoint";

        /// <summary>
        ///     Classes the cleanup.
        /// </summary>
        [OneTimeSetUp]
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
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint1  = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2  = Session.FindElementByAccessibilityId(Endpoint2);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);

            endPoint1.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint1.Text);
            Assert.AreEqual("Test", endPoint2.Text);

            endPoint1.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint1.Text);
            Assert.AreEqual("Test Text", endPoint2.Text);

            endPoint1.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text end point2 test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EndPoint2_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint1  = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2  = Session.FindElementByAccessibilityId(Endpoint2);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);

            endPoint2.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint1.Text);
            Assert.AreEqual("Test", endPoint2.Text);

            endPoint2.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint1.Text);
            Assert.AreEqual("Test Text", endPoint2.Text);

            endPoint2.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint1  = Session.FindElementByAccessibilityId(Endpoint1);
            var endPoint2  = Session.FindElementByAccessibilityId(Endpoint2);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);

            entryPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint1.Text);
            Assert.AreEqual("Test", endPoint2.Text);

            entryPoint.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint1.Text);
            Assert.AreEqual("Test Text", endPoint2.Text);

            entryPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint1.Text);
            Assert.AreEqual("", endPoint2.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() =>
            SetContent(
                () => new BoundEntryPointAndTwoBoundEndPointTextMvvmView
                          {
                              DataContext = new SimpleAttachedTextBindingViewModel()
                          });
    }
}
