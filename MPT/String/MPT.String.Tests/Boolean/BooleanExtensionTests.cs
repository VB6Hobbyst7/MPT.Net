using NUnit.Framework;
using MPT.String.Boolean;
using MPT.String.Character;

namespace MPT.String.Tests.Boolean
{
    [TestFixture]
    public class BooleanExtensionTests
    {
        [TestCase("true", ExpectedResult = true)]
        [TestCase("false", ExpectedResult = false)]
        [TestCase("unknown", ExpectedResult = null)]
        [TestCase("TRUE", ExpectedResult = true)]
        [TestCase("FALSE", ExpectedResult = false)]
        [TestCase("UNKNOWN", ExpectedResult = null)]
        [TestCase("True", ExpectedResult = true)]
        [TestCase("False", ExpectedResult = false)]
        [TestCase("Unknown", ExpectedResult = null)]
        [TestCase("FooBar", ExpectedResult = null)]
        [TestCase("", ExpectedResult = null)]
        [TestCase(" ", ExpectedResult = null)]
        [TestCase(null, ExpectedResult = null)]
        public bool? BoolerizeNullable(string value)
        {
            return value.BoolerizeNullable();
        }

        [TestCase(true, eCapitalization.alllower, ExpectedResult = "true")]
        [TestCase(false, eCapitalization.alllower, ExpectedResult = "false")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "TRUE")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "FALSE")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "True")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "False")]
        [TestCase(null, eCapitalization.Firstupper, ExpectedResult = "Null")]
        [TestCase(null, eCapitalization.Firstupper, "", ExpectedResult = "")]
        [TestCase(null, eCapitalization.Firstupper, "Unknown", ExpectedResult = "Unknown")]
        [TestCase(null, eCapitalization.Firstupper, "QienSabe", ExpectedResult = "Qiensabe")]
        public string StringifyNullable(bool? value, eCapitalization capitalization, string forNull = "Unknown")
        {
            return value.Stringify(forNull: forNull, pattern: capitalization);
        }




        [TestCase("true", ExpectedResult = true)]
        [TestCase("false", ExpectedResult = false)]
        [TestCase("unknown", ExpectedResult = false)]
        [TestCase("TRUE", ExpectedResult = true)]
        [TestCase("FALSE", ExpectedResult = false)]
        [TestCase("UNKNOWN", ExpectedResult = false)]
        [TestCase("True", ExpectedResult = true)]
        [TestCase("False", ExpectedResult = false)]
        [TestCase("Unknown", ExpectedResult = false)]
        [TestCase("FooBar", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool? Boolerize(string value)
        {
            return value.Boolerize();
        }

        [TestCase(true, eCapitalization.alllower, ExpectedResult = "true")]
        [TestCase(false, eCapitalization.alllower, ExpectedResult = "false")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "TRUE")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "FALSE")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "True")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "False")]
        public string Stringify(bool value, eCapitalization capitalization)
        {
            return value.Stringify(capitalization);
        }




        [TestCase("yes", ExpectedResult = true)]
        [TestCase("no", ExpectedResult = false)]
        [TestCase("YES", ExpectedResult = true)]
        [TestCase("NO", ExpectedResult = false)]
        [TestCase("Yes", ExpectedResult = true)]
        [TestCase("No", ExpectedResult = false)]
        [TestCase("FooBar", ExpectedResult = null)]
        [TestCase("", ExpectedResult = null)]
        [TestCase(" ", ExpectedResult = null)]
        [TestCase(null, ExpectedResult = null)]
        public bool? BoolerizeYesNoNullable(string value)
        {
            return value.BoolerizeYesNoNullable();
        }

        [TestCase(true, eCapitalization.alllower, ExpectedResult = "yes")]
        [TestCase(false, eCapitalization.alllower, ExpectedResult = "no")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "YES")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "NO")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "Yes")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "No")]
        [TestCase(null, eCapitalization.Firstupper, ExpectedResult = "Null")]
        [TestCase(null, eCapitalization.Firstupper, "", ExpectedResult = "")]
        [TestCase(null, eCapitalization.Firstupper, "Unknown", ExpectedResult = "Unknown")]
        [TestCase(null, eCapitalization.Firstupper, "QienSabe", ExpectedResult = "Qiensabe")]
        public string StringifyYesNoNullable(bool? value, eCapitalization capitalization, string forNull = "")
        {
            return value.StringifyYesNo(forNull: forNull, pattern: capitalization);
        }


        [TestCase("yes", ExpectedResult = true)]
        [TestCase("no", ExpectedResult = false)]
        [TestCase("YES", ExpectedResult = true)]
        [TestCase("NO", ExpectedResult = false)]
        [TestCase("Yes", ExpectedResult = true)]
        [TestCase("No", ExpectedResult = false)]
        [TestCase("FooBar", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool BoolerizeYesNo(string value)
        {
            return value.BoolerizeYesNo();
        }

        [TestCase(true, eCapitalization.alllower, ExpectedResult = "yes")]
        [TestCase(false, eCapitalization.alllower, ExpectedResult = "no")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "YES")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "NO")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "Yes")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "No")]
        public string StringifyYesNo(bool value, eCapitalization capitalization)
        {
            return value.StringifyYesNo(capitalization);
        }
    }
}
