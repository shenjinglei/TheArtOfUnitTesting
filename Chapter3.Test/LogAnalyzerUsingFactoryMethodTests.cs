using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3.Test
{
    [TestFixture]
    public class LogAnalyzerUsingFactoryMethodTests
    {
        [Test]
        public void OverrideTest()
        {
            FakeExtensionManager stub = new FakeExtensionManager();
            stub.WillBeValid = true;
            TestableLogAnalyzer logan = new TestableLogAnalyzer(stub);

            bool result = logan.IsValidLogFileName("File22.ext");

            Assert.IsTrue(result);

        }
    }

    class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;

        public TestableLogAnalyzer(IExtensionManager mgr)
        {
            Manager = mgr;
        }

        protected override IExtensionManager GetManager()
        {
            return Manager;
        }
    }
}
