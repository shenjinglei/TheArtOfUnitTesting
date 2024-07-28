using NSubstitute;

namespace Chapter5
{
    public class Tests2
    {
        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            FakeWebService mockWebService = new FakeWebService();
            FakeLogger stubLogger = new FakeLogger();
            stubLogger.WillThrow = new Exception("fake exception");

            var analyzer2 = new LogAnalyzer2(stubLogger, mockWebService);
            analyzer2.MinNameLength = 8;

            string tooShortName = "abc.txt";
            analyzer2.Analyze(tooShortName);

            StringAssert.Contains("fake exception", mockWebService.MessageToWebService);
        }

        public class FakeWebService : IWebService
        {
            public string? MessageToWebService;
            public void Write(string message)
            {
                MessageToWebService = message;
            }
        }

        public class FakeLogger : ILogger
        {
            public Exception? WillThrow = null;
            public string? LoggerGotMessage = null;
            public void LogError(string message)
            {
                LoggerGotMessage = message;
                if (WillThrow != null)
                {
                    throw WillThrow;
                }
            }
        }
    }
}