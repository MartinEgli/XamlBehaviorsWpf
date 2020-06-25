// -----------------------------------------------------------------------
// <copyright file="MultiSetterStaticEntryPointAndBoundEndPointTextTwoWayUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.MultiSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    [TestFixture, UserInterface]
    public class MultiSetterStaticEntryPointAndTwoWayEndPointTextBindingUiTests : UiTestSessionBase
    {
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
            var endPointA = Session.FindElementByAccessibilityId("EndPointA");
            var endPointB = Session.FindElementByAccessibilityId("EndPointB");
            Assert.AreEqual("Attached Text A", endPointA.Text);
            Assert.AreEqual("Attached Text B", endPointB.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndTwoWayEndPointTextView2());
    }
}
