using NUnit.Framework;
using NSubstitute;

using MPT.FileSystem.Processor;
using MPT.FileSystem.Validator;

namespace MPT.FileSystem.UnitTests.Processor
{
    [TestFixture]
    public class FolderListTests
    {
        [Test]
        public void Initialization()
        {
            IValidateFolder validator = Substitute.For<IValidateFolder>();
            FolderList folderList = new FolderList(validator);
            Assert.AreEqual(validator, folderList.FolderValidator);
        }

        [Test]
        public void FolderList_of_Valid_Path_Adds_Path()
        {
            IValidateFolder validator = Substitute.For<IValidateFolder>();

            FolderList folderList = new FolderList(validator);

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFolder(filePath).Returns(true);

            folderList.ProcessFolder(filePath);

            Assert.IsTrue(folderList.Paths.Count == 1);
            Assert.AreEqual(folderList.Paths[0], filePath);
        }

        [Test]
        public void FolderList_of_Valid_Path_Events_Messenger()
        {
            IValidateFolder validator = Substitute.For<IValidateFolder>();

            FolderList folderList = new FolderList(validator);

            var wasCalled = false;
            FolderProcessor.Messenger += (e) => wasCalled = true;

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFolder(filePath).Returns(true);

            folderList.ProcessFolder(filePath);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void FolderList_of_Invalid_Path_Does_Not_AddsPath()
        {
            IValidateFolder validator = Substitute.For<IValidateFolder>();

            FolderList folderList = new FolderList(validator);

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFolder(filePath).Returns(false);

            folderList.ProcessFolder(filePath);

            Assert.IsTrue(folderList.Paths.Count == 0);
        }

        [Test]
        public void FolderList_of_Invalid_Path_Does_Not_Event_Messenger()
        {
            IValidateFolder validator = Substitute.For<IValidateFolder>();

            FolderList folderList = new FolderList(validator);

            var wasCalled = false;
            FolderProcessor.Messenger += (e) => wasCalled = true;

            string filePath = @"C:\Foo\Bar";
            validator.IsValidFolder(filePath).Returns(false);

            folderList.ProcessFolder(filePath);

            Assert.IsFalse(wasCalled);
        }
    }
}
