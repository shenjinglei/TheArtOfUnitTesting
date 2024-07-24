namespace Chapter3
{
    public class LogAnalyzer
    {
        private IExtensionManager manager;
        public LogAnalyzer(IExtensionManager manager)
        {
            this.manager = manager;
        }

        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsValid(fileName);
        }
    }

    internal class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            return !string.IsNullOrEmpty(fileName);
        }
    }

    public interface IExtensionManager
    {
        bool IsValid(string fileName);
    }
}