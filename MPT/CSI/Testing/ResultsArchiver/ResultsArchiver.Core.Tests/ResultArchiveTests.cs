using System.IO;
using NCrunch.Framework;
using NUnit.Framework;

namespace Csi.Testing.ResultsArchiver.Core.Tests
{
    [TestFixture]
    public class ResultArchiveTests
    {
        public const string DIRECTORY_RESULT = "_results";
        public const string PROGRAM = "SAP2000";
       
        public const string SUITE_SUCCESSES = "Successes";
        public const string SUITE_FAILURES = "Failures";
        public const string SUITE_DUPLICATES = "Duplicates";

        public const string BIT_RUN = "32";
        public const string VERSION = "20.0.0.1384";
        public const string DESCRIPTION = "P1 S1 B1";
        public const string RUNTYPE_PSB = "PSB";

        public const string RUN_MATCHING = "2017-12-14(225016)";
        public const string RUN_NONMATCHING_BY_BIT = "2017-12-16(001206)";
        public const string RUN_NONMATCHING_BY_TEST_DESCRIPTION = "2017-12-14(225019)";

        public string PathBase;
        public string PathDestination;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            PathBase = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName; 
            PathBase = Path.Combine(PathBase, "resources-testing", "_results");

            PathDestination = Path.Combine(PathBase, PROGRAM, DIRECTORY_RESULT);
        }

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists(PathDestination))
                Directory.Delete(PathDestination, recursive: true);
        }


        [Test]
        public void ResultArchive_Initialization()
        {
            string pathSource = @"c:\pathSource";
            string pathDestination = @"c:\pathDestination";
            string suite = "Suite";
            

            ResultArchive resultArchive = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, BIT_RUN, VERSION, DESCRIPTION);

            Assert.That(resultArchive.PathSource, Is.EqualTo(pathSource));
            Assert.That(resultArchive.PathDestination, Is.EqualTo(pathDestination));
            Assert.IsFalse(resultArchive.OverWriteExisting);
            Assert.That(resultArchive.Suite, Is.EqualTo(suite));
            Assert.That(resultArchive.BitRun, Is.EqualTo(BIT_RUN));
            Assert.That(resultArchive.Version, Is.EqualTo(VERSION));
            Assert.That(resultArchive.Description, Is.EqualTo(DESCRIPTION));

            Assert.That(resultArchive.IsValidTestResultDirectory, Is.EqualTo(false));
            Assert.That(resultArchive.ResultsAndLogsLocated, Is.EqualTo(false));
            Assert.That(resultArchive.IsValid, Is.EqualTo(false));
            Assert.That(string.IsNullOrEmpty(resultArchive.ResultsLocation), Is.EqualTo(true));
            Assert.That(resultArchive.LogLocations.Count, Is.EqualTo(0));
        }
       

        [Test]
        public void Equals()
        {
            string pathSource = @"c:\pathSource";
            string pathDestination = @"c:\pathDestination";
            string suite = "Suite";
            string bitRun = "32";

            ResultArchive resultArchive = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, bitRun, VERSION, DESCRIPTION);
            ResultArchive resultArchiveMatch = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, bitRun, VERSION, DESCRIPTION);
            Assert.IsTrue(resultArchive.Equals(resultArchiveMatch));

            ResultArchive resultArchiveNonMatchingSuite = new ResultArchive(pathSource, pathDestination, PROGRAM, "NonMatchingSuite", bitRun, VERSION, DESCRIPTION);
            Assert.IsFalse(resultArchive.Equals(resultArchiveNonMatchingSuite));

            ResultArchive resultArchiveNonMatchingVersion = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, bitRun, "19.1.2.456", DESCRIPTION);
            Assert.IsFalse(resultArchive.Equals(resultArchiveNonMatchingVersion));

            ResultArchive resultArchiveNonMatchingDescription = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, bitRun, VERSION, "P1 S2 B2");
            Assert.IsFalse(resultArchive.Equals(resultArchiveNonMatchingDescription));

            ResultArchive resultArchiveNonMatchingBit = new ResultArchive(pathSource, pathDestination, PROGRAM, suite, "64", VERSION, DESCRIPTION);
            Assert.IsFalse(resultArchive.Equals(resultArchiveNonMatchingBit));
        }

        [Test]
        public void ConfirmValidTestResultDirectory_Confirms()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.ConfirmValidTestResultDirectory();
            Assert.IsTrue(resultArchive.IsValidTestResultDirectory);
        }

        [Test]
        public void ConfirmValidTestResultDirectory_Duplicate_Results_Files_Does_Not_Confirm()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "DuplicateResultsXMLFiles");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.ConfirmValidTestResultDirectory();
            Assert.IsFalse(resultArchive.IsValidTestResultDirectory);
        }

        [Test]
        public void ConfirmValidTestResultDirectory_No_Results_Files_Does_Not_Confirm()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "NoResultsXMLFiles");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.ConfirmValidTestResultDirectory();
            Assert.IsFalse(resultArchive.IsValidTestResultDirectory);
        }

        [Test]
        public void ConfirmValidTestResultDirectory_NonMatching_Bit_Does_Not_Confirm()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_NONMATCHING_BY_BIT);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.ConfirmValidTestResultDirectory();
            Assert.IsFalse(resultArchive.IsValidTestResultDirectory);
        }

        [Test]
        public void ConfirmValidTestResultDirectory_NonMatching_Description_Does_Not_Confirm()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_NONMATCHING_BY_TEST_DESCRIPTION);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.ConfirmValidTestResultDirectory();
            Assert.IsFalse(resultArchive.IsValidTestResultDirectory);
        }


        [Test]
        public void LocateResultsAndLogs_Locates_ResultsAndLogs()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.LocateResultsAndLogs();
            Assert.IsTrue(resultArchive.ResultsAndLogsLocated);
        }

        [Test]
        public void LocateResultsAndLogs_Duplicate_Results_Files_Does_Not_Locate_ResultsAndLogs()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "DuplicateResultsFiles");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.LocateResultsAndLogs();
            Assert.IsFalse(resultArchive.ResultsAndLogsLocated);
        }

        [Test]
        public void LocateResultsAndLogs_No_Results_Files_Does_Not_Locate_ResultsAndLogs()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "NoResultsFiles");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.LocateResultsAndLogs();
            Assert.IsFalse(resultArchive.ResultsAndLogsLocated);
        }

        [Test]
        public void LocateResultsAndLogs_No_Valid_SubDirectories_Files_Does_Not_Locate_ResultsAndLogs()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "NoValidSubDirectories");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.LocateResultsAndLogs();
            Assert.IsFalse(resultArchive.ResultsAndLogsLocated);
        }

        [Test]
        public void IsValidResult()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.IsValidResult();
            Assert.IsTrue(resultArchive.IsValid);
        }

        [Test]
        public void CreateResultsArchive_No_Results_Location_Found_Does_Not_Archive()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_FAILURES, RUNTYPE_PSB, "NoResultsFiles");

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_FAILURES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.IsValidResult();
            Assert.IsFalse(resultArchive.IsValid);
            resultArchive.CreateResultsArchive();
            Assert.IsFalse(Directory.Exists(PathDestination));
        }

        [Test]
        public void CreateResultsArchive_Not_IsValid_Does_Not_Archive()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_NONMATCHING_BY_TEST_DESCRIPTION);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION, DESCRIPTION);
            resultArchive.IsValidResult();
            Assert.IsFalse(resultArchive.IsValid);
            resultArchive.CreateResultsArchive();
            Assert.IsFalse(Directory.Exists(PathDestination));
        }


        [Test]
        public void CreateResultsArchive_Archives_Match()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_SUCCESSES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, PROGRAM, SUITE_SUCCESSES, BIT_RUN, VERSION,
                DESCRIPTION);
            resultArchive.IsValidResult();
            Assert.That(resultArchive.IsValid, Is.EqualTo(true));

            resultArchive.CreateResultsArchive();
            Assert.IsTrue(Directory.Exists(PathDestination));

            // Set
            string[] directories = Directory.GetDirectories(PathDestination);
            Assert.That(directories.Length, Is.EqualTo(1));

            // Suite
            string[] directoriesSuite = Directory.GetDirectories(directories[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Bit
            string[] directoriesBit = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesBit.Length, Is.EqualTo(1));

            // Run
            string[] directoriesRun = Directory.GetDirectories(directoriesBit[0]);
            Assert.That(directoriesRun.Length, Is.EqualTo(1));

            string[] filesResult = Directory.GetFiles(directoriesRun[0]);
            Assert.That(filesResult.Length, Is.EqualTo(1));
            
            // Log
            string[] directoriesLog = Directory.GetDirectories(directoriesRun[0]);
            Assert.That(directoriesLog.Length, Is.EqualTo(1));

            string[] filesLog = Directory.GetFiles(directoriesLog[0]);
            Assert.That(filesLog.Length, Is.EqualTo(7));
        }

        [Test]
        public void CreateResultsArchive_OverwriteExisting_Overwrites_Existing_File()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_DUPLICATES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, 
                                                PROGRAM, SUITE_DUPLICATES, BIT_RUN, VERSION,
                                                DESCRIPTION, overWriteExisting: true);
            resultArchive.IsValidResult();
            Assert.That(resultArchive.IsValid, Is.EqualTo(true));

            resultArchive.CreateResultsArchive();
            Assert.IsTrue(Directory.Exists(PathDestination));

            // Set
            string[] directories = Directory.GetDirectories(PathDestination);
            Assert.That(directories.Length, Is.EqualTo(1));

            // Suite
            string[] directoriesSuite = Directory.GetDirectories(directories[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Bit
            string[] directoriesBit = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesBit.Length, Is.EqualTo(1));

            // Run
            string[] directoriesRun = Directory.GetDirectories(directoriesBit[0]);
            Assert.That(directoriesRun.Length, Is.EqualTo(1));

            string[] filesResult = Directory.GetFiles(directoriesRun[0]);
            Assert.That(filesResult.Length, Is.EqualTo(1));

            // Log
            string[] directoriesLog = Directory.GetDirectories(directoriesRun[0]);
            Assert.That(directoriesLog.Length, Is.EqualTo(1));

            string[] filesLog = Directory.GetFiles(directoriesLog[0]);
            Assert.That(filesLog.Length, Is.EqualTo(7));
        }

        [Test]
        public void CreateResultsArchive_Not_OverwriteExisting_Does_Not_Overwrite_Existing_File()
        {
            string pathSource = Path.Combine(PathBase, PROGRAM, SUITE_DUPLICATES, RUNTYPE_PSB, RUN_MATCHING);

            ResultArchive resultArchive = new ResultArchive(pathSource, PathDestination, 
                                                PROGRAM, SUITE_DUPLICATES, BIT_RUN, VERSION,
                                                DESCRIPTION, overWriteExisting: false);
            resultArchive.IsValidResult();
            Assert.That(resultArchive.IsValid, Is.EqualTo(true));

            resultArchive.CreateResultsArchive();
            Assert.IsTrue(Directory.Exists(PathDestination));

            // Set
            string[] directories = Directory.GetDirectories(PathDestination);
            Assert.That(directories.Length, Is.EqualTo(1));

            // Suite
            string[] directoriesSuite = Directory.GetDirectories(directories[0]);
            Assert.That(directoriesSuite.Length, Is.EqualTo(1));

            // Bit
            string[] directoriesBit = Directory.GetDirectories(directoriesSuite[0]);
            Assert.That(directoriesBit.Length, Is.EqualTo(1));

            // Run
            string[] directoriesRun = Directory.GetDirectories(directoriesBit[0]);
            Assert.That(directoriesRun.Length, Is.EqualTo(1));

            string[] filesResult = Directory.GetFiles(directoriesRun[0]);
            Assert.That(filesResult.Length, Is.EqualTo(1));

            // Log
            string[] directoriesLog = Directory.GetDirectories(directoriesRun[0]);
            Assert.That(directoriesLog.Length, Is.EqualTo(1));

            string[] filesLog = Directory.GetFiles(directoriesLog[0]);
            Assert.That(filesLog.Length, Is.EqualTo(7));
        }
    }
}
