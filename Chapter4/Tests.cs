namespace Chapter4
{
    public interface IWebService
    {
        void LogError(string message);
    }

    public class FakeWebService : IWebService
    {
        public string? LastError;
        public void LogError(string message)
        {
            LastError = message;
        }
    }

    public class LogAnalyzer
    {
        private readonly IWebService _service;
        public LogAnalyzer(IWebService service)
        {
            _service = service;
        }

        public void Analyze(string fileName)
        {
            if(fileName.Length < 8)
            {
                _service.LogError("Filename too short:" + fileName);
            }
        }
    }

    public class Tests
    {

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            FakeWebService mockService = new FakeWebService();
            LogAnalyzer logAnalyzer = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";

            logAnalyzer.Analyze(tooShortFileName);

            StringAssert.Contains("Filename too short:abc.ext", mockService.LastError);
        }
    }
}