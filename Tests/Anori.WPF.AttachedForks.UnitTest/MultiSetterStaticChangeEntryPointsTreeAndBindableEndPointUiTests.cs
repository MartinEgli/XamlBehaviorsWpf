// -----------------------------------------------------------------------
// <copyright file="StaticChangeEntryPointsTreeAndBindableEndPointUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter;
    using Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter.Static;
    using Anori.WPF.Testing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StaticChangeEntryPointsTreeAndBindableEndPointView2 = Anori.WPF.AttachedAncestorProperties.GuiTest.MultiSetter.Static.StaticChangeEntryPointsTreeAndBindableEndPointView2;

    [TestClass]
    public class MultiSetterStaticChangeEntryPointsTreeAndBindableEndPointUiTests : UiTestSessionBase
    {
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
            var endpoint       = Session.FindElementsByAccessibilityId(Endpoint);
            var texts          = endpoint.Texts();
            var addpanel       = Session.FindElementByAccessibilityId(Addpanel);
            var removepanel    = Session.FindElementByAccessibilityId(Removepanel);
            var addsubpanel    = Session.FindElementByAccessibilityId(Addsubpanel);
            var removesubpanel = Session.FindElementByAccessibilityId(Removesubpanel);

            Assert.AreEqual(2,        texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,       texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,        texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,           texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Border",    texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,        texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,       texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,           texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Panel",     texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,       texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,        texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);

            addpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,       texts.Count);
            Assert.AreEqual("Panel", texts[0]);
            Assert.AreEqual("Panel", texts[1]);

            addsubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,           texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Panel",     texts[1]);

            removepanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,           texts.Count);
            Assert.AreEqual("SubPanel1", texts[0]);
            Assert.AreEqual("Border",    texts[1]);

            removesubpanel.Click();
            texts = endpoint.Texts();
            Assert.AreEqual(2,        texts.Count);
            Assert.AreEqual("Border", texts[0]);
            Assert.AreEqual("Border", texts[1]);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => SetContent(() => new StaticChangeEntryPointsTreeAndBindableEndPointView2());
    }
}
