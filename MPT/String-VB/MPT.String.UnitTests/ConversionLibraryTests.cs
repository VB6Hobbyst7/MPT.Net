using System;

using NUnit.Framework;

using MPT.Enums;

namespace MPT.String.UnitTests
{
    [TestFixture]
    public class ConversionLibraryTests
    {

        [TestCase("alllowertoallupper", eCapitalization.ALLCAPS, ExpectedResult = "ALLLOWERTOALLUPPER")]
        [TestCase("alllowertoalllower", eCapitalization.AllLower, ExpectedResult = "alllowertoalllower")]
        [TestCase("alllowertofirstupper", eCapitalization.Firstupper, ExpectedResult = "Alllowertofirstupper")]
        [TestCase("ALLUPPERTOALLUPPER", eCapitalization.ALLCAPS, ExpectedResult = "ALLUPPERTOALLUPPER")]
        [TestCase("ALLUPPERTOALLLOWER", eCapitalization.AllLower, ExpectedResult = "alluppertoalllower")]
        [TestCase("ALLUPPERTOFIRSTUPPER", eCapitalization.Firstupper, ExpectedResult = "Alluppertofirstupper")]
        [TestCase("Firstuppertoallupper", eCapitalization.ALLCAPS, ExpectedResult = "FIRSTUPPERTOALLUPPER")]
        [TestCase("Firstuppertoalllower", eCapitalization.AllLower, ExpectedResult = "firstuppertoalllower")]
        [TestCase("Firstuppertofirstupper", eCapitalization.Firstupper, ExpectedResult = "Firstuppertofirstupper")]
        [TestCase("MixedCaseToAllUpper", eCapitalization.ALLCAPS, ExpectedResult = "MIXEDCASETOALLUPPER")]
        [TestCase("MixedCaseToAllLower", eCapitalization.AllLower, ExpectedResult = "mixedcasetoalllower")]
        [TestCase("MixedCaseToFirstUpper", eCapitalization.Firstupper, ExpectedResult = "Mixedcasetofirstupper")]
        [TestCase("MixedCaseToUnknown", 4, ExpectedResult = "MIXEDCASETOUNKNOWN")] // 4 is an enum number beyond those specified.
        [TestCase(null, eCapitalization.AllLower, ExpectedResult = "")]
        public string CapitalizationSingleWord_Returns_Capitalized_String(string value, eCapitalization capitalization)
        {
            return ConversionLibrary.CapitalizationSingleWord(value, capitalization);
        }

        [TestCase("yes", ExpectedResult = eYesNoUnknown.yes)]
        [TestCase("no", ExpectedResult = eYesNoUnknown.no)]
        [TestCase("YES", ExpectedResult = eYesNoUnknown.yes)]
        [TestCase("NO", ExpectedResult = eYesNoUnknown.no)]
        [TestCase("Yes", ExpectedResult = eYesNoUnknown.yes)]
        [TestCase("No", ExpectedResult = eYesNoUnknown.no)]
        [TestCase("FooBar", ExpectedResult = eYesNoUnknown.unknown)]
        [TestCase("", ExpectedResult = eYesNoUnknown.unknown)]
        [TestCase(" ", ExpectedResult = eYesNoUnknown.unknown)]
        [TestCase(null, ExpectedResult = eYesNoUnknown.unknown)]
        public eYesNoUnknown ConvertYesNoUnknownEnum(string value)
        {
            return ConversionLibrary.ConvertYesNoUnknownEnum(value);
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
        public bool ConvertYesTrueBoolean(string value)
        {
            return ConversionLibrary.ConvertYesTrueBoolean(value);
        }

        [TestCase(true, eCapitalization.AllLower, ExpectedResult = "yes")]
        [TestCase(false, eCapitalization.AllLower, ExpectedResult = "no")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "YES")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "NO")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "Yes")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "No")]
        public string ConvertYesTrueString(bool value, eCapitalization capitalization)
        {
            return ConversionLibrary.ConvertYesTrueString(value, capitalization);
        }


        [TestCase("true", ExpectedResult = true)]
        [TestCase("false", ExpectedResult = false)]
        [TestCase("TRUE", ExpectedResult = true)]
        [TestCase("FALSE", ExpectedResult = false)]
        [TestCase("True", ExpectedResult = true)]
        [TestCase("False", ExpectedResult = false)]
        [TestCase("other", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ConvertTrueTrueBoolean(string value)
        {
            return ConversionLibrary.ConvertTrueTrueBoolean(value);
        }


        [TestCase(true, eCapitalization.AllLower, ExpectedResult = "true")]
        [TestCase(false, eCapitalization.AllLower, ExpectedResult = "false")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "TRUE")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "FALSE")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "True")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "False")]
        public string ConvertTrueTrueString(bool value, eCapitalization capitalization)
        {
            return ConversionLibrary.ConvertTrueTrueString(value, capitalization);
        }


        [TestCase(true, eCapitalization.AllLower, ExpectedResult = "yes")]
        [TestCase(false, eCapitalization.AllLower, ExpectedResult = "no")]
        [TestCase(true, eCapitalization.ALLCAPS, ExpectedResult = "YES")]
        [TestCase(false, eCapitalization.ALLCAPS, ExpectedResult = "NO")]
        [TestCase(true, eCapitalization.Firstupper, ExpectedResult = "Yes")]
        [TestCase(false, eCapitalization.Firstupper, ExpectedResult = "No")]
        public string ConvertTrueYesNoUnknownString(bool value, eCapitalization capitalization)
        {
            return ConversionLibrary.ConvertTrueYesNoUnknownString(value, capitalization);
        }

        [TestCase(true, ExpectedResult = eYesNoUnknown.yes)]
        [TestCase(false, ExpectedResult = eYesNoUnknown.no)]
        public eYesNoUnknown ConvertTrueYesNoUnknownEnum(bool value)
        {
            return ConversionLibrary.ConvertTrueYesNoUnknownEnum(value);
        }

        [TestCase("yes", ExpectedResult = true)]
        [TestCase("no", ExpectedResult = false)]
        [TestCase("unknown", ExpectedResult = false)]
        [TestCase("YES", ExpectedResult = true)]
        [TestCase("NO", ExpectedResult = false)]
        [TestCase("UNKNOWN", ExpectedResult = false)]
        [TestCase("Yes", ExpectedResult = true)]
        [TestCase("No", ExpectedResult = false)]
        [TestCase("Unknown", ExpectedResult = false)]
        [TestCase("FooBar", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool ConvertYesNoUnknownStringBoolean(string value)
        {
            return ConversionLibrary.ConvertYesNoUnknownStringBoolean(value);
        }

        [TestCase(eYesNoUnknown.yes, ExpectedResult = true)]
        [TestCase(eYesNoUnknown.no, ExpectedResult = false)]
        public bool ConvertYesNoUnknownBoolean(eYesNoUnknown value)
        {
            return ConversionLibrary.ConvertYesNoUnknownBoolean(value);
        }

        [TestCase("-52", ExpectedResult =-52)]
        [TestCase("-1", ExpectedResult = -1)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("27", ExpectedResult = 27)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(null, ExpectedResult = 0)]
        public int myCInt(string value)
        {
            return ConversionLibrary.myCInt(value);
        }

        [TestCase("-52.2", ExpectedResult = -52.2)]
        [TestCase("-1.7", ExpectedResult = -1.7)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1.9", ExpectedResult = 1.9)]
        [TestCase("27.3", ExpectedResult = 27.3)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(null, ExpectedResult = 0)]
        public double myCDbl(string value)
        {
            return ConversionLibrary.myCDbl(value);
        }

        [TestCase("-52.2", ExpectedResult = -52.2)]
        [TestCase("-1.7", ExpectedResult = -1.7)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1.9", ExpectedResult = 1.9)]
        [TestCase("27.3", ExpectedResult = 27.3)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(null, ExpectedResult = 0)]
        public decimal myCDec(string value)
        {
            return ConversionLibrary.myCDec(value);
        }


        [TestCase("Table Name", true, ExpectedResult = "Table Name")]
        [TestCase("Table Name", false, ExpectedResult = "[Table Name]")]
        [TestCase("[Table Name]", true, ExpectedResult = "Table Name")]
        [TestCase("[Table Name]", false, ExpectedResult = "[Table Name]")]
        [TestCase("", true, ExpectedResult = "")]
        [TestCase("", false, ExpectedResult = "")]
        [TestCase(" ", true, ExpectedResult = "")]
        [TestCase(" ", false, ExpectedResult = "")]
        [TestCase(null, true, ExpectedResult = "")]
        [TestCase(null, false, ExpectedResult = "")]
        public string ParseTableName(string tableName, bool removeBrackets)
        {
            return ConversionLibrary.ParseTableName(tableName, removeBrackets);
        }
    }
}
