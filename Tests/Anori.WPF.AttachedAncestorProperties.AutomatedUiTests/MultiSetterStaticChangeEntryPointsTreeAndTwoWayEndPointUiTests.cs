// -----------------------------------------------------------------------
// <copyright file="StaticChangeEntryPointsTreeAndBindableEndPointUiTests.cs" company="Anori Soft"
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
    public class MultiSetterStaticChangeEntryPointsTreeAndTwoWayEndPointUiTests : UiTestSessionBase
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
        private const string EndPointA = "EndPointA";
        private const string EndPointB = "EndPointB";

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
        /// <param name="context">The context.</param>
        [OneTimeSetUp]
        public static void ClassInitialize() => Setup();

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            var endPointA = Session.FindElementsByAccessibilityId(EndPointA);
            var endPointB = Session.FindElementsByAccessibilityId(EndPointB);
            var textsA = endPointA.Texts();
            var textsB = endPointB.Texts();
            var addpanel       = Session.FindElementByAccessibilityId(Addpanel);
            var removepanel    = Session.FindElementByAccessibilityId(Removepanel);
            var addsubpanel    = Session.FindElementByAccessibilityId(Addsubpanel);
            var removesubpanel = Session.FindElementByAccessibilityId(Removesubpanel);

            Assert.AreEqual(2,        textsA.Count);
            Assert.AreEqual("BorderA", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2,       textsA.Count);
            Assert.AreEqual("Panel", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            removepanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("BorderA", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addsubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("SubPanel1", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("SubPanel1", textsB[1]);

            removesubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("BorderA", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("Panel", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addsubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("SubPanel1", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("SubPanel1", textsB[1]);

            removesubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("Panel", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            removepanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("BorderA", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("Panel", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);

            addsubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("SubPanel1", textsA[0]);
            Assert.AreEqual("Panel", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("SubPanel1", textsB[1]);

            removepanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("SubPanel1", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("SubPanel1", textsB[1]);

            removesubpanel.Click();
            textsA = endPointA.Texts();
            textsB = endPointB.Texts();
            Assert.AreEqual(2, textsA.Count);
            Assert.AreEqual("BorderA", textsA[0]);
            Assert.AreEqual("BorderA", textsA[1]);
            Assert.AreEqual(2, textsB.Count);
            Assert.AreEqual("BorderB", textsB[0]);
            Assert.AreEqual("BorderB", textsB[1]);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticChangeEntryPointsTreeAndTwoWayEndPointView2());
    }
}
