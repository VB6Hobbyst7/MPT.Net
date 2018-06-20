using NUnit.Framework;
using MPT.String.Number;

namespace MPT.String.Tests.Number
{
    [TestFixture]
    public class NumberExtensionTests
    {
        [TestCase("52", ExpectedResult = "52")]
        [TestCase("-52", ExpectedResult = "-52")]
        [TestCase("5.2", ExpectedResult = "5.2")]
        [TestCase(".2", ExpectedResult = ".2")]
        [TestCase("Pt. 2", ExpectedResult = "2")]
        [TestCase("Pt. 2.2", ExpectedResult = "22")]
        [TestCase("Pt 2.2", ExpectedResult = "2.2")]
        [TestCase("5.2.2", ExpectedResult = "5.22")]
        [TestCase("5,200", ExpectedResult = "5200")]
        [TestCase("52'", ExpectedResult = "52")]
        [TestCase("52ft", ExpectedResult = "52")]
        [TestCase("A52ft", ExpectedResult = "52")]
        public string GetNumbers(string value)
        {
            return value.GetNumbers();
        }
        
        [TestCase("-52", ExpectedResult = "52")]
        public string GetNumbers_No_Sign(string value)
        {
            return value.GetNumbers(keepSign: false);
        }
        
        [TestCase("5.2", ExpectedResult = "52")]
        [TestCase(".2", ExpectedResult = "2")]
        [TestCase("5.2.2", ExpectedResult = "522")]
        public string GetNumbers_No_Decimal(string value)
        {
            return value.GetNumbers(keepDecimal: false);
        }

        [TestCase("-52", ExpectedResult = -52)]
        [TestCase("-1", ExpectedResult = -1)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("27", ExpectedResult = 27)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("", -1, ExpectedResult = -1)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(" ", -1, ExpectedResult = -1)]
        [TestCase(null, ExpectedResult = 0)]
        [TestCase(null, -1, ExpectedResult = -1)]
        public int ToInt(string value, int forNot = 0)
        {
            return value.ToInt(forNot);
        }

        [TestCase("-52.2", ExpectedResult = -52.2)]
        [TestCase("-1.7", ExpectedResult = -1.7)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1.9", ExpectedResult = 1.9)]
        [TestCase("27.3", ExpectedResult = 27.3)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("", -1.2345, ExpectedResult = -1.2345)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(" ", -1.2345, ExpectedResult = -1.2345)]
        [TestCase(null, ExpectedResult = 0)]
        [TestCase(null, -1.2345, ExpectedResult = -1.2345)]
        public double ToDouble(string value, double forNot = 0.0D)
        {
            return value.ToDouble(forNot);
        }

        [TestCase("-52.2", ExpectedResult = -52.2)]
        [TestCase("-1.7", ExpectedResult = -1.7)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1.9", ExpectedResult = 1.9)]
        [TestCase("27.3", ExpectedResult = 27.3)]
        [TestCase("FooBar", ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("", -1.25, ExpectedResult = -1.25)]
        [TestCase(" ", ExpectedResult = 0)]
        [TestCase(" ", -1.25, ExpectedResult = -1.25)]
        [TestCase(null, ExpectedResult = 0)]
        [TestCase(null, 1.25, ExpectedResult = 1.25)]
        public decimal ToDecimal(string value, decimal forNot = 0.0m)
        {
            return value.ToDecimal(forNot);
        }
    }
}
