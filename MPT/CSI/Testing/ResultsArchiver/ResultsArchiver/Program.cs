using System.IO;
using System.Reflection;
using Csi.Testing.ResultsArchiver.Core;

namespace Csi.Testing.ResultsArchiver
{
    /// <summary>
    /// The console program that aggregates all of the result summary files and logs into an archival directory for a given program and build.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments for a custom path and/or filename to the config XML file. 
        /// If only the file name is given, it is assumed that the file is at the same location as the running application.</param>
        public static void Main(string[] args)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFilePath = (args != null && args.Length > 0) ? args[0] : assemblyPath;
            string pathRoot = (args != null && args.Length > 1) ? args[1] : assemblyPath;

            ResultArchiver resultArchiver = new ResultArchiver(configFilePath, pathRoot);
            resultArchiver.Archive();
        }
    }
}
