using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter5
{
    public class LogAnalyzer
    {
        private ILogger _logger;

        public LogAnalyzer(ILogger logger)
        {
            _logger = logger;
        }

        public int MinNameLength { get; set; }

        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
             {
                _logger.LogError(string.Format("Filename too short: {0}", filename));
            }
        }
    }

    public class LogAnalyzer2
    {
        private ILogger _logger;
        private IWebService _webService;


        public LogAnalyzer2(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int MinNameLength { get; set; }

        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
            {
                try
                {
                    _logger.LogError(string.Format("Filename too short: {0}", filename));
                }
                catch (Exception e)
                {
                    _webService.Write("Error From Logger: " + e);

                }
            }
        }
    }

    public interface ILogger
    {
        void LogError(string message);

    }

    public interface IWebService
    {
        void Write(string message);
    }

    
}
