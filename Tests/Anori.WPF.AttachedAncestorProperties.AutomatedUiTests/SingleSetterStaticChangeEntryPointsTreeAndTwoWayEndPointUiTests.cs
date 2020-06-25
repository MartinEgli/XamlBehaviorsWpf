// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticChangeEntryPointsTreeAndTwoWayEndPointUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Collections.Generic;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using System.Threading.Tasks;

    using OpenQA.Selenium.Appium.Windows;

    [TestFixture]
    [UserInterface]
    public class SingleSetterStaticChangeEntryPointsTreeAndTwoWayEndPointUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticChangeEntryPointsTreeAndTwoWayEndPointView());

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            IReadOnlyCollection<WindowsElement> endpoint = Session.FindElementsByAccessibilityId(Endpoint);
            IReadOnlyList<string> texts = endpoint.Texts();
            WindowsElement addpanel = Session.FindElementByAccessibilityId(Addpanel);
            WindowsElement removepanel = Session.FindElementByAccessibilityId(Removepanel);
            WindowsElement addsubpanel = Session.FindElementByAccessibilityId(Addsubpanel);
            WindowsElement removesubpanel = Session.FindElementByAccessibilityId(Removesubpanel);

            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2, texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);
        }

        /// <summary>
        ///     The addpanel
        /// </summary>
        private const string Addpanel = "AddPanel";

        /// <summary>
        ///     The addsubpanel
        /// </summary>
        private const string Addsubpanel = "AddSubPanel";

        /// <summary>
        ///     The endpoint
        /// </summary>
        private const string Endpoint = "EndPoint";

        /// <summary>
        ///     The removepanel
        /// </summary>
        private const string Removepanel = "RemovePanel";

        /// <summary>
        ///     The removesubpanel
        /// </summary>
        private const string Removesubpanel = "RemoveSubPanel";

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
