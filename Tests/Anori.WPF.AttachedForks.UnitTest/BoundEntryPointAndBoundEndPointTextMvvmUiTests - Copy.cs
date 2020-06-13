using System.Threading.Tasks;
using Anori.WPF.AttachedAncestorProperties.GuiTest;
using Anori.WPF.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace Anori.WPF.AttachedAncestorProperties.UnitTest
{
    [TestClass]
    public class BoundEntryPointAndBoundEndPointTextMvvmEndPointTextBindingUiTests : UITestSessionBase
    {
        private const string Entrypoint = "EntryPoint";
        private const string Endpoint = "EndPoint";

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EntryPoint_Test()
        {
            var entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "");

            var endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "");

            session.FindElementByAccessibilityId(Entrypoint).SendKeys("Test");

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "Test");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "Test");

            session.FindElementByAccessibilityId(Entrypoint).SendKeys(" Text");

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "Test Text");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "Test Text");

            session.FindElementByAccessibilityId(Entrypoint).Clear();

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "");
        }

        [TestMethod]
        public async Task AttachedAncestorProperty_CheckText_EndPoint_Test()
        {
            var entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "");

            var endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "");

            session.FindElementByAccessibilityId(Endpoint).SendKeys("Test");

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "Test");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "Test");

            session.FindElementByAccessibilityId(Endpoint).SendKeys(" Text");

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "Test Text");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "Test Text");

            session.FindElementByAccessibilityId(Endpoint).Clear();

            entryPoint = session.FindElementByAccessibilityId(Entrypoint).Text;
            Assert.AreEqual(entryPoint, "");

            endPoint = session.FindElementByAccessibilityId(Endpoint).Text;
            Assert.AreEqual(endPoint, "");
        }



        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
            SetContent(() => new BoundEntryPointAndBoundEndPointTextMVVMView{DataContext = new SimpleAttachedTextBindingViewModel()});
        }

        [ClassCleanup]
        public static void ClassCleanup() => TearDown();
    }
}
