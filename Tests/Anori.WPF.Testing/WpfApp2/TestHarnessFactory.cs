using System;

namespace Anori.WPF.Testing
{
    public class TestHarnessFactory
    {
        private readonly Func<ITestHarness> creator;

        public TestHarnessFactory(Func<ITestHarness> creator)
        {
            this.creator = creator;
        }

        public ITestHarness CreateHarness()
        {
            return this.creator();
        }
    }
}
