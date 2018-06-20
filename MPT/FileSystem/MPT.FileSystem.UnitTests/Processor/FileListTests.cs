using NUnit.Framework;
using NSubstitute;

using MPT.FileSystem.Processor;
using MPT.FileSystem.Validator;

namespace MPT.FileSystem.UnitTests.Processor
{
    [TestFixture]
    public class FileListTests
    {
        [Test]
        public void Initialization()
        {
            IValidateFile validator = Substitute.For<IValidateFile>();
            FileList fileList = new FileList(validator);
            Assert.AreEqual(validator, fileList.FileValidator);
        }

        [Test]
        public void FileList_of_Valid_Path_Adds_Path()
        {
            IValidateFile validator = Substitute.For<IValidateFile>();
            
            FileList fileList = new FileList(validator);

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFile(filePath).Returns(true);

            fileList.ProcessFile(filePath);

            Assert.IsTrue(fileList.Paths.Count == 1);
            Assert.AreEqual(fileList.Paths[0], filePath);
        }

        [Test]
        public void FileList_of_Valid_Path_Events_Messenger()
        {
            IValidateFile validator = Substitute.For<IValidateFile>();

            FileList fileList = new FileList(validator);

            var wasCalled = false;
            FileProcessor.Messenger += (e) => wasCalled = true;

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFile(filePath).Returns(true);

            fileList.ProcessFile(filePath);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void FileList_of_Invalid_Path_Does_Not_AddsPath()
        {
            IValidateFile validator = Substitute.For<IValidateFile>();

            FileList fileList = new FileList(validator);

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFile(filePath).Returns(false);

            fileList.ProcessFile(filePath);

            Assert.IsTrue(fileList.Paths.Count == 0);
        }

        [Test]
        public void FileList_of_Invalid_Path_Does_Not_Event_Messenger()
        {
            IValidateFile validator = Substitute.For<IValidateFile>();

            FileList fileList = new FileList(validator);

            var wasCalled = false;
            FileProcessor.Messenger += (e) => wasCalled = true;

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFile(filePath).Returns(false);

            fileList.ProcessFile(filePath);

            Assert.IsFalse(wasCalled);
        }
    }
}
