using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NCrunch.Framework;

namespace Csi.Testing.ResultsArchiver.Core.Tests
{
    [TestFixture]
    public class ResultArchiverTests
    {
        public string RunPath = "";

        public const string PROGRAM = "SAP2000";
        public const string BIT_RUN = "32";
        public const string VERSION = "19.2.0.1350";
        public const string RUNTYPE_SINGLE = "Single";
        public const string RUNTYPE_PSB = "PSB";
        public const string DESCRIPTION = "P1 S1 B1";
        public const string DESCRIPTION_FILE = "Run as is";
        public const string DESCRIPTION_STANDARD = "Run as is";
        public const string SUITE_NAME_FILE = "Design";

        [TearDown]
        public void TearDown()
        {
            if (string.IsNullOrEmpty(RunPath)) return;

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
        public void ResultArchiver_Initialization_Specified_And_Defaults()
        {
            Assert.That(ResultArchiver.BitsRun[0], Is.EqualTo(BIT_RUN));
            Assert.That(ResultArchiver.PsbTestDescriptions[0], Is.EqualTo(DESCRIPTION));
            Assert.That(ResultArchiver.StandardSuites[0], Is.EqualTo("Analysis"));
            
            ResultArchiver resultArchiver = new ResultArchiver(PROGRAM, VERSION, RUNTYPE_PSB);

            //Assert.That(resultArchiver.PathRoot, Is.EqualTo(pathSource)); // Source is at location of program. Not useful for testing.
            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_PSB));
            Assert.IsFalse(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames.Count, Is.EqualTo(0));
            Assert.That(resultArchiver.TestDescriptions.Count, Is.EqualTo(9));
            Assert.That(resultArchiver.TestDescriptions[3], Is.EqualTo(ResultArchiver.PsbTestDescriptions[3]));

        }

        [Test]
        public void ResultArchiver_Initialization_Specified_No_Defaults()
        {
            List<string> suiteNames = new List<string> {"Foo", "Bar"};
            List<string> testDescriptions = new List<string> { "Moo", "Nar" };

            ResultArchiver resultArchiver = new ResultArchiver(PROGRAM, VERSION, RUNTYPE_SINGLE, suiteNames, testDescriptions, overWriteExisting: true);
            
            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_SINGLE));
            Assert.IsTrue(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames.Count, Is.EqualTo(2));
            Assert.That(resultArchiver.SuiteNames[1], Is.EqualTo(suiteNames[1]));
            Assert.That(resultArchiver.TestDescriptions.Count, Is.EqualTo(2));
            Assert.That(resultArchiver.TestDescriptions[1], Is.EqualTo(testDescriptions[1]));
        }

        [Test]
        public void ResultArchiver_Initialization_Empty_Test_Descriptions()
        {
            ResultArchiver resultArchiver = new ResultArchiver(PROGRAM, VERSION, RUNTYPE_SINGLE, null, testDescriptions: new List<string>());

            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_SINGLE));
            Assert.That(resultArchiver.SuiteNames.Count, Is.EqualTo(0));
            Assert.That(resultArchiver.TestDescriptions.Count, Is.EqualTo(1));
            Assert.That(resultArchiver.TestDescriptions[0], Is.EqualTo(DESCRIPTION_STANDARD));
        }

        [TestCase("single", ExpectedResult = "Single")]
        [TestCase("Single", ExpectedResult = "Single")]
        [TestCase("psb", ExpectedResult = "PSB")]
        [TestCase("PSB", ExpectedResult = "PSB")]
        [TestCase("development", ExpectedResult = "Examples Development")]
        [TestCase("Development", ExpectedResult = "Examples Development")]
        [TestCase("Foobar", ExpectedResult = "Foobar")]
        public string ResultArchiver_Initialization_RunTypes(string runType)
        {
            ResultArchiver resultArchiverSingle = new ResultArchiver(PROGRAM, VERSION, runType);
            return resultArchiverSingle.RunType;
        }

        [Test]
        public void ResultArchiver_Initialization_From_Config_File_Defaults()
        {
            ResultArchiver resultArchiver = new ResultArchiver();
            
            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_SINGLE));
            Assert.IsTrue(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames[0], Is.EqualTo(SUITE_NAME_FILE));
            Assert.That(resultArchiver.TestDescriptions[0], Is.EqualTo(DESCRIPTION_FILE));
        }

        [Test]
        public void ResultArchiver_Initialization_From_Config_File_By_FileName_Only()
        {
            // Set up custom config file
            string configFile = "ResultsArchiver_CustomName.Config.xml";
            ResultArchiver resultArchiverPath = new ResultArchiver();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string fileDestination = Path.Combine(resultArchiverPath.PathRoot, configFile);
            if (!File.Exists(fileDestination)) 
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(configFile);

            // Check results
            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_SINGLE));
            Assert.IsTrue(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames[0], Is.EqualTo(SUITE_NAME_FILE));
            Assert.That(resultArchiver.TestDescriptions[0], Is.EqualTo(DESCRIPTION_FILE));
        }

        [Test]
        public void ResultArchiver_Initialization_From_Config_File_By_Full_File_Path()
        {
            // Set up custom config file
            string configFile = "ResultsArchiver_CustomName.Config.xml";
            ResultArchiver resultArchiverPath = new ResultArchiver();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string otherLocationPath = Path.Combine(resultArchiverPath.PathRoot, "otherLocation");

            if (!Directory.Exists(otherLocationPath))
                Directory.CreateDirectory(otherLocationPath);
            string fileDestination = Path.Combine(otherLocationPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(fileDestination);

            // Check results
            Assert.That(resultArchiver.Application, Is.EqualTo(PROGRAM));
            Assert.That(resultArchiver.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchiver.RunType, Is.EqualTo(RUNTYPE_SINGLE));
            Assert.IsTrue(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames[0], Is.EqualTo(SUITE_NAME_FILE));
            Assert.That(resultArchiver.TestDescriptions[0], Is.EqualTo(DESCRIPTION_FILE));
        }

        [Test]
        public void ResultArchiver_Initialization_From_Config_File_Empty()
        {
            // Set up custom config file
            string configFile = "ResultsArchiver_Empty.Config.xml";
            ResultArchiver resultArchiverPath = new ResultArchiver();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string fileDestination = Path.Combine(resultArchiverPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(configFile);

            // Check results
            Assert.That(resultArchiver.Application, Is.EqualTo(null));
            Assert.That(resultArchiver.Version, Is.EqualTo(null));
            Assert.That(resultArchiver.RunType, Is.EqualTo(null));
            Assert.IsFalse(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames.Count, Is.EqualTo(0));
            Assert.That(resultArchiver.TestDescriptions.Count, Is.EqualTo(0));

            // Archive should do nothing with a malformed object
            Assert.DoesNotThrow(() => resultArchiver.Archive());
        }

        [Test]
        public void ResultArchiver_Initialization_From_Config_File_Empty_Properties()
        {
            // Set up custom config file
            string configFile = "ResultsArchiver_EmptyProperties.Config.xml";
            ResultArchiver resultArchiverPath = new ResultArchiver();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;

            string fileDestination = Path.Combine(resultArchiverPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing", configFile), fileDestination);

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(configFile);

            // Check results
            Assert.That(resultArchiver.Application, Is.EqualTo(string.Empty));
            Assert.That(resultArchiver.Version, Is.EqualTo(string.Empty));
            Assert.That(resultArchiver.RunType, Is.EqualTo(string.Empty));
            Assert.IsFalse(resultArchiver.OverWriteExisting);
            Assert.That(resultArchiver.SuiteNames[0], Is.EqualTo(string.Empty));
            Assert.That(resultArchiver.TestDescriptions[0], Is.EqualTo(string.Empty));

            // Archive should do nothing with a malformed object
            Assert.DoesNotThrow(() => resultArchiver.Archive());
        }

        [Test]
        public void ResultArchiver_Initialization_Custom_PathRoot()
        {
            string customRootPath = @"C:\\Foo\Bar";
            ResultArchiver resultArchiverPath = new ResultArchiver();
            string pathConfig = Path.Combine(resultArchiverPath.PathRoot, ResultArchiver.CONFIG_FILENAME);
            ResultArchiver resultArchiver = new ResultArchiver(pathConfig, customRootPath);

            Assert.That(resultArchiver.PathRoot, Is.EqualTo(customRootPath));
        }

        [Test]
        public void Archive_Does_Nothing_When_PathRoot_Does_Not_Exist()
        {
            ResultArchiver resultArchiver = new ResultArchiver(pathRoot: @"c:\Foo\Bar");
            Assert.DoesNotThrow(() => resultArchiver.Archive());
        }

        [Test]
        public void Archive_Does_Nothing_When_Required_Directories_Do_Not_Exist()
        {
            ResultArchiver resultArchiver = new ResultArchiver(PROGRAM, VERSION, RUNTYPE_SINGLE, null, new List<string>(ResultArchiver.PsbTestDescriptions));
            RunPath = resultArchiver.PathRoot;

            string pathSource = Path.Combine(resultArchiver.PathRoot, ResultArchiver.DIR_TESTING);
            if (Directory.Exists(pathSource))
                Directory.Delete(pathSource, recursive: true);

            Assert.DoesNotThrow(() => resultArchiver.Archive());

            string pathResults = Path.Combine(resultArchiver.PathRoot, ResultArchiver.DIR_RESULTS);
            if (Directory.Exists(pathResults))
                Directory.Delete(pathResults, recursive: true);

            Assert.DoesNotThrow(() => resultArchiver.Archive());

            string pathArchive = Path.Combine(resultArchiver.PathRoot, ResultArchiver.DIR_ARCHIVE);
            if (Directory.Exists(pathArchive))
                Directory.Delete(pathArchive, recursive: true);

            Assert.DoesNotThrow(() => resultArchiver.Archive());

            Assert.IsFalse(Directory.Exists(pathArchive));
        }
        
        [TestCase(null, "19.2.0.1350", "single")]
        [TestCase("SAP2000", null, "single")]
        [TestCase("SAP2000", "19.2.0.1350", null)]
        public void Archive_Does_Nothing_When_Required_Properties_Are_NullOrEmpty(string application, string version, string runType)
        {
            // Set up object
            ResultArchiver resultArchiver = new ResultArchiver(application, version, runType);
            RunPath = resultArchiver.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            Assert.DoesNotThrow(() => resultArchiver.Archive());

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(0));
        }


        [Test, NUnit.Framework.Timeout(480000)]
        public void Archive_PSB()
        {
            // Set up object
            ResultArchiver resultArchiver = new ResultArchiver("SAP2000", "20.0.0.1384", RUNTYPE_PSB);
            RunPath = resultArchiver.PathRoot;
            
            string solutionPath = NCrunchEnvironment.GetOriginalSolutionPath();
            string sourceBasePath = Directory.GetParent(Directory.GetParent(solutionPath).FullName).FullName;

            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(2));

            // Analysis Results
            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[1]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(9));

            // Check first PSB
            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v20.0.0 Build 1384", "64-bit", "Analysis", "P1 S1 B1");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-12-15(180937) SAP2000_20.0.0 Build_1384 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "Example 1-001.LOG");
            Assert.That(File.Exists(logFilePath));

            // Check Standard PSB
            archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v20.0.0 Build 1384", "64-bit", "Analysis", "P2 S2 B1 - Standard");
            resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-12-15(180952) SAP2000_20.0.0 Build_1384 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            logFilePath = Path.Combine(logDestinationPath, "Example 1-001.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        [Test]
        public void Archive_Single()
        {
            // Set up object
            ResultArchiver resultArchiver = new ResultArchiver("SAP2000", "19.2.0.1350", RUNTYPE_SINGLE, testDescriptions: new List<string> {"Run as is"});
            RunPath = resultArchiver.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));
            
            // Method under test
            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(2));

            // Analysis Results
            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Analysis", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-17(184745) SAP2000_19.2.0 Build_1350.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "Example 1-001.LOG");
            Assert.That(File.Exists(logFilePath));

            // Design Results
            directoriesRuns = Directory.GetDirectories(directoriesSuite[1]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }

        [Test]
        public void Archive_Examples_Development()
        {
            // Set up object
            ResultArchiver resultArchiver = new ResultArchiver("SAP2000", "20.0.0.1381", "development", testDescriptions: new List<string> { "Run as is" });
            RunPath = resultArchiver.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));
            
            // Method under test
            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v20.0.0 Build 1381", "32-bit", "Development", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-12-08(024756) SAP2000_20.0.0 Build_1381 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-16 SFD Ex001.LOG");
            Assert.That(File.Exists(logFilePath));

            logFilePath = Path.Combine(logDestinationPath, "AISC 360-16 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }


        [Test]
        public void Archive_Single_Filtered_By_Suite()
        {
            // Set up object
            ResultArchiver resultArchiver = new ResultArchiver("SAP2000", "19.2.0.1350", RUNTYPE_SINGLE, 
                                                    suiteNames: new List<string> {"Design"}, testDescriptions: new List<string> { "Run as is" });
            RunPath = resultArchiver.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Method under test
            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

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
        public void Archive_Single_OverwriteExisting_Overwrites_Existing_File()
        {
            // Set up object
            ResultArchiver resultArchiverPath = new ResultArchiver();
            RunPath = resultArchiverPath.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Create duplicate directory
            string pathDuplicateSource = Path.Combine(pathResults, "SAP2000", "Design", "Single", "Success - 2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is");
            CopyDirectory(Path.Combine(pathDuplicateSource, "Steel Frame"), Path.Combine(pathDuplicateSource, "Steel Frame - Copy"));

            // Set up custom config file
            string configFile = "ResultsArchiver_OverwriteExisting.Config.xml";
            sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string fileDestination = Path.Combine(resultArchiverPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(configFile);
            Assert.IsTrue(resultArchiver.OverWriteExisting);

            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

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
        public void Archive_Single_Not_OverwriteExisting_Does_Not_Overwrite_Existing_File()
        {
            // Set up object
            ResultArchiver resultArchiverPath = new ResultArchiver();
            RunPath = resultArchiverPath.PathRoot;

            // Set up directories
            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources", "_Tests");
            CopyDirectory(sourceBasePath, RunPath);

            string pathSource = Path.Combine(RunPath, ResultArchiver.DIR_TESTING);
            Assert.That(Directory.Exists(pathSource));

            string pathResults = Path.Combine(RunPath, ResultArchiver.DIR_RESULTS);
            Assert.That(Directory.Exists(pathResults));

            string pathArchive = Path.Combine(RunPath, ResultArchiver.DIR_ARCHIVE);
            Assert.That(Directory.Exists(pathArchive));

            // Create duplicate directory
            string pathDuplicateSource = Path.Combine(pathResults, "SAP2000", "Design", "Single", "Success - 2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is");
            CopyDirectory(Path.Combine(pathDuplicateSource, "Steel Frame"), Path.Combine(pathDuplicateSource, "Steel Frame - Copy"));

            // Set up custom config file
            string configFile = "ResultsArchiver_NoOverwriteExisting.Config.xml";
            sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ResultsArchiver", "resources-testing");
            string fileDestination = Path.Combine(resultArchiverPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ResultArchiver resultArchiver = new ResultArchiver(configFile);
            Assert.IsFalse(resultArchiver.OverWriteExisting);
            
            resultArchiver.Archive();

            // Check results
            string[] directoriesProgramVersion = Directory.GetDirectories(pathArchive);
            Assert.That(directoriesProgramVersion.Length, Is.EqualTo(1));

            string[] directoriesBits = Directory.GetDirectories(directoriesProgramVersion[0]);
            Assert.That(directoriesBits.Length, Is.EqualTo(1));

            // Design Results
            string[] directoriesSuite = Directory.GetDirectories(directoriesBits[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            string[] directoriesRuns = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesRuns.Length, Is.EqualTo(1));

            string archiveDestinationPath = Path.Combine(pathArchive, "SAP2000 v19.2.0 Build 1350", "32-bit", "Design", "Run as is");
            string resultsSummaryFilePath = Path.Combine(archiveDestinationPath, "test_results_2017-08-15(225720) SAP2000_19.2.0 Build_1350 run_as_is.html");
            Assert.That(File.Exists(resultsSummaryFilePath));

            string logDestinationPath = Path.Combine(archiveDestinationPath, "logs");
            string logFilePath = Path.Combine(logDestinationPath, "AISC 360-05 SFD Ex002.LOG");
            Assert.That(File.Exists(logFilePath));
        }


        private void CopyDirectory(string sourceFolder, string outputFolder)
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
