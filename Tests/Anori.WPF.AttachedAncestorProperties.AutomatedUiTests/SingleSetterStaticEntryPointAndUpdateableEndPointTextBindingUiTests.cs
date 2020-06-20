// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndUpdateableEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;


    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StaticEntryPointAndUpdateableEndPointTextView = Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static.StaticEntryPointAndUpdateableEndPointTextView;

    [TestClass]
    public class SingleSetterStaticEntryPointAndUpdateableEndPointTextBindingUiTests : UiTestSessionBase
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
            var endPoint = Session.FindElementByAccessibilityId("TextBox1");
            Assert.AreEqual("Attached Text", endPoint.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndUpdateableEndPointTextView());
    }
}
