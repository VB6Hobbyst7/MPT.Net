using MPT.FileSystem.Validator;
using MPT.String;
using NUnit.Framework;

namespace MPT.FileSystem.UnitTests.Validator
{
    [TestFixture]
    public class NameFolderValidatorTests
    {
        [TestCase("FooBar")]
        [TestCase("FooBar.MooSar")]
        [TestCase(@"C:\Foo\FooBar")]
        [TestCase(@"C:\Foo\FooBar.MooSar")]
        public void Initialize(string name)
        {
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name);

            string expectedName = StringLibrary.GetSuffix(name, @"\");

            Assert.AreEqual(expectedName, nameFolderValidator.Name);
            Assert.AreEqual(false, nameFolderValidator.ExcludeFolder);
        }

        [TestCase("FooBar")]
        [TestCase("FooBar.MooSar")]
        [TestCase(@"C:\Foo\FooBar")]
        [TestCase(@"C:\Foo\FooBar.MooSar")]
        public void Initialize_And_Exclude_Match(string name)
        {
            bool excludeFolder = true;
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name, excludeFolder: excludeFolder);

            string expectedName = StringLibrary.GetSuffix(name, @"\");

            Assert.AreEqual(expectedName, nameFolderValidator.Name);
            Assert.AreEqual(excludeFolder, nameFolderValidator.ExcludeFolder);
        }

        [TestCase("FooBar", "FooBar")]
        [TestCase(@"C:\Foo\FooBar", "FooBar")]
        [TestCase(@"C:\Foo\FooBar", @"C:\Foo\FooBar")]
        [TestCase(@"C:\Foo\Foo.Bar", "Foo.Bar")]
        [TestCase(@"C:\Foo\Foo.Bar", @"C:\Foo\Bar\Foo.Bar")]
        public void IsValidFolder_Of_Matching_Folder_of_Matches_to_be_Included_Returns_True(string name, string folderMatch)
        {
            bool excludeFolder = false;
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name, excludeFolder: excludeFolder);

            Assert.IsTrue(nameFolderValidator.IsValidFolder(folderMatch));
        }

        [TestCase("FooBar", "FooBar")]
        [TestCase(@"C:\Foo\FooBar", "FooBar")]
        [TestCase(@"C:\Foo\FooBar", @"C:\Foo\FooBar")]
        [TestCase(@"C:\Foo\Foo.Bar", "Foo.Bar")]
        [TestCase(@"C:\Foo\Foo.Bar", @"C:\Foo\Bar\Foo.Bar")]
        public void IsValidFolder_Of_Matching_Folder_of_Matches_to_be_Excluded_Returns_False(string name, string folderMatch)
        {
            bool excludeFolder = true;
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name, excludeFolder: excludeFolder);

            Assert.IsFalse(nameFolderValidator.IsValidFolder(folderMatch));
        }

        [TestCase("FooBar", "Nar")]
        [TestCase("FooBar", "Noo")]
        [TestCase("FooBar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar", "NooNar")]
        [TestCase(@"C:\Foo\FooBarMooSar", @"C:\Foo\NooNar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Noo.Nar")]
        public void IsValidFolder_Of_NonMatching_Folder_of_Matches_to_be_Included_Returns_False(string name, string folderMatch)
        {
            bool excludeFolder = false;
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name, excludeFolder: excludeFolder);

            Assert.IsFalse(nameFolderValidator.IsValidFolder(folderMatch));
        }

        [TestCase("FooBar", "Nar")]
        [TestCase("FooBar", "Noo")]
        [TestCase("FooBar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar", "NooNar")]
        [TestCase(@"C:\Foo\FooBarMooSar", @"C:\Foo\NooNar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Noo.Nar")]
        public void IsValidFolder_Of_NonMatching_Folder_of_Matches_to_be_Excluded_Returns_True(string name, string folderMatch)
        {
            bool excludeFolder = true;
            NameFolderValidator nameFolderValidator = new NameFolderValidator(name, excludeFolder: excludeFolder);

            Assert.IsTrue(nameFolderValidator.IsValidFolder(folderMatch));
        }
    }
}
