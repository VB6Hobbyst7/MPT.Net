using System.IO;

using NUnit.Framework;
using NSubstitute;

using MPT.FileSystem.Processor;
using MPT.FileSystem.Validator;

namespace MPT.FileSystem.UnitTests.Processor
{
    [TestFixture]
    public class RecursiveFolderProcessorTests : LibraryTests_Base
    {
        [Test]
        public void ProcessDirectory_of_Valid_Path_without_Subdirectories_Adds_Path()
        {
            string targetDirectory = Path.GetFullPath(Path.Combine(_assemblyFolder, @"..\"));

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(_assemblyFolder).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessDirectory(targetDirectory,
                                                                                                        processor,
                                                                                                        includeSubDirectories: false);
            Assert.AreEqual(1, processorResult.Paths.Count);
            Assert.AreEqual(processorResult.Paths[0], _assemblyFolder);
        }

        [Test]
        public void ProcessDirectory_of_InValid_Path_without_Subdirectories_Does_Not_Add_Path()
        {
            string targetDirectory = @"C:\Foo\Bar";

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(targetDirectory).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessDirectory(targetDirectory,
                                                                                                        processor,
                                                                                                        includeSubDirectories: false);
            Assert.AreEqual(0, processorResult.Paths.Count);
        }

        [Test]
        public void ProcessDirectory_of_InValid_Path_without_Subdirectories_Events_Messenger()
        {
            string targetDirectory = @"C:\Foo\Bar";

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(targetDirectory).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            var wasCalled = false;
            RecursiveFolderProcessor.Messenger += (e) => wasCalled = true;

            RecursiveFolderProcessor.ProcessDirectory(targetDirectory,
                                                    processor,
                                                    includeSubDirectories: false);
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ProcessDirectory_of_Valid_Path_with_Subdirectories_Adds_All_Valid_Paths()
        {
            string validPath = _assemblyFolder;
            string targetDirectory = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(validPath).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessDirectory(targetDirectory,
                                                                                                        processor,
                                                                                                        includeSubDirectories: true);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validPath);
        }

        [Test]
        public void ProcessPaths_of_Valid_Path_without_Subdirectories_Adds_Path()
        {
            string validPath = _assemblyFolder;
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(targetDirectory1).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessPaths(paths,
                                                                                                    processor,
                                                                                                    includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], targetDirectory1);
        }

        [Test]
        public void ProcessPaths_of_Valid_Path_with_Subdirectories_Adds_Path()
        {
            string validPath = _assemblyFolder;
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(validPath).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessPaths(paths,
                                                                                                    processor,
                                                                                                    includeSubDirectories: true);
            Assert.IsTrue(processorResult.Paths.Count == 1);
            Assert.AreEqual(processorResult.Paths[0], validPath);
        }

        [Test]
        public void ProcessPaths_of_InValid_Path_without_Subdirectories_Does_Not_Add_Path()
        {
            string validPath = @"C:\Foo\Bar\Moo\Sar";
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(validPath).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            FolderList processorResult = (FolderList)RecursiveFolderProcessor.ProcessPaths(paths,
                                                                                                    processor,
                                                                                                    includeSubDirectories: false);
            Assert.IsTrue(processorResult.Paths.Count == 0);
        }

        [Test]
        public void ProcessPaths_of_InValid_Path_without_Subdirectories_Events_Messenger()
        {
            string validPath = @"C:\Foo\Bar\Moo\Sar";
            string targetDirectory1 = Path.GetFullPath(Path.Combine(validPath, @"..\"));
            string targetDirectory2 = Path.GetFullPath(Path.Combine(validPath, @"..\..\"));
            string targetDirectory3 = Path.GetFullPath(Path.Combine(validPath, @"..\..\..\"));

            string[] paths = { targetDirectory1, targetDirectory2, targetDirectory3 };

            IValidateFolder validator = Substitute.For<IValidateFolder>();
            validator.IsValidFolder(validPath).Returns(true);

            IProcessFolder processor = new FolderList(validator);

            var wasCalled = false;
            RecursiveFolderProcessor.Messenger += (e) => wasCalled = true;

            RecursiveFolderProcessor.ProcessPaths(paths,
                                                  processor,
                                                  includeSubDirectories: false);
            Assert.IsTrue(wasCalled);
        }
    }
}
