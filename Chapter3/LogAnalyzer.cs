namespace Chapter3
{
    public class LogAnalyzer
    {
        private IExtensionManager manager;
        public LogAnalyzer()
        {
            manager = ExtensionManagerFactory.Create();
        }

        public bool IsValidLogFileName(string fileName)
        {
            try
            {
                return manager.IsValid(fileName)
                    && Path.GetFileNameWithoutExtension(fileName).Length > 5;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ExtensionManagerFactory
    {
        private static IExtensionManager? CustomManager = null;

        public static IExtensionManager Create()
        {
            if (CustomManager != null) return CustomManager;
            return new FileExtensionManager();
        }

        public static void SetManager(IExtensionManager manager)
        {
            CustomManager = manager;
        }
    }


}