using System.Xml.Linq;
using NUnit.Framework;

namespace MPT.Xml.UnitTests
{
    [TestFixture]
    public class XmlHelperTests
    {
        [Test]
        public void ElementValueNull_When_Null_Returns_Empty()
        {
            XElement element = null;
            string result = element.ElementValueNull();
            Assert.That(string.IsNullOrEmpty(result));
        }

        [Test]
        public void ElementValueNull_When_Not_Null_Returns_Element_Value()
        {
            XElement element = new XElement("Foo", "Bar");
            string result = element.ElementValueNull();
            Assert.That(result, Is.EqualTo("Bar"));
        }

        [Test]
        public void AttributeValueNull_When_Element_Null_Returns_Empty()
        {
            XElement element = null;
            string result = element.AttributeValueNull("Moo");
            Assert.That(string.IsNullOrEmpty(result));
        }

        [Test]
        public void AttributeValueNull_When_Attribute_Null_Returns_Empty()
        {
            XElement element = new XElement("Foo", "Bar");
            string result = element.AttributeValueNull("Moo");
            Assert.That(string.IsNullOrEmpty(result));
        }

        [Test]
        public void AttributeValueNull_When_Not_Null_Returns_Element_Value()
        {
            XElement element = new XElement("Foo", new XAttribute("Moo", "Nar"), "Bar");
            string result = element.AttributeValueNull("Moo");
            Assert.That(result, Is.EqualTo("Nar"));
        }
    }
}
