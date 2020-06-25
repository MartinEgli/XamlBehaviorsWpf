// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticChangeEntryPointsTreeAndOneWayEndPointUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using OpenQA.Selenium.Appium.Windows;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    [UserInterface]
    public class SingleSetterStaticChangeEntryPointsTreeAndOneWayEndPointUiTests : UiTestSessionBase
    {
        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticChangeEntryPointsTreeAndOneWayEndPointView());

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            IReadOnlyCollection<WindowsElement> endpoint = Session.FindElementsByAccessibilityId(Endpoint);
            WindowsElement addpanel = Session.FindElementByAccessibilityId(Addpanel);
            WindowsElement removepanel = Session.FindElementByAccessibilityId(Removepanel);
            WindowsElement addsubpanel = Session.FindElementByAccessibilityId(Addsubpanel);
            WindowsElement removesubpanel = Session.FindElementByAccessibilityId(Removesubpanel);

            IReadOnlyList<string> texts = endpoint.Texts();
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

        private const string Addpanel = "AddPanel";

        private const string Addsubpanel = "AddSubPanel";

        private const string Endpoint = "EndPoint";

        private const string Removepanel = "RemovePanel";

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
