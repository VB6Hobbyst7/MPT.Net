using System;
using System.IO;
using NUnit.Framework;
using NCrunch.Framework;

namespace Csi.Testing.ExamplesReleaser.Core.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        public const string PROGRAM_ETABS = "ETABS";
        public const string PROGRAM_SAP2000 = "SAP2000";
        public const string PROGRAM_CSIBRIDGE = "CSiBridge";

        public const string SAMPLE_DIR = "Sample";

        public string PathBase;
        public string PathDestination;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            PathBase = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName;
            PathBase = Path.Combine(PathBase, "resources-testing", "_modelsDB");

            PathDestination = Path.Combine(PathBase, "release");
        }

        [SetUp]
        public void Setup()
        {
            if (Directory.Exists(PathDestination))
                Directory.Delete(PathDestination, recursive: true);
        }

        [Test]
        public void ResultArchive_Initialization()
        {
            string pathModelControlFile = @"c:\pathSource\test_MC.xml";
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);

            Assert.That(example.PathModelControlFile, Is.EqualTo(pathModelControlFile));
            Assert.That(example.PathDestinationRoot, Is.EqualTo(pathDestinationRoot));
            Assert.That(example.Application, Is.EqualTo(PROGRAM_ETABS));

            Assert.That(string.IsNullOrEmpty(example.PathModelFile));
            Assert.That(example.PathSupportingFiles.Count, Is.EqualTo(0));
            Assert.That(string.IsNullOrEmpty(example.SubSuiteDirectory));
            Assert.IsFalse(example.IsSetPublic);
            Assert.IsFalse(example.IsComplete);
            Assert.IsFalse(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
        }


        [Test]
        public void Equals()
        {
            string pathModelControlFile = @"c:\pathSource\test_MC.xml";
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            Example exampleMatch = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            Assert.IsTrue(example.Equals(exampleMatch));

            Example exampleNonMatchingModelControl = new Example(@"c:\pathSource\testNonMatching_MC.xml",
                pathDestinationRoot, PROGRAM_ETABS);
            Assert.IsFalse(example.Equals(exampleNonMatchingModelControl));

            Example exampleNonMatchingDestination = new Example(pathModelControlFile, @"c:\pathDestinationNonMatching",
                PROGRAM_ETABS);
            Assert.IsFalse(example.Equals(exampleNonMatchingDestination));

            Example exampleNonMatchingApplication = new Example(pathModelControlFile, pathDestinationRoot,
                "ProgramNonMatching");
            Assert.IsFalse(example.Equals(exampleNonMatchingApplication));

            // Support Files
            example.IsValidForRelease();
            // Different lists by count
            pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSSupportingFilesExample",
                "Example 6-010_MC.xml");
            Example exampleWithSupportFiles = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            exampleWithSupportFiles.IsValidForRelease();
            Assert.IsFalse(example.Equals(exampleWithSupportFiles));

            // Different lists by content
            pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSSupportingFilesExample",
                "Example 6-010a_MC.xml");
            Example exampleNonMatchingSupportFilesContent = new Example(pathModelControlFile, pathDestinationRoot,
                PROGRAM_ETABS);
            exampleNonMatchingSupportFilesContent.IsValidForRelease();
            Assert.IsFalse(exampleWithSupportFiles.Equals(exampleNonMatchingSupportFilesContent));
        }

        [Test]
        public void IsValidForRelease_Is_False_For_NonExisting_ModelControl_File()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "NonExisting_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsFalse(example.IsMatchingProgram);
            Assert.IsFalse(example.IsSetPublic);
            Assert.IsFalse(example.IsComplete);
            Assert.IsFalse(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_False_For_NonMatching_Program()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "SAP2000Example", "Example 1-004_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsFalse(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_False_For_NonMatching_Program_of_MultiProgram()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "SAP2000Example", "Example 1-004_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsFalse(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_False_For_Private_Example()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSPrivateExample",
                "ACI 318-14 CFD Ex003_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsTrue(example.IsMatchingProgram);
            Assert.IsFalse(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_False_For_IncompleteExample()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSIncompleteExample",
                "Example 1-003_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsTrue(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsFalse(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_False_For_Deactivated_Example()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSDeactivatedExample",
                "CSA A23.3-04 SWD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsTrue(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsFalse(example.IsNotTurnedOff);
            Assert.IsFalse(example.IsValid);
            Assert.IsFalse(validForRelease);
        }

        [Test]
        public void IsValidForRelease_Is_True_For_Active_Public_Example_of_Matching_Program()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExample", "Example 1-003_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsTrue(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsTrue(example.IsValid);
            Assert.IsTrue(validForRelease);
        }

        [TestCase(PROGRAM_CSIBRIDGE)]
        [TestCase(PROGRAM_SAP2000)]
        public void IsValidForRelease_Is_True_For_Active_Public_Example_of_MultiProgram_Matching_Program(
            string programName)
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "SAP2000Example", "Example 1-004_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, programName);
            bool validForRelease = example.IsValidForRelease();

            Assert.IsTrue(example.IsMatchingProgram);
            Assert.IsTrue(example.IsSetPublic);
            Assert.IsTrue(example.IsComplete);
            Assert.IsTrue(example.IsNotTurnedOff);
            Assert.IsTrue(example.IsValid);
            Assert.IsTrue(validForRelease);
        }

        [Test]
        public void SubSuiteDirectory_From_Keyword()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSDesignKeywordExample",
                "ACI 318-14 CFD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.SubSuiteDirectory, Is.EqualTo("Concrete Frame"));
        }

        [Test]
        public void SubSuiteDirectory_From_Classification()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSDesignClassificationExample",
                "ACI 318-14 CFD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.SubSuiteDirectory, Is.EqualTo("Concrete Frame"));
        }

        [Test]
        public void SubSuiteDirectory_From_Any()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSDesignExample",
                "ACI 318-14 CFD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.SubSuiteDirectory, Is.EqualTo("Concrete Frame"));
        }

        [Test]
        public void SubSuiteDirectory_Is_None_From_Any()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExample",
                "ACI 318-14 CFD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.SubSuiteDirectory, Is.EqualTo(null));
        }

        [Test]
        public void PathSupportingFiles_Is_Empty_For_No_Supporting_Files()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExample",
                "ACI 318-14 CFD Ex001_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.PathSupportingFiles.Count, Is.EqualTo(0));
        }

        [Test]
        public void PathSupportingFiles_Has_Paths_For_Supporting_Files()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSSupportingFilesExample",
                "Example 6-010_MC.xml");
            string pathDestinationRoot = @"c:\pathDestination";

            Example example = new Example(pathModelControlFile, pathDestinationRoot, PROGRAM_ETABS);
            example.IsValidForRelease();
            Assert.That(example.PathSupportingFiles.Count, Is.Not.EqualTo(0));
            Assert.That(example.PathSupportingFiles[0], Is.Not.EqualTo(@"models\EQ6-010-trans.txt"));
            Assert.That(example.PathSupportingFiles[1], Is.Not.EqualTo(@"models\EQ6-010-long.txt"));
        }


        [Test]
        public void ReleaseExample_Invalid_Does_Nothing()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "NonExisting_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsFalse(validForRelease);

            example.ReleaseExample();

            Assert.That(!Directory.Exists(PathDestination));
        }

        [Test]
        public void ReleaseExample_Valid_With_No_Model_FileName_Does_Nothing()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExampleNoModelFileName",
                "Example 1-003_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();

            Assert.That(!Directory.Exists(PathDestination));
        }

        [Test]
        public void ReleaseExample_Valid_With_No_Model_File_Does_Nothing()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExampleNoModelFile", "Example 1-003_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();

            Assert.That(!Directory.Exists(PathDestination));
        }


        [Test]
        public void ReleaseExample_Valid_Releases_Example()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSExample", "Example 1-003_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();
            
            int numberOfFiles = new DirectoryInfo(PathDestination).GetFiles("*", SearchOption.AllDirectories).Length ;
            Assert.That(numberOfFiles, Is.EqualTo(1));
        }

        [Test]
        public void ReleaseExample_Valid_With_Supporting_Files_Releases_Example_With_Supporting_Files()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSSupportingFilesExample", "Example 6-010_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();

            int numberOfFiles = new DirectoryInfo(PathDestination).GetFiles("*", SearchOption.AllDirectories).Length;

            // 1 model file, 2 supporting files  = 3 released files
            Assert.That(numberOfFiles, Is.EqualTo(3));
        }

        [Test]
        public void ReleaseExample_Valid_With_Missing_Supporting_Files_Releases_Example_With_Existing_Supporting_Files()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSSupportingFilesMissingExample", "Example 6-010_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();

            int numberOfFiles = new DirectoryInfo(PathDestination).GetFiles("*", SearchOption.AllDirectories).Length;

            // 1 model file, 1 existing supporting file  = 2 released files
            // There was one additional supporting file that did not exist that was ignored
            Assert.That(numberOfFiles, Is.EqualTo(2));
        }

        [Test]
        public void ReleaseExample_Valid_In_SubSuite_Releases_Example_To_SubSuite()
        {
            string pathModelControlFile = Path.Combine(PathBase, SAMPLE_DIR, "ETABSDesignExample", "ACI 318-14 CFD Ex001_MC.xml");

            Example example = new Example(pathModelControlFile, PathDestination, PROGRAM_ETABS);
            bool validForRelease = example.IsValidForRelease();
            Assert.IsTrue(validForRelease);

            example.ReleaseExample();

            DirectoryInfo directory = new DirectoryInfo(PathDestination);
            int numberOfFiles = directory.GetFiles().Length;
            Assert.That(numberOfFiles, Is.EqualTo(0));

            DirectoryInfo[] subDirectories = directory.GetDirectories();
            Assert.That(subDirectories.Length, Is.EqualTo(1));
            subDirectories = subDirectories[0].GetDirectories();
            Assert.That(subDirectories.Length, Is.EqualTo(1));
            string directoryName = subDirectories[0].Name;
            Assert.That(directoryName, Is.EqualTo("Concrete Frame"));
            numberOfFiles = subDirectories[0].GetFiles().Length;
            Assert.That(numberOfFiles, Is.EqualTo(1));
        }
    }
}
