namespace Chapter5
{
    public class Tests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            FakeLogger logger = new FakeLogger();
            LogAnalyzer analyzer = new LogAnalyzer(logger);

            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");

            StringAssert.Contains("too short", logger.LastError);
        }

        //monk class
        class FakeLogger : ILogger
        {
            public string? LastError;
            public void LogError(string message)
            {
                LastError = message;
            }
        }
    }
}