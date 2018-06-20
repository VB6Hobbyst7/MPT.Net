using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Lists.UnitTests
{
    [TestFixture]
    public class ListLibraryTests_Parsing
    {
        [TestCase("The.Happy.Brown.Fox", ".", ExpectedResult = "The Happy Brown Fox")]
        [TestCase("Foo + Bar = FooBar + BarFoo", "+", ExpectedResult = "Foo Bar = FooBar BarFoo")]
        [TestCase("The.Happy.Brown.Fox", "", ExpectedResult = "The.Happy.Brown.Fox")]
        [TestCase("The.Happy.Brown.Fox", " ", ExpectedResult = "The.Happy.Brown.Fox")]
        [TestCase("", "", ExpectedResult = "")]
        [TestCase(" ", "", ExpectedResult = "")]
        [TestCase(null, "", ExpectedResult = "")]
        [TestCase("", null, ExpectedResult = "")]
        [TestCase("", " ", ExpectedResult = "")]
        public string ParseStringToList_Parses_String_To_List_By_Demarcator(string value, string demaractor)
        {
            List<string> values = ListLibrary.ParseStringToList(value, demaractor);

            string result = "";
            foreach (string splitValue in values)
            {
                result += splitValue + " ";
            }
            return result.Trim();
        }

        
        [TestCase("4-11", ExpectedResult = "4, 5, 6, 7, 8, 9, 10, 11,")]
        [TestCase("1.3-11.9", ExpectedResult = "")]
        [TestCase("A-G", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        public string ParseRangeToListOfInteger(string rangeString)
        {
            IEnumerable<int> listFromRange = ListLibrary.ParseRangeToListOfInteger(rangeString);

            string output = "";
            foreach (int item in listFromRange)
            {
                output += item + ", ";
            }
            output = output.Trim();
            return output;
        }

        [Test]
        public void ParseRangeToListOfInteger_With_Negative_Numbers_Throws_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                ListLibrary.ParseRangeToListOfInteger("-4-11");
            });
            Assert.That(ex.Message, Is.EqualTo("Negative values are not supported."));
        }



        [TestCase("4-11", ExpectedResult = "4, 5, 6, 7, 8, 9, 10, 11,")]
        [TestCase("1.3-11.9", ExpectedResult = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,")]
        [TestCase("A-G", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        public string ParseRangeToListOfDouble(string rangeString)
        {
            IEnumerable<double> listFromRange = ListLibrary.ParseRangeToListOfDouble(rangeString);

            string output = "";
            foreach (double item in listFromRange)
            {
                output += item + ", ";
            }
            output = output.Trim();
            return output;
        }
        
        [Test]
        public void ParseRangeToListOfDouble_With_Negative_Numbers_Throws_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                        {
                            ListLibrary.ParseRangeToListOfDouble("-4-11");
                        });
            Assert.That(ex.Message, Is.EqualTo("Negative values are not supported."));
        }

        [Test]
        public void ParseListToString_With_Null_List()
        {
            string stringOfLists = ListLibrary.ParseListToString(null);
            Assert.IsEmpty(stringOfLists);
        }

        [Test]
        public void ParseListToString_With_Empty_List()
        {
            List<string> numericStringList = new List<string>();
            string stringOfLists = ListLibrary.ParseListToString(numericStringList);
            Assert.IsEmpty(stringOfLists);
        }

        [Test]
        public void ParseListToString()
        {
            List<string> numericStringList = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            string stringOfLists = ListLibrary.ParseListToString(numericStringList);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8, 9, 10", stringOfLists);
        }

        [TestCase("1-4", ExpectedResult = "1, 2, 3, 4")]
        [TestCase("-1-4", ExpectedResult = "")]
        [TestCase("1; 2; 3; 4", ExpectedResult = "1, 2, 3, 4")]
        [TestCase("1;2;3;4", ExpectedResult = "1, 2, 3, 4")]
        [TestCase("1 2 3 4", ExpectedResult = "1, 2, 3, 4")]
        [TestCase("1,2,3,4", ExpectedResult = "1, 2, 3, 4")]
        [TestCase("A-Z", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ParseNumericStringToList(string stringOfLists)
        {
            IList<string> numericStringList = ListLibrary.ParseNumericStringToList(stringOfLists);

            string result = "";
            foreach (string numericString in numericStringList)
            {
                result += numericString + ", ";
            }
            if (result.Length > 1)
            {
                result = result.Trim();
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        [TestCase("1-4", ExpectedResult = "4, 3, 2, 1")]
        [TestCase("-1-4", ExpectedResult = "")]
        [TestCase("1; 2; 3; 4", ExpectedResult = "4, 3, 2, 1")]
        [TestCase("1;2;3;4", ExpectedResult = "4, 3, 2, 1")]
        [TestCase("1 2 3 4", ExpectedResult = "4, 3, 2, 1")]
        [TestCase("1,2,3,4", ExpectedResult = "4, 3, 2, 1")]
        [TestCase("A-Z", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ParseNumericStringToList_Sort_Descending(string stringOfLists)
        {
            IList<string> numericStringList = ListLibrary.ParseNumericStringToList(stringOfLists, sortAscending: false);

            string result = "";
            foreach (string numericString in numericStringList)
            {
                result += numericString + ", ";
            }
            if (result.Length > 1)
            {
                result = result.Trim();
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
