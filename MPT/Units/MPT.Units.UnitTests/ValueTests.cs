
using NUnit.Framework;

using MPT.Units.Core;

namespace MPT.Units.UnitTests
{
    [TestFixture]
    public class ValueTests
    {
        [TestCase("", "", ExpectedResult = "0")]
        [TestCase("", "mm", ExpectedResult = "0 mm")]
        [TestCase("1", "mm", ExpectedResult = "1 mm")]
        [TestCase("1", "kip*ft", ExpectedResult = "1 (kip*ft)")]
        [TestCase("1", "(kip*ft)", ExpectedResult = "1 kip*ft")]  //TODO: This is odd. Make parentheses more consitent.
        [TestCase("1", "N-m/sec^2", ExpectedResult = "1 (N*m)/sec^2")]
        [TestCase("1", "Kip*ft", ExpectedResult = "1 (kip*ft)")]
        public string New_Magnitude_And_Units_Creates_Magnitude_And_Unit_Value(
            string unitMagnitude, 
            string unitValue)
        {
            cValue value1 = new cValue(unitMagnitude, unitValue);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }

        [Test]
        public void New_Magnitude_And_Units_with_Only_Magnitude_Creates_Magnitude()
        {
            string expectedValue = "Yielding";
            cValue value1 = new cValue(expectedValue, "");

            Assert.AreEqual(expectedValue, value1.Magnitude);
        }

        [TestCase(0, "", ExpectedResult = "0")]
        [TestCase(2, "mm", ExpectedResult = "2 mm")]
        [TestCase(3.124, "kip*ft", ExpectedResult = "3.124 (kip*ft)")]
        [TestCase(-5, "N-m/sec^2", ExpectedResult = "-5 (N*m)/sec^2")]
        public string New_Numerical_Magnitude_And_Units_Creates_Magnitude_And_Unit_Value(
            double unitMagnitude, 
            string unitValue)
        {
            cValue value1 = new cValue(unitMagnitude, unitValue);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }
        
        [TestCase("", ExpectedResult = "0")]
        [TestCase("1", ExpectedResult = "1")]
        [TestCase("3.124", ExpectedResult = "3.124")]
        [TestCase("-5", ExpectedResult = "-5")]
        [TestCase("Yielding", ExpectedResult = "Yielding")] // TODO: Validate that units are of valid types. If not, set them blank and make value the magnitude        
        public string New_Magnitude_Or_Units_Creates_Magnitude(string magnitudeOrUnit)
        {
            cValue value1 = new cValue(magnitudeOrUnit);

            return value1.Magnitude;
        }

        [TestCase("mm", ExpectedResult = "mm")]
        [TestCase("kip*ft", ExpectedResult = "(kip*ft)")]
        [TestCase("N-m/sec^2", ExpectedResult = "(N*m)/sec^2")]
        public string New_Magnitude_Or_Units_Creates_Unit_Value(string magnitudeOrUnit)
        {
            cValue value1 = new cValue(magnitudeOrUnit);

            return value1.Units;
        }

        [TestCase(1, ExpectedResult = "1")]
        [TestCase(2, ExpectedResult = "2")]
        [TestCase(3.124, ExpectedResult = "3.124")]
        [TestCase(-5, ExpectedResult = "-5")]
        public string New_Numeric_Value_Creates_Magnitude(double unitMagnitude)
        {
            cValue value1 = new cValue(unitMagnitude);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }


        [TestCase("1", "m", "mm", ExpectedResult = "1000 mm")]
        [TestCase("1", "ft", "in", ExpectedResult = "12 in")]
        [TestCase("12", "kip*in", "kip*ft", ExpectedResult = "1 (kip*ft)")]
        [TestCase("12", "Kip*in", "kip*ft", ExpectedResult = "1 (kip*ft)")]
        [TestCase("12", "kip*in", "Kip*ft", ExpectedResult = "1 (Kip*ft)")] // TODO: normalize kip/Kip, etc.
        [TestCase("1", "m", "ft", ExpectedResult = "3.28083989501312 ft")]
        [TestCase("1", "m", "kN, mm, C", ExpectedResult = "1000 mm")]
        public string ConvertTo_Converts_Magnitude_and_Unit_Value_from_Current_Unit(
            string valueStart, 
            string unitStart, 
            string unitConvert)
        {
            cValue value1 = new cValue(valueStart, unitStart);
            value1.ConvertTo(unitConvert);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }

        [TestCase("5", "in", ExpectedResult = "5")]
        [TestCase("Yield", "kN, m, C", ExpectedResult = "Yield")]
        public string ConvertTo_Converts_Magnitude_and_Unit_Value_to_Current_Unit_with_No_Unit_Value(
            string valueStart,
            string unitConvert)
        {
            cValue value1 = new cValue(valueStart);
            value1.ConvertTo(unitConvert);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }


        [TestCase("1", "kN*mm", "5", "kN, m, C", ExpectedResult = "5000 (kN*mm)")]
        [TestCase("1", "kN", "5", "KN, m, C", ExpectedResult = "5 kN")]
        [TestCase("1", "kip*ft", "12", "kip, in", ExpectedResult = "1 (kip*ft)")]
        [TestCase("1", "Kip*ft", "12", "kip, in", ExpectedResult = "1 (Kip*ft)")]
        [TestCase("1", "(Kip*ft)", "12", "kip, in", ExpectedResult = "1 (Kip*ft)")] // TODO: This fails. Parentheses are breaking it.
        [TestCase("1", "kip*ft", "12", "Kip, in", ExpectedResult = "1 (kip*ft)")] // TODO: normalize kip/Kip, etc.
        public string ConvertFrom_Converts_Magnitude_and_Unit_Value_to_Current_Unit(
            string magnitudeStart, 
            string unitStart, 
            string magnitudeConvert, 
            string unitConvert)
        {
            cValue value1 = new cValue(magnitudeStart, unitStart);
            value1.ConvertFrom(magnitudeConvert, unitConvert);
            
            return (value1.Magnitude + " " + value1.Units).Trim();
        }

        [TestCase("Yield", "5", "kN, m, C", ExpectedResult = "Yield")]
        [TestCase("1", "5", "kN, m, C", ExpectedResult = "1")]
        public string ConvertFrom_Converts_Magnitude_and_Unit_Value_to_Current_Unit_with_No_Unit_Value(
            string magnitudeStart,
            string magnitudeConvert,
            string unitConvert)
        {
            cValue value1 = new cValue(magnitudeStart);
            value1.ConvertFrom(magnitudeConvert, unitConvert);

            return (value1.Magnitude + " " + value1.Units).Trim();
        }
    }
}
