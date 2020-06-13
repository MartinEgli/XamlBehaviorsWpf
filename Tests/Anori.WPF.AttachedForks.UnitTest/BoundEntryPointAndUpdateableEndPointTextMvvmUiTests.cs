// -----------------------------------------------------------------------
// <copyright file="BoundEntryPointAndUpdateableEndPointTextMvvmUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.GuiTest;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    [TestClass]
    public class BoundEntryPointAndUpdateableEndPointTextMvvmUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     The endpoint
        /// </summary>
        private const string Endpoint = "EndPoint";

        /// <summary>
        ///     The entrypoint
        /// </summary>
        private const string Entrypoint = "EntryPoint";

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
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint   = Session.FindElementByAccessibilityId(Endpoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);

            endPoint.SendKeys("Test");
            Assert.AreEqual("",     entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);

            endPoint.SendKeys(" Text");
            Assert.AreEqual("",          entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint.Text);

            endPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_Entry_And_EndPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint   = Session.FindElementByAccessibilityId(Endpoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);

            entryPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);

            endPoint.SendKeys(Keys.End + " AAA");
            Assert.AreEqual("Test",     entryPoint.Text);
            Assert.AreEqual("Test AAA", endPoint.Text);

            entryPoint.SendKeys(" BBB");
            Assert.AreEqual("Test BBB", entryPoint.Text);
            Assert.AreEqual("Test BBB", endPoint.Text);

            endPoint.Clear();
            Assert.AreEqual("Test BBB", entryPoint.Text);
            Assert.AreEqual("",         endPoint.Text);

            entryPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPoint = Session.FindElementByAccessibilityId(Entrypoint);
            var endPoint   = Session.FindElementByAccessibilityId(Endpoint);

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
        [TestInitialize]
        public void TestInitialize() =>
            SetContent(
                () => new BoundEntryPointAndUpdateableEndPointTextMVVMView
                          {
                              DataContext = new SimpleAttachedTextBindingViewModel()
                          });
    }
}
