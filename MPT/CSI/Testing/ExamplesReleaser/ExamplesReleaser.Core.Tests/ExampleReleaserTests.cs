using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NCrunch.Framework;

namespace Csi.Testing.ExamplesReleaser.Core.Tests
{
    [TestFixture]
    public class ExampleReleaserTests
    {
        public string RunPath = "";
        public string OtherLocationPath = "";
        public string DestinationPath = "";

        string assemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;

        public const string PROGRAM = "SAP2000";
        public const string PATH_SOURCE = @"c:\foo";
        public const string PATH_RELEASE = @"c:\bar";

        [TearDown]
        public void TearDown()
        {
            if (string.IsNullOrEmpty(RunPath)) return;

            string testTestingPath = Path.Combine(RunPath, ExampleReleaser.DIR_SOURCE);

            if (Directory.Exists(testTestingPath))
                Directory.Delete(testTestingPath, recursive: true);

            string testResultsPath = Path.Combine(RunPath, ExampleReleaser.DIR_RELEASE);

            if (Directory.Exists(testResultsPath))
                Directory.Delete(testResultsPath, recursive: true);

            //if (!string.IsNullOrEmpty(OtherLocationPath) && Directory.Exists(OtherLocationPath))
                //Directory.Delete(OtherLocationPath, recursive: true);

            //if (!string.IsNullOrEmpty(DestinationPath) && Directory.Exists(DestinationPath))
                //Directory.Delete(DestinationPath, recursive: true);
            RunPath = string.Empty;
        }


        [Test]
        public void ExampleReleaser_Initialization_Specified()
        {
            RunPath = Environment.CurrentDirectory;
            DestinationPath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;

            Assert.That(ExampleReleaser.MODEL_CONTROL_XML_FILE_PATTERN, Is.EqualTo("*_MC.xml"));
            Assert.That(ExampleReleaser.CONFIG_FILENAME, Is.EqualTo("ExamplesReleaser.Config.xml"));
            Assert.That(ExampleReleaser.DIR_RELEASE, Is.EqualTo("Verification"));
            Assert.That(ExampleReleaser.DIR_SOURCE, Is.EqualTo("Verification"));
            Assert.That(ExampleReleaser.StandardSuites[0], Is.EqualTo("Analysis"));
            Assert.That(ExampleReleaser.StandardSuites[1], Is.EqualTo("Design"));

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, RunPath, DestinationPath);

            //Assert.That(exampleReleaser.PathRoot, Is.EqualTo(PATH_SOURCE)); // Source is at location of program. Not useful for testing.
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(RunPath));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(DestinationPath));

        }

        [Test]
        public void ExampleReleaser_Initialization_Specified_Invalid_Source_Destination_Directories()
        {
            RunPath = @"c:\foo";
            DestinationPath = @"c:\bar";

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, RunPath, DestinationPath);

            //Assert.That(exampleReleaser.PathRoot, Is.EqualTo(PATH_SOURCE)); // Source is at location of program. Not useful for testing.
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(@"\..\" + ExampleReleaser.DIR_SOURCE));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));

        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Defaults()
        {
            ExampleReleaser exampleReleaser = new ExampleReleaser();

            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(@"\..\" + ExampleReleaser.DIR_SOURCE));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_By_FileName_Only()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo("ETABS"));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(@"\..\" + ExampleReleaser.DIR_SOURCE));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_By_Full_File_Path()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");

            if (!Directory.Exists(OtherLocationPath))
                Directory.CreateDirectory(OtherLocationPath);
            string fileDestination = Path.Combine(OtherLocationPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);


            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo("ETABS"));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(@"\..\" + ExampleReleaser.DIR_SOURCE));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Empty()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_Empty.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(null));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(null));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(null));

            // Archive should do nothing with a malformed object
            Assert.DoesNotThrow(() => exampleReleaser.Release());
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Empty_Properties()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_EmptyProperties.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;

            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing", configFile), fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(string.Empty));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"\..\", ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));

            // Archive should do nothing with a malformed object
            Assert.DoesNotThrow(() => exampleReleaser.Release());
        }

        [Test]
        public void ExampleReleaser_Initialization_Custom_PathRoot()
        {
            string customRootPath = @"C:\\Foo\Bar";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();
            string pathConfig = Path.Combine(exampleReleaserPath.PathRoot, ExampleReleaser.CONFIG_FILENAME);
            ExampleReleaser exampleReleaser = new ExampleReleaser(pathConfig, customRootPath);

            Assert.That(exampleReleaser.PathRoot, Is.EqualTo(customRootPath));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_PathRoot_Does_Not_Exist()
        {
            ExampleReleaser exampleReleaser = new ExampleReleaser(pathRoot: @"c:\Foo\Bar");

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_Required_Directories_Do_Not_Exist()
        {
            ExampleReleaser exampleReleaserBase = new ExampleReleaser(PROGRAM);
            string pathSource = Path.Combine(exampleReleaserBase.PathRoot, ExampleReleaser.DIR_SOURCE);
            string pathRelease = Path.Combine(exampleReleaserBase.PathRoot, ExampleReleaser.DIR_RELEASE);

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, pathSource, pathRelease);
            RunPath = exampleReleaser.PathRoot;

            
            if (Directory.Exists(pathSource))
                Directory.Delete(pathSource, recursive: true);

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            
            if (Directory.Exists(pathRelease))
                Directory.Delete(pathRelease, recursive: true);

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.IsFalse(Directory.Exists(pathRelease));
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_Required_Properties_Are_NullOrEmpty()
        {
            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(null, @"c:\", @"c:\Program Files");

            // Method under test
            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Correctly_Reads_Custom_Config_File()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");

            if (!Directory.Exists(OtherLocationPath))
                Directory.CreateDirectory(OtherLocationPath);
            string fileDestination = Path.Combine(OtherLocationPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            // Method under test
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(modelsDatabaseDestinationPath));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Releases_Examples_Of_Program_Set_For_Release_And_None_Others()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");

            if (!Directory.Exists(OtherLocationPath))
                Directory.CreateDirectory(OtherLocationPath);
            string fileDestination = Path.Combine(OtherLocationPath, configFile);
            if (!File.Exists(fileDestination))
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            // Method under test
            exampleReleaser.Release();

            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(4));
        }



        private static void copyDirectory(string sourceFolder, string outputFolder)
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
