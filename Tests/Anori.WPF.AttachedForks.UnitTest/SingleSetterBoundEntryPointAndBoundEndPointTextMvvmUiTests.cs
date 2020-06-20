// -----------------------------------------------------------------------
// <copyright file="SingleSetterBoundEntryPointAndBoundEndPointTextMvvmUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    using Anori.WPF.AttachedAncestorProperties.GuiTest;
    using Anori.WPF.AttachedAncestorProperties.GuiTest.SingleSetter.Mvvm;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    using System.Threading.Tasks;

    using OpenQA.Selenium.Appium.Windows;

    [TestClass]
    public class SingleSetterBoundEntryPointAndBoundEndPointTextMvvmUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     The endpoint
        /// </summary>
        private const string EndPoint = "EndPoint";

        /// <summary>
        ///     The entrypoint
        /// </summary>
        private const string EntryPoint = "EntryPoint";

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

        /// <summary>
        ///     Attacheds the ancestor property check text end point test.
        /// </summary>
        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EndPoint_Test()
        {
            SimpleAttachedTextBindingViewModel viewModel =
                Session.Harness.ContentDataContext as SimpleAttachedTextBindingViewModel;
            Assert.IsNotNull(viewModel);

            WindowsElement entryPoint = Session.FindElementByAccessibilityId(EntryPoint);
            WindowsElement endPoint = Session.FindElementByAccessibilityId(EndPoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
            Assert.AreEqual(null, viewModel.Text);

            entryPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);
            Assert.AreEqual("Test", viewModel.Text);

            entryPoint.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint.Text);
            Assert.AreEqual("Test Text", viewModel.Text);

            entryPoint.SendKeys(Keys.Backspace + Keys.Backspace);
            Assert.AreEqual("Test Te", entryPoint.Text);
            Assert.AreEqual("Test Te", endPoint.Text);
            Assert.AreEqual("Test Te", viewModel.Text);

            entryPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
            Assert.AreEqual("", viewModel.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text entry point test.
        /// </summary>
        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            SimpleAttachedTextBindingViewModel viewModel =
                Session.Harness.ContentDataContext as SimpleAttachedTextBindingViewModel;
            Assert.IsNotNull(viewModel);

            WindowsElement entryPoint = Session.FindElementByAccessibilityId(EntryPoint);
            WindowsElement endPoint = Session.FindElementByAccessibilityId(EndPoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
            Assert.AreEqual(null, viewModel.Text);

            entryPoint.SendKeys("Test");
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);
            Assert.AreEqual("Test", viewModel.Text);

            entryPoint.SendKeys(" Text");
            Assert.AreEqual("Test Text", entryPoint.Text);
            Assert.AreEqual("Test Text", endPoint.Text);
            Assert.AreEqual("Test Text", viewModel.Text);

            entryPoint.Clear();
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
            Assert.AreEqual("", viewModel.Text);
        }

        /// <summary>
        ///     Attacheds the ancestor property check text MVVM test.
        /// </summary>
        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_MVVM_Test()
        {
            SimpleAttachedTextBindingViewModel viewModel =
                Session.Harness.ContentDataContext as SimpleAttachedTextBindingViewModel;
            Assert.IsNotNull(viewModel);

            WindowsElement entryPoint = Session.FindElementByAccessibilityId(EntryPoint);
            WindowsElement endPoint = Session.FindElementByAccessibilityId(EndPoint);

            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);

            viewModel.Text = "Test";
            Assert.AreEqual("Test", entryPoint.Text);
            Assert.AreEqual("Test", endPoint.Text);

            viewModel.Text = "TestTest";
            Assert.AreEqual("TestTest", entryPoint.Text);
            Assert.AreEqual("TestTest", endPoint.Text);

            viewModel.Text = "";
            Assert.AreEqual("", entryPoint.Text);
            Assert.AreEqual("", endPoint.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() =>
            SetContent(
                () => new BoundEntryPointAndBoundEndPointTextMVVMView
                      {
                          DataContext = new SimpleAttachedTextBindingViewModel()
                      });
    }
}
