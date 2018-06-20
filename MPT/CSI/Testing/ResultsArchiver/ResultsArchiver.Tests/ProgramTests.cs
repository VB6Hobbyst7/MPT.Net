using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Csi.Testing.ResultsArchiver;
using Csi.Testing.ResultsArchiver.Core;
using NUnit.Framework;
using NCrunch.Framework;
using System.Reflection;

// See: https://www.codeproject.com/Articles/17652/How-to-Test-Console-Applications
// See: https://www.cafe-encounter.net/p848/how-to-create-an-nunit-test-project-that-is-also-a-self-running-console-app-exe
// See: https://www.ncrunch.net/documentation/reference_project-configuration_run-pre-or-post-build-events
// See: http://forum.ncrunch.net/yaf_postst1073_Run-pre-post-build-events-doesn-t-resolve-macros-correctly.aspx
// See: http://www.ncrunch.net/documentation/reference_project-configuration_additional-files-to-include
namespace Csi.Testing.ResultsArchiver.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        public string RunPath = "";
        public string ConsoleName = "ResultsArchiver.exe";

        protected TextWriter _consoleNormalOutput;
        protected StringWriter _consoleTestingOutput;
        protected StringBuilder _testingStringBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Set current folder to testing folder
            string assemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;

            // Get directory name
            string dirName = Path.GetDirectoryName(assemblyCodeBase);
            if (dirName == null) return;

            // Remove URL-prefix if it exists.
            if (dirName.StartsWith("file:\\"))
                dirName = dirName.Substring(6);

            // Set current folder.
            Environment.CurrentDirectory = dirName;

            // Initialize string builder to replace console.
            _testingStringBuilder = new StringBuilder();
            _consoleTestingOutput = new StringWriter(_testingStringBuilder);

            // Swap normal output console with testing console - to reuse it later.
            _consoleNormalOutput = Console.Out;
            Console.SetOut(_consoleTestingOutput);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // Set normal output stream to the console.
            Console.SetOut(_consoleNormalOutput);
        }

        [SetUp]
        public void SetUp()
        {
            // Clear string builder.
            _testingStringBuilder.Remove(0, _testingStringBuilder.Length);
        }

        [TearDown]
        public void TearDown()
        {
            // Verbose output in console.
            _consoleNormalOutput.Write(_testingStringBuilder.ToString());

            if (string.IsNullOrEmpty(RunPath)) return;

            // Remove directories created for the test.
            string testTestingPath = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);

            if (Directory.Exists(testTestingPath))
                Directory.Delete(testTestingPath, recursive: true);

            string testResultsPath = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);

            if (Directory.Exists(testResultsPath))
                Directory.Delete(testResultsPath, recursive: true);

            string testArchivePath = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);

            if (Directory.Exists(testArchivePath))
                Directory.Delete(testArchivePath, recursive: true);

            RunPath = string.Empty;
        }

        [Test]
        public void Main_NoArguments_Creates_Archive_From_Default_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            copyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            Program.Main(null);

            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        [Test]
        public void Main_EXE_NoArguments_Creates_Archive_From_Default_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            copyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            startConsoleApplication(Path.Combine(RunPath, ConsoleName), workingDirectory: RunPath);

            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        [Test]
        public void Main_With_File_Argument_Creates_Archive_From_Specified_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            
            // Set up custom config file
            string configFile = "ResultsArchiver_CustomName.Config.xml";
            string sourceBasePathFile = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string fileDestination = Path.Combine(RunPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePathFile, configFile), fileDestination);

            // Set up test directories
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            copyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            string[] parameters = { configFile };
            Program.Main(parameters);

            // Test Results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        [Test]
        public void Main_With_File_Path_Argument_Creates_Archive_From_Specified_Config_File_at_Specified_Location()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;

            // Set up custom config file
            string configFile = "ResultsArchiver_CustomName.Config.xml";
            string sourceBasePathFile = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string otherLocationPath = Path.Combine(RunPath, "otherLocation");

            if (!Directory.Exists(otherLocationPath))
                Directory.CreateDirectory(otherLocationPath);
            string fileDestination = Path.Combine(otherLocationPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePathFile, configFile), fileDestination);


            // Set up test directories
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            copyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            string[] parameters = { fileDestination };
            Program.Main(parameters);

            // Test Results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        /// <summary>
        /// Starts the console application.
        /// </summary>
        /// <param name="applicationName">Name of the console application to run.</param>
        /// <param name="arguments">The arguments. Provide an empty string for no arguments.</param>
        /// <returns>System.Int32.</returns>
        protected int startConsoleApplication(string applicationName, string arguments = "", string workingDirectory = "")
        {
            // Initialize process here
            Process process = new Process
            {
                StartInfo =
                {
                    FileName = applicationName,

                    // add arguments as whole string
                    Arguments = arguments,          

                    // Use it to start from testing environment
                    UseShellExecute = false,        

                    // redirect outputs to have it in testing console
                    RedirectStandardOutput = true,  
                    RedirectStandardError = true,   

                    // set working directory
                    WorkingDirectory = (string.IsNullOrEmpty(workingDirectory))? Environment.CurrentDirectory : workingDirectory
                }
            };

            // start and wait for exit
            process.Start();
            process.WaitForExit();

            // get output to testing console.
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.Write(process.StandardError.ReadToEnd());

            // return exit code
            return process.ExitCode;
        }

        private void copyDirectory(string sourceFolder, string outputFolder)
        {
            // Create subdirectory structure in destination    
            foreach (string directory in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(Path.Combine(outputFolder, directory.Substring(sourceFolder.Length + 1)));
            }

            foreach (string fileName in Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories))
            {
                string fileDestination = fileName.Substring(sourceFolder.Length + 1);
                if (File.Exists(fileDestination)) continue;
                File.Copy(fileName, Path.Combine(outputFolder, fileDestination));
            }
        }
    }
}
