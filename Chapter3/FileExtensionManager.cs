namespace Chapter3
{
    public class FileExtensionManager : IExtensionManager
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