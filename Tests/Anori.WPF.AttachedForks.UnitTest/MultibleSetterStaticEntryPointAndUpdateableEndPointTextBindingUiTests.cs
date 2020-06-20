// -----------------------------------------------------------------------
// <copyright file="MultibleSetterStaticEntryPointAndUpdateableEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultibleSetterStaticEntryPointAndUpdateableEndPointTextBindingUiTests : UiTestSessionBase
    {
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
            var endPointA = Session.FindElementByAccessibilityId("EndPointA");
            var endPointB = Session.FindElementByAccessibilityId("EndPointB");
            Assert.AreEqual("Attached Text A", endPointA.Text);
            Assert.AreEqual("Attached Text B", endPointB.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndUpdateableEndPointTextView2());
    }
}
