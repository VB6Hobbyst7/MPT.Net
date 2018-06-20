using System.IO;
using MPT.FileSystem.Validator;

using NUnit.Framework;

namespace MPT.FileSystem.UnitTests.Validator
{
    [TestFixture]
    public class ReadOnlyFileValidatorTests : FileLibraryTests_Base
    {
        [Test]
        public void Initialize()
        {
            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator();

            Assert.AreEqual(false, readOnlyFileValidator.ExcludeFile);
        }

        [Test]
        public void Initialize_And_Exclude_Match()
        {
            bool excludeFile = true;

            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator(excludeFile);

            Assert.AreEqual(excludeFile, readOnlyFileValidator.ExcludeFile);
        }


        [Test]
        public void IsValidFile_Of_ReadOnly_File_of_Matches_to_be_Included_Returns_True()
        {
            bool excludeFile = false;
            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator(excludeFile);

            File.SetAttributes(_pathOriginal, FileAttributes.ReadOnly);

            Assert.IsTrue(readOnlyFileValidator.IsValidFile(_pathOriginal));
        }

        [Test]
        public void IsValidFile_Of_ReadOnly_File_of_Matches_to_be_Excluded_Returns_False()
        {
            bool excludeFile = true;
            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator(excludeFile);

            File.SetAttributes(_pathOriginal, FileAttributes.ReadOnly);

            Assert.IsFalse(readOnlyFileValidator.IsValidFile(_pathOriginal));
        }

        [Test]
        public void IsValidFile_Of_Non_ReadOnly_File_of_Matches_to_be_Included_Returns_False()
        {
            bool excludeFile = false;
            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator(excludeFile);

            Assert.IsFalse(readOnlyFileValidator.IsValidFile(_pathOriginal));
        }

        [Test]
        public void IsValidFile_Of_Non_ReadOnly_File_of_Matches_to_be_Excluded_Returns_True()
        {
            bool excludeFile = true;
            ReadOnlyFileValidator readOnlyFileValidator = new ReadOnlyFileValidator(excludeFile);

            Assert.IsTrue(readOnlyFileValidator.IsValidFile(_pathOriginal));
        }
    }
}
