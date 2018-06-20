using NUnit.Framework;
using MPT.String.Character;

namespace MPT.String.Tests.Character
{
    [TestFixture]
    public class CharacterExtensionTests
    {
        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase("Mtns", ExpectedResult = false)]
        [TestCase("Foobar", ExpectedResult = true)]
        [TestCase("Sally sells seashells down by the seashore", ExpectedResult = true)]
        public bool ContainsVowels(string word)
        {
            return word.ContainsVowels();
        }

        [TestCase("TO LOWER FIRST", ExpectedResult = "tO LOWER FIRST")]
        [TestCase("To lower first", ExpectedResult = "to lower first")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToLowerFirst(string value)
        {
            return value.ToLowerFirst();
        }

        [TestCase("to Upper First", ExpectedResult = "To Upper First")]
        [TestCase("to upper first", ExpectedResult = "To upper first")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToUpperFirst(string value)
        {
            return value.ToUpperFirst();
        }
        
        [TestCase("Foo Bar", ExpectedResult = "FooBar")]
        [TestCase(null, ExpectedResult = "")]
        public string Merge(string value)
        {
            string[] values = value?.Split(' ');
            return values.Merge();
        }

        [TestCase("Foo Bar", "+", ExpectedResult = "Foo+Bar")]
        [TestCase(null, ExpectedResult = "")]
        public string Merge(string value, string demarcator)
        {
            string[] values = value?.Split(' ');
            return values.Merge(demarcator);
        }


        [TestCase(null, null)]
        [TestCase("", null)]
        public void Split_Empty_Returns_Empty(string value, string demarcator)
        {
            string[] values = value.Split(demarcator);
            Assert.That(values.Length == 0);
        }


        [TestCase("Foo", null)]
        [TestCase("Foo", "")]
        [TestCase("Foo", "Bar")]
        [TestCase("Fo", "Foo")]
        [TestCase("Foo", "Foo")]
        public void Split_No_Matches_Returns_Original(string value, string demarcator)
        {
            string[] values = value.Split(demarcator);
            Assert.That(values.Length == 1);
            Assert.AreEqual(values[0], value);
        }


        [TestCase("FooBar", "Bar", ExpectedResult = "Foo")]
        [TestCase("FooBarBar", "Bar", ExpectedResult = "Foo")]
        [TestCase("FooBarFoo", "Bar", ExpectedResult = "FooFoo")]
        [TestCase("FooBarFooMooNarBarCar", "Bar", ExpectedResult = "FooFooMooNarCar")]
        [TestCase("Foo2Bar", "2", ExpectedResult = "FooBar")]
        [TestCase("Foo#Foo", "#", ExpectedResult = "FooFoo")]
        [TestCase("Foo#", "#", ExpectedResult = "Foo")]
        public string Split_With_Matches_Returns_Split_Without_Demarcator(string value, string demarcator)
        {
            string[] values = value.Split(demarcator);
            return values.Merge();
        }


        [TestCase("alllowertoallupper", eCapitalization.ALLCAPS, ExpectedResult = "ALLLOWERTOALLUPPER")]
        [TestCase("alllowertoalllower", eCapitalization.alllower, ExpectedResult = "alllowertoalllower")]
        [TestCase("alllowertofirstupper", eCapitalization.Firstupper, ExpectedResult = "Alllowertofirstupper")]
        [TestCase("ALLUPPERTOALLUPPER", eCapitalization.ALLCAPS, ExpectedResult = "ALLUPPERTOALLUPPER")]
        [TestCase("ALLUPPERTOALLLOWER", eCapitalization.alllower, ExpectedResult = "alluppertoalllower")]
        [TestCase("ALLUPPERTOFIRSTUPPER", eCapitalization.Firstupper, ExpectedResult = "Alluppertofirstupper")]
        [TestCase("Firstuppertoallupper", eCapitalization.ALLCAPS, ExpectedResult = "FIRSTUPPERTOALLUPPER")]
        [TestCase("Firstuppertoalllower", eCapitalization.alllower, ExpectedResult = "firstuppertoalllower")]
        [TestCase("Firstuppertofirstupper", eCapitalization.Firstupper, ExpectedResult = "Firstuppertofirstupper")]
        [TestCase("MixedCaseToAllUpper", eCapitalization.ALLCAPS, ExpectedResult = "MIXEDCASETOALLUPPER")]
        [TestCase("MixedCaseToalllower", eCapitalization.alllower, ExpectedResult = "mixedcasetoalllower")]
        [TestCase("MixedCaseToFirstUpper", eCapitalization.Firstupper, ExpectedResult = "Mixedcasetofirstupper")]
        [TestCase("MixedCaseToUnknown", 4, ExpectedResult = "MIXEDCASETOUNKNOWN")] // 4 is an enum number beyond those specified.
        [TestCase(null, eCapitalization.alllower, ExpectedResult = "")]
        public string Capitalize(string value, eCapitalization capitalization)
        {
            return value.Capitalize(capitalization);
        }
        

        [TestCase("Foo", "<", "", ExpectedResult = "<Foo")] 
        [TestCase("Foo", "", ">", ExpectedResult = "Foo>")]
        [TestCase("Foo", "<", ">", ExpectedResult = "<Foo>")]
        [TestCase("", "<", ">", ExpectedResult = "")]
        [TestCase("", "", ">", ExpectedResult = "")]
        [TestCase("", "<", "", ExpectedResult = "")]
        [TestCase(" ", "", "", ExpectedResult = "")]
        [TestCase("", "", "", ExpectedResult = "")]
        [TestCase(null, null, null, ExpectedResult = "")]
        public string JoinPrePost(string value,
           string pre = "",
           string post = "")
        {
            return value.JoinPrePost(pre, post);
        }

        [TestCase("<Foo<", "<", "", ExpectedResult = "Foo<")]  
        [TestCase(">Foo<", "<", "", ExpectedResult = ">Foo<")] 
        [TestCase(">Foo>", "", ">", ExpectedResult = ">Foo")]
        [TestCase("<Foo>", "<", ">", ExpectedResult = "Foo")]
        [TestCase("", "<", ">", ExpectedResult = "")]
        [TestCase("", "", ">", ExpectedResult = "")]
        [TestCase("", "<", "", ExpectedResult = "")]
        [TestCase(" ", "", "", ExpectedResult = "")]
        [TestCase("", "", "", ExpectedResult = "")]
        [TestCase(null, null, null, ExpectedResult = "")] 
        public string TrimPrePost(string value,
            string pre = "",
            string post = "")
        {
            return value.TrimPrePost(pre, post);
        }

        [TestCase("Mtns", ExpectedResult = "Mtns")]
        [TestCase("Foo", ExpectedResult = "Foo")]
        [TestCase("Foos", ExpectedResult = "Foo")]
        [TestCase("Bars", ExpectedResult = "Bar")]
        [TestCase("Francas", ExpectedResult = "Franca")]
        [TestCase("Pieces", ExpectedResult = "Piece")]
        [TestCase("Sandwiches", ExpectedResult = "Sandwich")] // 'ch'-'es' case
        [TestCase("Peaches", ExpectedResult = "Peach")]
        [TestCase("Aches", ExpectedResult = "Ache")]
        [TestCase("s", ExpectedResult = "s")]
        [TestCase("ss", ExpectedResult = "ss")]
        [TestCase("so", ExpectedResult = "so")]
        [TestCase("son", ExpectedResult = "son")]
        [TestCase("sons", ExpectedResult = "son")]
        [TestCase("sone", ExpectedResult = "sone")]
        [TestCase("5", ExpectedResult = "5")]
        [TestCase("5555", ExpectedResult = "5555")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToSingular(string value)
        {
            return value.ToSingular();
        }

        [TestCase("Mtns", ExpectedResult = "Mtn")]
        [TestCase("Foo", ExpectedResult = "Foo")]
        [TestCase("Foos", ExpectedResult = "Foo")]
        public string ToSingular_Not_Requiring_Vowels(string value)
        {
            return value.ToSingular(requireVowels: false);
        }

        [TestCase("Foos", ExpectedResult = "Foos")]
        [TestCase("Foo", ExpectedResult = "Foos")]
        [TestCase("Bar", ExpectedResult = "Bars")]
        [TestCase("Franca", ExpectedResult = "Francas")]
        [TestCase("Piece", ExpectedResult = "Pieces")]
        [TestCase("Sandwich", ExpectedResult = "Sandwiches")] // 'ch'-'es' case
        [TestCase("Peach", ExpectedResult = "Peaches")]
        [TestCase("Ache", ExpectedResult = "Aches")]
        [TestCase("s", ExpectedResult = "s")]
        [TestCase("ss", ExpectedResult = "ss")]
        [TestCase("so", ExpectedResult = "so")]
        [TestCase("son", ExpectedResult = "sons")]
        [TestCase("sone", ExpectedResult = "sones")]
        [TestCase("5", ExpectedResult = "5")]
        [TestCase("5555", ExpectedResult = "5555")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToPlural(string value)
        {
            return value.ToPlural();
        }

        [TestCase("FooBar", 0, ExpectedResult = "0 FooBars")]
        [TestCase("FooBar", 1, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 2, ExpectedResult = "2 FooBars")]
        [TestCase("FooBar", -1, ExpectedResult = "-1 FooBars")]
        [TestCase("5", 2, ExpectedResult = "5")]
        [TestCase("5555", 2, ExpectedResult = "5555")]
        [TestCase(" ", 2, ExpectedResult = "")]
        [TestCase("", 2, ExpectedResult = "")]
        [TestCase(null, 2, ExpectedResult = "")]
        public string ToPlural(string value, int number)
        {
            return value.ToPlural(number);
        }

        [TestCase("FooBar", 0.0, 0.001, ExpectedResult = "0 FooBars")]
        [TestCase("FooBar", 1, 0.001, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 1.001, 0.0001, ExpectedResult = "1.001 FooBars")]
        [TestCase("FooBar", 1.0001, 0.001, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 1.0001, 0.00009, ExpectedResult = "1.0001 FooBars")]
        [TestCase("FooBar", 1.0001, 0.0001, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 1.0001, 0.0002, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 1.1, 0.001, ExpectedResult = "1.1 FooBars")]
        [TestCase("FooBar", 2.1, 0.001, ExpectedResult = "2.1 FooBars")]
        [TestCase("FooBar", -1.1, 0.001, ExpectedResult = "-1.1 FooBars")]
        [TestCase("5", 2.1, 0.001, ExpectedResult = "5")]
        [TestCase("5555", 2.1, 0.001, ExpectedResult = "5555")]
        [TestCase("FooBar", 1.1, -0.001, ExpectedResult = "1.1 FooBars")]
        [TestCase(" ", 2.1, 0.001, ExpectedResult = "")]
        [TestCase("", 2.1, 0.001, ExpectedResult = "")]
        [TestCase(null, 2.1, 0.001, ExpectedResult = "")]
        public string ToPlural(string value, double number, double tolerance = 1E-3)
        {
            return value.ToPlural(number, tolerance);
        }

        [TestCase("FooBar", 0.0, ExpectedResult = "0 FooBars")]
        [TestCase("FooBar", 1, ExpectedResult = "1 FooBar")]
        [TestCase("FooBar", 1.1, ExpectedResult = "1.1 FooBars")]
        [TestCase("FooBar", 2.1, ExpectedResult = "2.1 FooBars")]
        [TestCase("FooBar", -1.1, ExpectedResult = "-1.1 FooBars")]
        [TestCase("5", 2.1, ExpectedResult = "5")]
        [TestCase(" ", 2.1, ExpectedResult = "")]
        [TestCase("", 2.1, ExpectedResult = "")]
        [TestCase(null, 2.1, ExpectedResult = "")]
        public string ToPlural(string value, decimal number)
        {
            return value.ToPlural(number);
        }

        [TestCase("Foo", ExpectedResult = "Foo's")]     
        [TestCase("Foos", ExpectedResult = "Foos'")]    // Check ending in s for s' case
        [TestCase("It", ExpectedResult = "Its")]        // Check for It for 'its' case
        [TestCase("s", ExpectedResult = "s")]
        [TestCase("ss", ExpectedResult = "ss")]
        [TestCase("so", ExpectedResult = "so")]
        [TestCase("cows", ExpectedResult = "cows'")]
        [TestCase("son", ExpectedResult = "son's")]
        [TestCase("sone", ExpectedResult = "sone's")]
        [TestCase("5", ExpectedResult = "5")]
        [TestCase("5555", ExpectedResult = "5555")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToPossesive(string value)
        {
            return value.ToPossessive();
        }

        [TestCase("FooBar", ExpectedResult = "FooBar")]
        [TestCase("Foo", ExpectedResult = "Foo")]
        [TestCase("Foo's", ExpectedResult = "Foo")]
        [TestCase("Foos'", ExpectedResult = "Foo")]
        [TestCase("Its", ExpectedResult = "It")]
        [TestCase("5", ExpectedResult = "5")]
        [TestCase("5555", ExpectedResult = "5555")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FromPossessive(string value)
        {
            return value.FromPossessive();
        }
    }
}
