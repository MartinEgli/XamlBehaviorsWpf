// -----------------------------------------------------------------------
// <copyright file="SingleSetterStaticEntryPointAndOneWayEndPointTextBindingUiTests.cs" company="Anori Soft"
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.AttachedAncestorProperties.AutomatedUiTests
{
    using System.Threading.Tasks;

    using Anori.WPF.AttachedAncestorProperties.ManualUiTests.SingleSetter.Static;
    using Anori.WPF.Testing;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

    [TestFixture, UserInterface]
    public class SingleSetterStaticEntryPointAndOneWayEndPointTextBindingUiTests : UiTestSessionBase
    {
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
        public static void ClassInitialize(TestContext context) => Setup(context);

        [Test]
        public async Task AttachedAncestorProperty_CheckText_Test()
        {
            var endPoint = Session.FindElementByAccessibilityId("TextBox1");
            Assert.AreEqual("Attached Text", endPoint.Text);
        }

        /// <summary>
        ///     Tests the initialize.
        /// </summary>
        [SetUp]
        public void TestInitialize() => SetContent(() => new StaticEntryPointAndOneWayEndPointTextView());
    }
}
