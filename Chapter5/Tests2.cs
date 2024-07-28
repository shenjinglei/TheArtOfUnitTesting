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

            public void Write(ErrorInfo message)
            {
                throw new NotImplementedException();
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

        [Test]
        public void Analyze_LoggerThrows_CallsWebService2()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });

            var analyzer = new LogAnalyzer2(stubLogger, mockWebService);
            analyzer.MinNameLength = 8;
            analyzer.Analyze("abc.txt");

            mockWebService.Received().Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });

            var analyzer = new LogAnalyzer3(stubLogger, mockWebService);
            analyzer.MinNameLength = 8;
            analyzer.Analyze("abc.txt");

            mockWebService.Received().Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000 
                && info.Message.Contains("fake exception")));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObjectCompare()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });

            var analyzer = new LogAnalyzer3(stubLogger, mockWebService);
            analyzer.MinNameLength = 8;
            analyzer.Analyze("abc.txt");

            var expected = new ErrorInfo(1000,"fake exception");
            mockWebService.Received().Write(expected);
        }
    }
}