using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName)
                && Path.GetFileNameWithoutExtension(fileName).Length > 5; ;
        }

        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }

    }
}
