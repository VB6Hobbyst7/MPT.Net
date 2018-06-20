using NUnit.Framework;

using MPT.FileSystem.Validator;

namespace MPT.FileSystem.UnitTests.Validator
{
    [TestFixture]
    public class NameExtensionFileValidatorTests
    {
        [TestCase("Foo","Bar")]
        [TestCase("Foo", ".Bar")]
        public void Initialize_With_Separate_Name_And_Extension(string name, string extension)
        {
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(name, extension);

            Assert.AreEqual("Foo", nameExtensionFileValidator.Name);
            Assert.AreEqual("Bar", nameExtensionFileValidator.Extension);
            Assert.AreEqual(false, nameExtensionFileValidator.ExcludeFile);
        }

        [TestCase("Foo", "Bar")]
        [TestCase("Foo", ".Bar")]
        public void Initialize_With_Separate_Name_And_Extension_And_Exclude_Match(string name, string extension)
        {
            bool excludeFile = true;

            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(name, extension, excludeFile: excludeFile);

            Assert.AreEqual("Foo", nameExtensionFileValidator.Name);
            Assert.AreEqual("Bar", nameExtensionFileValidator.Extension);
            Assert.AreEqual(excludeFile, nameExtensionFileValidator.ExcludeFile);
        }

        [Test]
        public void Initialize_With_Combined_Name_And_Extension()
        {
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension);
            
            Assert.AreEqual(name, nameExtensionFileValidator.Name);
            Assert.AreEqual(extension, nameExtensionFileValidator.Extension);
            Assert.AreEqual(false, nameExtensionFileValidator.ExcludeFile);
        }

        [Test]
        public void Initialize_With_Combined_Name_And_Extension_And_Exclude_Match()
        {
            bool excludeFile = true;
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension, excludeFile: excludeFile);

            Assert.AreEqual(name, nameExtensionFileValidator.Name);
            Assert.AreEqual(extension, nameExtensionFileValidator.Extension);
            Assert.AreEqual(excludeFile, nameExtensionFileValidator.ExcludeFile);
        }

        [Test]
        public void IsValidFile_Of_Matching_File_of_Matches_to_be_Included_Returns_True()
        {
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            bool excludeFile = false;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension, excludeFile: excludeFile);

            Assert.IsTrue(nameExtensionFileValidator.IsValidFile(nameWithExtension));
        }

        [Test]
        public void IsValidFile_Of_Matching_File_of_Matches_to_be_Excluded_Returns_False()
        {
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            bool excludeFile = true;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension, excludeFile: excludeFile);

            Assert.IsFalse(nameExtensionFileValidator.IsValidFile(nameWithExtension));
        }

        [Test]
        public void IsValidFile_Of_NonMatching_File_of_Matches_to_be_Included_Returns_False()
        {
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            bool excludeFile = false;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension, excludeFile: excludeFile);

            string nonMatchingName = "Moo.Far";

            Assert.IsFalse(nameExtensionFileValidator.IsValidFile(nonMatchingName));
        }

        [Test]
        public void IsValidFile_Of_NonMatching_File_of_Matches_to_be_Excluded_Returns_True()
        {
            string name = "Foo";
            string extension = "bar";
            string nameWithExtension = name + "." + extension;
            bool excludeFile = true;
            NameExtensionFileValidator nameExtensionFileValidator = new NameExtensionFileValidator(nameWithExtension, excludeFile: excludeFile);

            string nonMatchingName = "Moo.Far";

            Assert.IsTrue(nameExtensionFileValidator.IsValidFile(nonMatchingName));
        }
    }
}
