using MPT.FileSystem.Validator;

using NUnit.Framework;

namespace MPT.FileSystem.UnitTests.Validator
{
    [TestFixture]
    public class PartialNameMatchFileValidatorTests
    {
        [TestCase("FooBar")]
        [TestCase("FooBar.MooSar")]
        [TestCase(@"C:\Foo\FooBar.MooSar")]
        public void Initialize(string name)
        {
            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name);

            Assert.AreEqual("FooBar", partialNameMatchValidator.Name);
            Assert.AreEqual(false, partialNameMatchValidator.ExcludeFile);
        }

        [TestCase("FooBar")]
        [TestCase("FooBar.MooSar")]
        [TestCase(@"C:\Foo\FooBar.MooSar")]
        public void Initialize_And_Exclude_Match(string name)
        {
            bool excludeFile = true;

            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name, excludeFile: excludeFile);

            Assert.AreEqual("FooBar", partialNameMatchValidator.Name);
            Assert.AreEqual(excludeFile, partialNameMatchValidator.ExcludeFile);
        }
        

        [TestCase("FooBar", "Bar")]
        [TestCase("FooBar", "Foo")]
        [TestCase("FooBar", "Foo.Bar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Foo.Bar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Foo.Bar")]
        public void IsValidFile_Of_Matching_File_of_Matches_to_be_Included_Returns_True(string name, string partialMatch)
        {
            bool excludeFile = false;
            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name, excludeFile: excludeFile);

            Assert.IsTrue(partialNameMatchValidator.IsValidFile(partialMatch));
        }

        [TestCase("FooBar", "Bar")]
        [TestCase("FooBar", "Foo")]
        [TestCase("FooBar", "Foo.Bar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Foo.Bar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Foo.Bar")]
        public void IsValidFile_Of_Matching_File_of_Matches_to_be_Excluded_Returns_False(string name, string partialMatch)
        {
            bool excludeFile = true;
            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name, excludeFile: excludeFile);

            Assert.IsFalse(partialNameMatchValidator.IsValidFile(partialMatch));
        }

        [TestCase("FooBar", "Nar")]
        [TestCase("FooBar", "Noo")]
        [TestCase("FooBar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Noo.Nar")]
        public void IsValidFile_Of_NonMatching_File_of_Matches_to_be_Included_Returns_False(string name, string partialMatch)
        {
            bool excludeFile = false;
            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name, excludeFile: excludeFile);

            Assert.IsFalse(partialNameMatchValidator.IsValidFile(partialMatch));
        }

        [TestCase("FooBar", "Nar")]
        [TestCase("FooBar", "Noo")]
        [TestCase("FooBar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", "Noo.Nar")]
        [TestCase(@"C:\Foo\FooBar.MooSar", @"C:\Foo\Noo.Nar")]
        public void IsValidFile_Of_NonMatching_File_of_Matches_to_be_Excluded_Returns_True(string name, string partialMatch)
        {
            bool excludeFile = true;
            PartialNameMatchValidator partialNameMatchValidator = new PartialNameMatchValidator(name, excludeFile: excludeFile);

            Assert.IsTrue(partialNameMatchValidator.IsValidFile(partialMatch));
        }
    }
}
