using System.Collections.Generic;
using MPT.String;
using NUnit.Framework;

namespace MPT.FileSystem.UnitTests
{
    [TestFixture]
    public class StringLibraryTests_String_Query
    {
        [TestCase(null, ExpectedResult = false)]
        [TestCase(' ', ExpectedResult = false)]
        [TestCase('b', ExpectedResult = false)]
        [TestCase('B', ExpectedResult = false)]
        [TestCase('a', ExpectedResult = true)]
        [TestCase('e', ExpectedResult = true)]
        [TestCase('i', ExpectedResult = true)]
        [TestCase('o', ExpectedResult = true)]
        [TestCase('u', ExpectedResult = true)]
        [TestCase('A', ExpectedResult = true)]
        [TestCase('E', ExpectedResult = true)]
        [TestCase('I', ExpectedResult = true)]
        [TestCase('O', ExpectedResult = true)]
        [TestCase('U', ExpectedResult = true)]
        public bool IsVowel(char character)
        {
            return StringLibrary.IsVowel(character);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("foo bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "foo bar", ExpectedResult = true)]
        public bool StringsMatch_Case_Insensitive(string string1, string string2)
        {
            return StringLibrary.StringsMatch(string1, string2, caseSensitive: false);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("foo bar", "Foo Bar", ExpectedResult = false)]
        [TestCase("Foo Bar", "foo bar", ExpectedResult = false)]
        public bool StringsMatch_Case_Sensitive(string string1, string string2)
        {
            return StringLibrary.StringsMatch(string1, string2, caseSensitive: true);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("1, 2, 3, 4", " ", ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = true)]
        public bool StringExistInName_Case_Insensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.StringExistInName(stringSearched, stringToSearchFor, caseSensitive: false);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("1, 2, 3, 4", " ", ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("foo", "foobar", ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = true)]
        public bool StringExistInName_Case_Sensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.StringExistInName(stringSearched, stringToSearchFor, caseSensitive: true);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("foo bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "foo bar", ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = false)]
        public bool IsNameMatching_Case_Insensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.IsNameMatching(stringSearched, stringToSearchFor, partialNameMatch: false,
                caseSensitive: false);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("foo bar", "Foo Bar", ExpectedResult = false)]
        [TestCase("Foo Bar", "foo bar", ExpectedResult = false)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = false)]
        public bool IsNameMatching_Case_Sensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.IsNameMatching(stringSearched, stringToSearchFor, partialNameMatch: false,
                caseSensitive: true);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = true)]
        public bool IsNameMatching_Partial_Match_Case_Insensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.IsNameMatching(stringSearched, stringToSearchFor, partialNameMatch: true,
                caseSensitive: false);
        }

        [TestCase("", "", ExpectedResult = true)]
        [TestCase(" ", " ", ExpectedResult = true)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("foo bar", "foo bar", ExpectedResult = true)]
        [TestCase("Foo Bar", "Foo Bar", ExpectedResult = true)]
        [TestCase("", "g", ExpectedResult = false)]
        [TestCase("g", "", ExpectedResult = false)]
        [TestCase(" ", "g", ExpectedResult = false)]
        [TestCase("g", " ", ExpectedResult = false)]
        [TestCase(null, "g", ExpectedResult = false)]
        [TestCase("g", null, ExpectedResult = false)]
        [TestCase("this longer sentence contains foo bar somewhere", "foo bar", ExpectedResult = true)]
        [TestCase("this longer sentence contains foo bar somewhere", "Foo Bar", ExpectedResult = false)]
        [TestCase("this longer sentence contains Foo Bar somewhere", "Foo Bar", ExpectedResult = true)]
        public bool IsNameMatching_Partial_Match_Case_Sensitive(string stringSearched, string stringToSearchFor)
        {
            return StringLibrary.IsNameMatching(stringSearched, stringToSearchFor, partialNameMatch: true,
                caseSensitive: true);
        }

        [TestCase(" ", 1, " ", ExpectedResult = true)]
        [TestCase("Bar", 4, "FooBar", ExpectedResult = true)]
        [TestCase("Bar", 3, "FooBar", ExpectedResult = false)]
        [TestCase("", 3, "FooBar", ExpectedResult = false)]
        [TestCase(null, 3, "FooBar", ExpectedResult = false)]
        [TestCase("Bar", 4, "", ExpectedResult = false)]
        [TestCase("Bar", 4, " ", ExpectedResult = false)]
        [TestCase("Bar", 4, null, ExpectedResult = false)]
        public bool StringFoundAtIndex(string stringSearchedFor, int index, string originalText)
        {
            return StringLibrary.StringFoundAtIndex(stringSearchedFor, index, originalText);
        }

        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase("Foo", ExpectedResult = false)]
        [TestCase("  Foo  Bar  ", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = true)]
        [TestCase("                      ", ExpectedResult = true)]
        public bool HasAllWhiteSpace(string text)
        {
            return StringLibrary.HasAllWhiteSpace(text);
        }
    }

    [TestFixture]
    public class StringLibraryTests_Get_Set_Portions_of_String
    {
        [TestCase("", "", ExpectedResult ="")]
        [TestCase(" ", " ", ExpectedResult = "")]
        [TestCase(null, null, ExpectedResult = "")]
        [TestCase("Foo Bar", "", ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar", null, ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar", " ", ExpectedResult = "Foo")]
        [TestCase("Foo.Bar", ".", ExpectedResult = "Foo")]
        [TestCase("Foo.Bar", ".B", ExpectedResult = "Foo")]
        [TestCase("Foo.Bar", "$", ExpectedResult = "Foo.Bar")]
        [TestCase("Foo.Bar", "F", ExpectedResult = "Foo.Bar")]
        [TestCase("Foo.Bar", "o", ExpectedResult = "F")]
        public string GetPrefix(string name, string character)
        {
            return StringLibrary.GetPrefix(name, character);
        }

        [TestCase("", "", ExpectedResult = "")]
        [TestCase(" ", " ", ExpectedResult = "")]
        [TestCase(null, null, ExpectedResult = "")]
        [TestCase("Foo Bar", "", ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar", null, ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar", " ", ExpectedResult = "Bar")]
        [TestCase("Foo.Bar", ".", ExpectedResult = "Bar")]
        [TestCase("Foo.Bar", ".B", ExpectedResult = "Bar")]
        [TestCase("Foo.Bar", "$", ExpectedResult = "Foo.Bar")]
        [TestCase("Foo.Bar", "r", ExpectedResult = "Foo.Bar")]
        [TestCase("Foo.Bar", "a", ExpectedResult = "r")]
        public string GetSuffix(string name, string character)
        {
            return StringLibrary.GetSuffix(name, character);
        }

        [TestCase("Foo Bar Moo", "", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", null, ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", " ", ExpectedResult = "FooBar Moo")]
        [TestCase("Foo Bar Moo", "Bar ", ExpectedResult = "Foo Moo")]
        [TestCase("Foo Bar Moo", "Foo ", ExpectedResult = "Bar Moo")]
        [TestCase("Foo Bar Moo", " Moo", ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar Moo", "Not Present", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo Too", "Bar Moo ", ExpectedResult = "Foo Too")]
        [TestCase("Foo Bar Moo Too", "Foo Bar Moo Too", ExpectedResult = "")]
        [TestCase("Foo Bar Moo Too", "Foo Bar Moo Too Soo", ExpectedResult = "Foo Bar Moo Too")]
        public string FilterFromText_Retain_Prefix_And_Suffix(string name, string filterString)
        {
            return StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: true);
        }

        [TestCase("Foo Bar Moo", "", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", null, ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", " ", ExpectedResult = "Foo")]
        [TestCase("Foo Bar Moo", "Bar ", ExpectedResult = "Foo ")]
        [TestCase("Foo Bar Moo", "Foo ", ExpectedResult = "")]
        [TestCase("Foo Bar Moo", " Moo", ExpectedResult = "Foo Bar")]
        [TestCase("Foo Bar Moo", "Not Present", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo Too", "Bar Moo ", ExpectedResult = "Foo ")]
        public string FilterFromText_Retain_Prefix_Only(string name, string filterString)
        {
            return StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: false);
        }

        [TestCase("Foo Bar Moo", "", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", null, ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo", " ", ExpectedResult = "Bar Moo")]
        [TestCase("Foo Bar Moo", "Bar ", ExpectedResult = "Moo")]
        [TestCase("Foo Bar Moo", "Foo ", ExpectedResult = "Bar Moo")]
        [TestCase("Foo Bar Moo", " Moo", ExpectedResult = "")]
        [TestCase("Foo Bar Moo", "Not Present", ExpectedResult = "Foo Bar Moo")]
        [TestCase("Foo Bar Moo Too", "Bar Moo ", ExpectedResult = "Too")]
        public string FilterFromText_Retain_Suffix_Only(string name, string filterString)
        {
            return StringLibrary.FilterFromText(name, filterString, retainPrefix: false, retainSuffix: true);
        }

        [TestCase("Foo Bar Moo Too", "Bar Moo ", ExpectedResult = "Foo Too")]
        [TestCase("Foo Bar Moo Too", "bar moo ", ExpectedResult = "Foo Bar Moo Too")]
        [TestCase("Foo Bar Moo Too", "Bar moo ", ExpectedResult = "Foo Bar Moo Too")]
        public string FilterFromText_Retain_Prefix_And_Suffix_Case_Sensitive(string name, string filterString)
        {
            return StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: true, caseSensitive: true);
        }

        [TestCase(@"Foo\Bar\Moo\Too", @"\Too", ExpectedResult = @"Foo\Bar\Moo")]
        [TestCase(@"Foo\Too\Bar\Moo\Too", @"\Too", ExpectedResult = @"Foo\Too\Bar\Moo")]
        public string FilterFromText_Remove_Final_Directory_In_Path(string name, string filterString)
        {
            return StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: true, filterEndOfName: true);
        }

        //[TestCase("Foo Bar Moo Too", "Not Found")]
        //public void FilterFromText_Events_Messenger(string name, string filterString)
        //{
        //    var wasCalled = false;
        //    StringLibrary.Messenger += (e) => wasCalled = true;

        //    StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: true, suppressWarnings: false);

        //    Assert.IsTrue(wasCalled);
        //}

        //[TestCase("Foo Bar Moo Too", "Bar")]
        //public void FilterFromText_No_Events_Messenger(string name, string filterString)
        //{
        //    var wasCalled = false;
        //    StringLibrary.Messenger += (e) => wasCalled = true;

        //    StringLibrary.FilterFromText(name, filterString, retainPrefix: true, retainSuffix: true, suppressWarnings: false);

        //    Assert.IsFalse(wasCalled);
        //}

        [TestCase("", "Foo", "Bar", ExpectedResult ="")]
        [TestCase(null, "Foo", "Bar", ExpectedResult = "")]
        [TestCase(" ", "Foo", "Bar", ExpectedResult = "")]
        [TestCase("FooBar", "", "Bar", ExpectedResult = "FooBar")]
        [TestCase("FooBar", null, "Bar", ExpectedResult = "FooBar")]
        [TestCase("FooBar", " ", "Bar", ExpectedResult = "FooBar")]
        [TestCase("FooBar", "Foo", "", ExpectedResult = "Bar")]
        [TestCase("FooBar", "Foo", null, ExpectedResult = "Bar")]
        [TestCase("FooBar", "Foo", " ", ExpectedResult = " Bar")]
        [TestCase("FooBar", "Foo", "Bar", ExpectedResult = "BarBar")]
        [TestCase("FooBar", "FooBarFoo", "Bar", ExpectedResult = "FooBar")]
        [TestCase("FooBar", "foo", "Bar", ExpectedResult = "BarBar")]
        [TestCase("FooBar", "Moo", "Bar", ExpectedResult = "FooBar")]
        [TestCase("FooBar", "FooBar", "BarFoo", ExpectedResult = "FooBar")]
        public string ReplaceInText(string oldName, string oldSubString, string newSubString)
        {
            return StringLibrary.ReplaceInText(oldName, oldSubString, newSubString);
        }

        [TestCase("FooBar", "Foo", "Bar", ExpectedResult = "BarBar")]
        [TestCase("FooBar", "FooBar", "BarFoo", ExpectedResult = "BarFoo")]
        public string ReplaceInText_Can_Replace_All(string oldName, string oldSubString, string newSubString)
        {
            return StringLibrary.ReplaceInText(oldName, oldSubString, newSubString, canReplaceAll: true);
        }

        [TestCase("FooBar", "Foo", "Bar", ExpectedResult = "BarBar")]
        [TestCase("FooBar", "foo", "Bar", ExpectedResult = "FooBar")]
        public string ReplaceInText_Case_Sensitive(string oldName, string oldSubString, string newSubString)
        {
            return StringLibrary.ReplaceInText(oldName, oldSubString, newSubString, caseSensitive: true);
        }

        //[TestCase("FooBar", "Moo", "Bar")]
        //public void ReplaceInText_Events_Messenger(string oldName, string oldSubString, string newSubString)
        //{
        //    var wasCalled = false;
        //    StringLibrary.Messenger += (e) => wasCalled = true;

        //    StringLibrary.ReplaceInText(oldName, oldSubString, newSubString, suppressWarnings: false);

        //    Assert.IsTrue(wasCalled);
        //}

        //[TestCase("FooBar", "Foo", "Bar")]
        //public void ReplaceInText_No_Events_Messenger(string oldName, string oldSubString, string newSubString)
        //{
        //    var wasCalled = false;
        //    StringLibrary.Messenger += (e) => wasCalled = true;

        //    StringLibrary.ReplaceInText(oldName, oldSubString, newSubString, suppressWarnings: false);

        //    Assert.IsFalse(wasCalled);
        //}

        [TestCase("", 2, ExpectedResult = "")]
        [TestCase(null, 2, ExpectedResult = "")]
        [TestCase(" ", 2, ExpectedResult = "")]
        [TestCase("FooBar", -2, ExpectedResult = "FooBar")]
        [TestCase("FooBar", -1, ExpectedResult = "FooBar")]
        [TestCase("FooBar", 0, ExpectedResult = "FooBar")]
        [TestCase("FooBar", 1, ExpectedResult = "")]
        [TestCase("FooBar", 2, ExpectedResult = "F")]
        [TestCase("FooBar", 6, ExpectedResult = "FooBa")]
        [TestCase("FooBar", 7, ExpectedResult = "FooBar")]
        [TestCase("FooBar", 45, ExpectedResult = "FooBar")]
        public string LeftOfIndex(string text, int index)
        {
            return StringLibrary.LeftOfIndex(text, index);
        }

        [TestCase("", 2, ExpectedResult = "")]
        [TestCase(null, 2, ExpectedResult = "")]
        [TestCase(" ", 2, ExpectedResult = "")]
        [TestCase("FooBar", -2, ExpectedResult = "FooBar")]
        [TestCase("FooBar", -1, ExpectedResult = "FooBar")]
        [TestCase("FooBar", 0, ExpectedResult ="FooBar")]
        [TestCase("FooBar", 1, ExpectedResult = "ooBar")]
        [TestCase("FooBar", 2, ExpectedResult = "oBar")]
        [TestCase("FooBar", 5, ExpectedResult = "r")]
        [TestCase("FooBar", 7, ExpectedResult = "")]
        [TestCase("FooBar", 45, ExpectedResult = "")]
        public string RightOfIndex(string text, int index)
        {
            return StringLibrary.RightOfIndex(text, index);
        }

        [TestCase("", ExpectedResult ="")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase("1 foo equals 2 bar for 3 foobars", ExpectedResult = "123")]
        [TestCase("1.4 foo equals 2 bar for 3.2 foobars", ExpectedResult = "1.423.2")]
        [TestCase("-1 foo equals 2 bar for +3 foobars", ExpectedResult = "-12+3")]
        public string FilterToNumeric(string stringToFilter)
        {
            return StringLibrary.FilterToNumeric(stringToFilter);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase("1.4 foo equals 2 bar for 3.2 foobars", ExpectedResult = "1.4 2 3.2")]
        [TestCase("-1 foo equals 2 bar for +3 foobars", ExpectedResult = "-1 2 +3")]
        public string FilterToNumeric_Keeping_Spaces(string stringToFilter)
        {
            return StringLibrary.FilterToNumeric(stringToFilter, keepSpaces: true);
        }

        [TestCase("", "", "", ExpectedResult ="")]
        [TestCase(" ", "", "", ExpectedResult = "")]
        [TestCase("", " ", "", ExpectedResult = "")]
        [TestCase("", "", " ", ExpectedResult = "")]
        [TestCase(" ", " ", " ", ExpectedResult = "")]
        [TestCase(null, "", "", ExpectedResult = "")]
        [TestCase("", null, "", ExpectedResult = "")]
        [TestCase("", "", null, ExpectedResult = "")]
        [TestCase(null, null, null, ExpectedResult = "")]
        [TestCase("foobarfoo", "", "", ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", "", "", ExpectedResult = "foo bar foo")]
        [TestCase("foobarfoo", " ", "", ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", " ", "", ExpectedResult = "foobar foo")]
        [TestCase("foobarfoo", null, "", ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", null, "", ExpectedResult = "foo bar foo")]
        [TestCase("foobarfoo", "", null, ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", "", null, ExpectedResult = "foo bar foo")]
        [TestCase("foobarfoo", " ", null, ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", " ", null, ExpectedResult = "foobar foo")]
        [TestCase("foobarfoo", null, null, ExpectedResult = "foobarfoo")]
        [TestCase("foo bar foo", null, null, ExpectedResult = "foo bar foo")]
        [TestCase("foobarfoo", "foo", "moo", ExpectedResult = "moobarfoo")]
        [TestCase("foo bar foo", "foo", "moo", ExpectedResult = "moo bar foo")]
        [TestCase("foo bar foo", "soo", "moo", ExpectedResult = "foo bar foo")]
        public string ReplaceFirst(string text, string textSearch, string textReplace)
        {
            return StringLibrary.ReplaceFirst(text, textSearch, textReplace);
        }
    }

    
    [TestFixture]
    public class StringLibraryTests_Lists
    {
        private static readonly List<string> stringsTwo = new List<string>() { "one", "two" };
        private static readonly List<string> stringsThree = new List<string>() { "one", "two", "three" };

        [TestCase("", ExpectedResult = "one, two, three")]
        [TestCase(" ", ExpectedResult = "one, two, three")]
        [TestCase(null, ExpectedResult = "one, two, three")]
        [TestCase("and", ExpectedResult = "one, and two, and three")]
        [TestCase("or", ExpectedResult = "one, or two, or three")]
        public string ConcatenateListToMessage(string joiner)
        {
            return StringLibrary.ConcatenateListToMessage(stringsThree, joiner);
        }

        [TestCase("", ExpectedResult = "one, two, three")]
        [TestCase(" ", ExpectedResult = "one, two, three")]
        [TestCase(null, ExpectedResult = "one, two, three")]
        [TestCase("and", ExpectedResult = "one and two and three")]
        [TestCase("or", ExpectedResult = "one or two or three")]
        public string ConcatenateListToMessage_Always_Using_Joiner(string joiner)
        {
            return StringLibrary.ConcatenateListToMessage(stringsThree, joiner, alwaysUseJoiner: true);
        }

        [TestCase("", ExpectedResult = "one, two")]
        [TestCase(" ", ExpectedResult = "one, two")]
        [TestCase(null, ExpectedResult = "one, two")]
        [TestCase("and", ExpectedResult = "one and two")]
        [TestCase("or", ExpectedResult = "one or two")]
        public string ConcatenateListToMessage_of_Two_Items_Always_Using_Joiner(string joiner)
        {
            return StringLibrary.ConcatenateListToMessage(stringsTwo, joiner, alwaysUseJoiner: true);
        }

        [TestCase("", ExpectedResult = "one, two")]
        [TestCase(" ", ExpectedResult = "one, two")]
        [TestCase(null, ExpectedResult = "one, two")]
        [TestCase("and", ExpectedResult = "one and two")]
        [TestCase("or", ExpectedResult = "one or two")]
        public string ConcatenateListToMessage_of_Two_Items_Not_Always_Using_Joiner(string joiner)
        {
            return StringLibrary.ConcatenateListToMessage(stringsTwo, joiner);
        }

        [TestCase("", "always ", ExpectedResult = "always one, always two, always three")]
        [TestCase(" ", "always ", ExpectedResult = "always one, always two, always three")]
        [TestCase(null, "always ", ExpectedResult = "always one, always two, always three")]
        [TestCase("and", "always ", ExpectedResult = "always one, and always two, and always three")]
        [TestCase("or", "always ", ExpectedResult = "always one, or always two, or always three")]
        public string ConcatenateListToMessage_with_Prefix(string joiner, string prefix)
        {
            return StringLibrary.ConcatenateListToMessage(stringsThree, joiner, prefix: prefix);
        }

        [TestCase("one, two, three", "")]
        [TestCase("one, two, three", " ")]
        [TestCase("one, two, three", null)]
        [TestCase("one, and two, and three", "and")]
        [TestCase("one, or two, or three", "or")]
        public void SplitMessageToList(string message, string joiner)
        {
            Assert.Contains("one", StringLibrary.SplitMessageToList(message, joiner));
            Assert.Contains("two", StringLibrary.SplitMessageToList(message, joiner));
            Assert.Contains("three", StringLibrary.SplitMessageToList(message, joiner));
        }

        [TestCase("one, two, three", "")]
        [TestCase("one, two, three", " ")]
        [TestCase("one, two, three", null)]
        [TestCase("one, and two, and three", "and")]
        [TestCase("one, or two, or three", "or")]
        public void SplitMessageToList_Always_Using_Joiner(string message, string joiner)
        {
            Assert.Contains("one", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: true));
            Assert.Contains("two", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: true));
            Assert.Contains("three", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: true));
        }

        [TestCase("one, two", "")]
        [TestCase("one, two", " ")]
        [TestCase("one, two", null)]
        [TestCase("one, and two", "and")]
        [TestCase("one, or two", "or")]
        public void SplitMessageToList_of_Two_Items_Always_Using_Joiner(string message, string joiner)
        {
            Assert.Contains("one", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: true));
            Assert.Contains("two", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: true));
        }

        [TestCase("one, two", "")]
        [TestCase("one, two", " ")]
        [TestCase("one, two", null)]
        [TestCase("one, and two", "and")]
        [TestCase("one, or two", "or")]
        public void SplitMessageToList_of_Two_Items_Not_Always_Using_Joiner(string message, string joiner)
        {
            Assert.Contains("one", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: false));
            Assert.Contains("two", StringLibrary.SplitMessageToList(message, joiner, alwaysUseJoiner: false));
        }

        [TestCase("always one, always two, always three", "", "always ")]
        [TestCase("always one, always two, always three", " ", "always ")]
        [TestCase("always one, always two, always three", null, "always ")]
        [TestCase("always one, and always two, and always three", "and", "always ")]
        [TestCase("always one, or always two, or always three", "or", "always ")]
        public void SplitMessageToList_with_Prefix(string message, string joiner, string prefix)
        {
            Assert.Contains("one", StringLibrary.SplitMessageToList(message, joiner, prefix: prefix));
            Assert.Contains("two", StringLibrary.SplitMessageToList(message, joiner, prefix: prefix));
            Assert.Contains("three", StringLibrary.SplitMessageToList(message, joiner, prefix: prefix));
        }

        [Test]
        public void FilterListFromList_Of_Intersecting_Lists_Returns_Filtered_List()
        {
            List<string> filterList = new List<string>() { "Foo", "Bar"};
            List<string> remnantList = new List<string>() { "One", "Two", "Three" };
            List<string> combinedList = new List<string>();
            combinedList.Add(remnantList[0]);
            combinedList.Add(filterList[0]);
            combinedList.Add(remnantList[1]);
            combinedList.Add(filterList[1]);
            combinedList.Add(remnantList[2]);

            List<string> reducedList = StringLibrary.FilterListFromList(combinedList, filterList);

            Assert.AreEqual(remnantList[0], reducedList[0]);
            Assert.AreEqual(remnantList[1], reducedList[1]);
            Assert.AreEqual(remnantList[2], reducedList[2]);
        }

        [Test]
        public void FilterListFromList_Of_NonIntersecting_Lists_Returns_Base_List()
        {
            List<string> filterList = new List<string>() { "Foo", "Bar" };
            List<string> remnantList = new List<string>() { "One", "Two", "Three" };
            List<string> combinedList = remnantList;
            
            List<string> reducedList = StringLibrary.FilterListFromList(combinedList, filterList);

            Assert.AreEqual(remnantList[0], reducedList[0]);
            Assert.AreEqual(remnantList[1], reducedList[1]);
            Assert.AreEqual(remnantList[2], reducedList[2]);
        }

        [Test]
        public void FilterListFromList_Of_Null_Base_List_Returns_Empty_List()
        {
            List<string> filterList = new List<string>() { "Foo", "Bar" };
            List<string> combinedList = null;

            List<string> reducedList = StringLibrary.FilterListFromList(combinedList, filterList);

            Assert.AreEqual(0, reducedList.Count);
        }

        [Test]
        public void FilterListFromList_Of_Null_Filter_List_Returns_Base_List()
        {
            List<string> filterList = null;
            List<string> remnantList = new List<string>() { "One", "Two", "Three" };
            List<string> combinedList = remnantList;

            List<string> reducedList = StringLibrary.FilterListFromList(combinedList, filterList);

            Assert.AreEqual(remnantList[0], reducedList[0]);
            Assert.AreEqual(remnantList[1], reducedList[1]);
            Assert.AreEqual(remnantList[2], reducedList[2]);
        }
    }

    [TestFixture]
    public class StringLibraryTests_Adjusting_Cleaning
    {
        [TestCase("", '\'', ExpectedResult = "")]
        [TestCase(" ", '\'', ExpectedResult = " ")]
        [TestCase(null, '\'', ExpectedResult = "")]
        [TestCase(@"'Foo bar'", '\'', ExpectedResult = "Foo bar")]
        [TestCase(@"""Foo bar""", '\"', ExpectedResult = "Foo bar")]
        [TestCase(@"$Foo bar$", '$', ExpectedResult = "Foo bar")]
        public string TrimCharacterFromEnds_Both(string stringTrim, char character)
        {
            return StringLibrary.TrimCharacterFromEnds(stringTrim, character);
        }

        [TestCase("", '\'', ExpectedResult = "")]
        [TestCase(" ", '\'', ExpectedResult = " ")]
        [TestCase(null, '\'', ExpectedResult = "")]
        [TestCase(@"'Foo bar'", '\'', ExpectedResult = @"'Foo bar")]
        [TestCase(@"""Foo bar""", '\"', ExpectedResult = @"""Foo bar")]
        [TestCase(@"$Foo bar$", '$', ExpectedResult = "$Foo bar")]
        public string TrimCharacterFromEnds_Right(string stringTrim, char character)
        {
            return StringLibrary.TrimCharacterFromEnds(stringTrim, character, trimLeft: false);
        }

        [TestCase("", '\'', ExpectedResult = "")]
        [TestCase(" ", '\'', ExpectedResult = " ")]
        [TestCase(null, '\'', ExpectedResult = "")]
        [TestCase(@"'Foo bar'", '\'', ExpectedResult = @"Foo bar'")]
        [TestCase(@"""Foo bar""", '\"', ExpectedResult = @"Foo bar""")]
        [TestCase(@"$Foo bar$", '$', ExpectedResult = "Foo bar$")]
        public string TrimCharacterFromEnds_Left(string stringTrim, char character)
        {
            return StringLibrary.TrimCharacterFromEnds(stringTrim, character, trimRight: false);
        }

        [TestCase("", '\'', ExpectedResult = "''")]
        [TestCase(" ", '\'', ExpectedResult = "' '")]
        [TestCase(null, '\'', ExpectedResult = "''")]
        [TestCase("Foo bar", '\'', ExpectedResult = @"'Foo bar'")]
        [TestCase("Foo bar", '\"', ExpectedResult = @"""Foo bar""")]
        [TestCase("Foo bar", '$', ExpectedResult = @"$Foo bar$")]
        public string AddCharacterToEnds_Both(string stringAdd, char character)
        {
            return StringLibrary.AddCharacterToEnds(stringAdd, character);
        }

        [TestCase("", '\'', ExpectedResult = "'")]
        [TestCase(" ", '\'', ExpectedResult = " '")]
        [TestCase(null, '\'', ExpectedResult = "'")]
        [TestCase("Foo bar", '\'', ExpectedResult = @"Foo bar'")]
        [TestCase("Foo bar", '\"', ExpectedResult = @"Foo bar""")]
        [TestCase("Foo bar", '$', ExpectedResult = @"Foo bar$")]
        public string AddCharacterToEnds_Right(string stringAdd, char character)
        {
            return StringLibrary.AddCharacterToEnds(stringAdd, character, addLeft: false);
        }

        [TestCase("", '\'', ExpectedResult = "'")]
        [TestCase(" ", '\'', ExpectedResult = "' ")]
        [TestCase(null, '\'', ExpectedResult = "'")]
        [TestCase("Foo bar", '\'', ExpectedResult = @"'Foo bar")]
        [TestCase("Foo bar", '\"', ExpectedResult = @"""Foo bar")]
        [TestCase("Foo bar", '$', ExpectedResult = @"$Foo bar")]
        public string AddCharacterToEnds_Left(string stringAdd, char character)
        {
            return StringLibrary.AddCharacterToEnds(stringAdd, character, addRight: false);
        }

        [TestCase("", "Bar", ExpectedResult = "")]
        [TestCase(" ", "Bar", ExpectedResult = " ")]
        [TestCase(null, "Bar", ExpectedResult = "")]
        [TestCase("Bar", "Bar", ExpectedResult = "")]
        [TestCase("Foo", "Bar", ExpectedResult = "Foo")]
        [TestCase("FooBar", "Bar", ExpectedResult = "FooBar")]
        public string RemoveWord(string word,
            string wordToRemove)
        {
            return StringLibrary.RemoveWord(word, wordToRemove);
        }

        [TestCase("", "spire", ExpectedResult = "")]
        [TestCase(" ", "spire", ExpectedResult = " ")]
        [TestCase(null, "spire", ExpectedResult = "")]
        [TestCase("Nonmatching", "spire", ExpectedResult = "Nonmatching")]
        [TestCase("spire", "spire", ExpectedResult = "spire")]
        [TestCase("fooSpire", "spire", ExpectedResult = "fooSpire")]
        [TestCase("pinnacle", "spire", ExpectedResult = "spire")]
        [TestCase("fooPinnacle", "spire", ExpectedResult = "fooPinnacle")]
        [TestCase("tower", "spire", ExpectedResult = "spire")]
        [TestCase("fooTower", "spire", ExpectedResult = "fooTower")]
        [TestCase("needle", "spire", ExpectedResult = "spire")]
        [TestCase("fooNeedle", "spire", ExpectedResult = "fooNeedle")]
        public string ReplaceWithConstant(string word, string wordToUse)
        {
            List<string> wordsToReplace = new List<string>()
            {
                "pinnacle",
                "tower",
                "needle"
            };
            return StringLibrary.ReplaceWithConstant(word, wordsToReplace, wordToUse);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase("nonmatchings", ExpectedResult = "nonmatchings")]
        [TestCase("mt",  ExpectedResult = "mount")]
        [TestCase("mtn", ExpectedResult = "mountain")]
        [TestCase("mtns", ExpectedResult = "mountains")]
        [TestCase("pk", ExpectedResult = "peak")]
        [TestCase("pks", ExpectedResult = "peaks")]
        [TestCase("pt", ExpectedResult = "point")]
        [TestCase("pts", ExpectedResult = "points")]
        [TestCase("n", ExpectedResult = "north")]
        [TestCase("s", ExpectedResult = "south")]
        [TestCase("e", ExpectedResult = "east")]
        [TestCase("w", ExpectedResult = "west")]
        public string ReplaceUsingMap(string word)
        {
            Dictionary<string, string> wordMap = new Dictionary<string, string>()
            {
            { "mt", "mount"},
            { "mtn", "mountain"},
            { "mtns", "mountains"},
            { "pk", "peak"},
            { "pks", "peaks"},
            { "pt", "point"},
            { "pts", "points"},
            { "n", "north"},
            { "s", "south"},
            { "e", "east"},
            { "w", "west"},
            };

            return StringLibrary.ReplaceUsingMap(word, wordMap);
        }


        [TestCase("bar", "Bar", ExpectedResult = "bar")]
        [TestCase("Bar", "bar", ExpectedResult = "Bar")]
        [TestCase("", "Bar", ExpectedResult = "")]
        [TestCase(" ", "Bar", ExpectedResult = " ")]
        [TestCase(null, "Bar", ExpectedResult = "")]
        [TestCase("Bar", "Bar", ExpectedResult = "")]
        [TestCase("Foo", "Bar", ExpectedResult = "Foo")]
        [TestCase("FooBar", "Bar", ExpectedResult = "FooBar")]
        public string RemoveWord_Case_Sensitive(string word,
            string wordToRemove)
        {
            return StringLibrary.RemoveWord(word, wordToRemove, isCaseSensitive: true);
        }

        [TestCase("", "spire", ExpectedResult = "")]
        [TestCase(" ", "spire", ExpectedResult = " ")]
        [TestCase(null, "spire", ExpectedResult = "")]
        [TestCase("Nonmatching", "spire", ExpectedResult = "Nonmatching")]
        [TestCase("spire", "spire", ExpectedResult = "spire")]
        [TestCase("fooSpire", "spire", ExpectedResult = "fooSpire")]
        [TestCase("pinnacle", "spire", ExpectedResult = "pinnacle")]
        [TestCase("piNNacle", "spire", ExpectedResult = "spire")]
        [TestCase("fooPinnacle", "spire", ExpectedResult = "fooPinnacle")]
        [TestCase("tower", "spire", ExpectedResult = "tower")]
        [TestCase("Tower", "spire", ExpectedResult = "spire")]
        [TestCase("fooTower", "spire", ExpectedResult = "fooTower")]
        [TestCase("needle", "spire", ExpectedResult = "needle")]
        [TestCase("needlE", "spire", ExpectedResult = "spire")]
        [TestCase("fooNeedle", "spire", ExpectedResult = "fooNeedle")]
        public string ReplaceWithConstant_Case_Sensitive(string word, string wordToUse)
        {
            List<string> wordsToReplace = new List<string>()
            {
                "piNNacle",
                "Tower",
                "needlE"
            };
            return StringLibrary.ReplaceWithConstant(word, wordsToReplace, wordToUse, isCaseSensitive: true);
        }

        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = " ")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase("nonmatchings", ExpectedResult = "nonmatchings")]
        [TestCase("mt", ExpectedResult = "mt")]
        [TestCase("Mt", ExpectedResult = "mounT")]
        [TestCase("mtn", ExpectedResult = "mountain")]
        [TestCase("mtns", ExpectedResult = "mountains")]
        [TestCase("pk", ExpectedResult = "peak")]
        [TestCase("pks", ExpectedResult = "pks")]
        [TestCase("pKs", ExpectedResult = "pEAks")]
        [TestCase("pt", ExpectedResult = "point")]
        [TestCase("pts", ExpectedResult = "points")]
        [TestCase("n", ExpectedResult = "north")]
        [TestCase("s", ExpectedResult = "south")]
        [TestCase("e", ExpectedResult = "east")]
        [TestCase("w", ExpectedResult = "west")]
        public string ReplaceUsingMap_Case_Sensitive(string word)
        {
            Dictionary<string, string> wordMap = new Dictionary<string, string>()
            {
            { "Mt", "mounT"},
            { "mtn", "mountain"},
            { "mtns", "mountains"},
            { "pk", "peak"},
            { "pKs", "pEAks"},
            { "pt", "point"},
            { "pts", "points"},
            { "n", "north"},
            { "s", "south"},
            { "e", "east"},
            { "w", "west"},
            };

            return StringLibrary.ReplaceUsingMap(word, wordMap, isCaseSensitive: true);
        }
    }
}
