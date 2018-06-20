using System.Diagnostics;
using System.IO;

using NUnit.Framework;
using NSubstitute;

using MPT.FileSystem.Processor;
using MPT.FileSystem.Validator;

namespace MPT.FileSystem.UnitTests.Processor
{
    [TestFixture]
    public class RecursiveFileProcessorTests : LibraryTests_Base
    {
        [Test]
        public void ProcessFile_of_Valid_Path_Adds_Path()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessFile(validFilePath,
                                                                                    processor);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessFile_of_InValid_Path_Does_Not_Add_Path()
        {
            string targetFile = @"C:\Foo\Bar\Foo.Bar";

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(targetFile).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessFile(targetFile,
                                                                                    processor);
            Assert.IsTrue(processorResult.Paths.Count == 0);
        }

        [Test]
        public void ProcessFile_of_InValid_Path_Events_Messenger()
        {
            string targetFile = @"C:\Foo\Bar\Foo.Bar";

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(targetFile).Returns(true);

            IProcessFile processor = new FileList(validator);

            var wasCalled = false;
            RecursiveFileProcessor.Messenger += (e) => wasCalled = true;

            RecursiveFileProcessor.ProcessFile(targetFile,
                                                processor);
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ProcessDirectory_of_Valid_Path_without_Subdirectories_Adds_Path()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string targetDirectory = Path.GetDirectoryName(validFilePath);

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessDirectory(targetDirectory,
                                                                                         processor,
                                                                                         includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessDirectory_of_InValid_Path_without_Subdirectories_Does_Not_Add_Path()
        {
            string targetDirectory = @"C:\Foo\Bar";

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(targetDirectory).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessDirectory(targetDirectory,
                                                                                        processor,
                                                                                        includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 0);
        }

        [Test]
        public void ProcessDirectory_of_InValid_Path_without_Subdirectories_Events_Messenger()
        {
            string targetDirectory = @"C:\Foo\Bar";

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(targetDirectory).Returns(true);

            IProcessFile processor = new FileList(validator);

            var wasCalled = false;
            RecursiveFileProcessor.Messenger += (e) => wasCalled = true;

            RecursiveFileProcessor.ProcessDirectory(targetDirectory,
                                                    processor,
                                                    includeSubDirectories: false);
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ProcessDirectory_of_Valid_Path_with_Subdirectories_Adds_All_Valid_Paths()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string targetDirectory = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\"));

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessDirectory(targetDirectory,
                                                                                        processor,
                                                                                        includeSubDirectories: true);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessPaths_of_Valid_Path_without_Subdirectories_Adds_Path()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string targetDirectory1 = Path.GetDirectoryName(validFilePath);
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessPaths(paths,
                                                                                    processor,
                                                                                    includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessPaths_of_Valid_Path_with_Subdirectories_Adds_All_Valid_Path()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validFilePath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessPaths(paths,
                                                                                    processor,
                                                                                    includeSubDirectories: true);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessPaths_of_Valid_File_Path_Adds_All_Valid_Path()
        {
            string validFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validFilePath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validFilePath, @"..\..\..\"));

            string[] paths = { validFilePath, targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validFilePath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessPaths(paths,
                                                                                    processor,
                                                                                    includeSubDirectories: true);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validFilePath);
        }

        [Test]
        public void ProcessPaths_of_InValid_Path_without_Subdirectories_Does_Not_Add_Path()
        {
            string validPath = @"C:\Foo\Bar\Moo\Sar.txt";
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validPath).Returns(true);

            IProcessFile processor = new FileList(validator);

            FileList processorResult = (FileList)RecursiveFileProcessor.ProcessPaths(paths,
                                                                                    processor,
                                                                                    includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 0);
        }

        [Test]
        public void ProcessPaths_of_InValid_Path_without_Subdirectories_Events_Messenger()
        {
            string validPath = @"C:\Foo\Bar\Moo\Sar.txt";
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFile validator = Substitute.For<IValidateFile>();
            validator.IsValidFile(validPath).Returns(true);

            IProcessFile processor = new FileList(validator);

            var wasCalled = false;
            RecursiveFileProcessor.Messenger += (e) => wasCalled = true;

            RecursiveFileProcessor.ProcessPaths(paths,
                                                  processor,
                                                  includeSubDirectories: false);
            Assert.IsTrue(wasCalled);
        }
    }
}
