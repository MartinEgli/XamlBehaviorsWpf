// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndTwoWayEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using OpenQA.Selenium.Appium.Windows;

    using System.Threading.Tasks;


    [TestFixture]
    [UserInterface]
    public class SingleSetterStaticEntryPointAndTwoWayEndPointTextBindingUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndTwoWayEndPointTextView());

        /// <summary>
        ///     Attacheds the ancestor property check text test.
        /// </summary>
        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            WindowsElement endpoint = Session.FindElementByAccessibilityId("EndPoint1");
            Assert.AreEqual("Attached Text", endpoint.Text);
        }
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
    }
}
