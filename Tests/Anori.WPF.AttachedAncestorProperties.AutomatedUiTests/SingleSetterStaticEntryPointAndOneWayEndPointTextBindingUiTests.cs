// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndOneWayEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using OpenQA.Selenium.Appium.Windows;

    using System.Threading.Tasks;

    [TestFixture]
    [UserInterface]
    public class SingleSetterStaticEntryPointAndOneWayEndPointTextBindingUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndOneWayEndPointTextView());

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            WindowsElement endPoint = Session.FindElementByAccessibilityId("TextBox1");
            Assert.AreEqual("Attached Text", endPoint.Text);
        }
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
    }
}
