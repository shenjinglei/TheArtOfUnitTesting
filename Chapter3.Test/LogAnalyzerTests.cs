namespace Chapter3.Test
{
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager myFakeManager = new FakeExtensionManager();
            myFakeManager.WillBeValid = true;

            ExtensionManagerFactory.SetManager(myFakeManager);
            LogAnalyzer log = new LogAnalyzer();

            bool result = log.IsValidLogFileName("short2.ext");

            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager myFakeManager = new FakeExtensionManager();
            myFakeManager.WillThrow = new Exception("This is fake.");
            ExtensionManagerFactory.SetManager(myFakeManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("anything.anyextension");

            Assert.False(result);
        }
    }

    public class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;
        public Exception? WillThrow = null;
        public bool IsValid(string fileName)
        {
            if(WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeValid;
        }
    }
}