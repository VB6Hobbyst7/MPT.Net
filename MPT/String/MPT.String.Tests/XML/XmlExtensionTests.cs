using NUnit.Framework;
using MPT.String.XML;

namespace MPT.String.Tests.XML
{
    [TestFixture]
    public class XmlExtensionTests
    {
        [TestCase("&", ExpectedResult = "&amp;")]
        [TestCase("<", ExpectedResult = "&lt;")]
        [TestCase(">", ExpectedResult = "&gt;")]
        [TestCase("%", ExpectedResult = "&#37;")]
        [TestCase("This text has > 0% & < 100% valid characters.", ExpectedResult = "This text has &gt; 0&#37; &amp; &lt; 100&#37; valid characters.")]
        [TestCase("&amp;", ExpectedResult = "&amp;")]
        [TestCase("&lt;", ExpectedResult = "&lt;")]
        [TestCase("&gt;", ExpectedResult = "&gt;")]
        [TestCase("&#37;", ExpectedResult = "&#37;")]
        [TestCase("This text has &gt; 0&#37; &amp; &lt; 100&#37; valid characters.", ExpectedResult = "This text has &gt; 0&#37; &amp; &lt; 100&#37; valid characters.")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string ToXmlValue(string value)
        {
            return value.ToXmlValue();
        }

       
        [TestCase("&amp;", ExpectedResult = "&")]
        [TestCase("&lt;", ExpectedResult = "<")]
        [TestCase("&gt;", ExpectedResult = ">")]
        [TestCase("&#37;", ExpectedResult = "%")]
        [TestCase("This text has &gt; 0&#37; &amp; &lt; 100&#37; valid characters.", ExpectedResult = "This text has > 0% & < 100% valid characters.")]
        [TestCase("&", ExpectedResult = "&")]
        [TestCase("<", ExpectedResult = "<")]
        [TestCase(">", ExpectedResult = ">")]
        [TestCase("%", ExpectedResult = "%")]
        [TestCase("This text has > 0% & < 100% valid characters.", ExpectedResult = "This text has > 0% & < 100% valid characters.")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string FromXmlValue(string value)
        {
            return value.FromXmlValue();
        }


        [TestCase("Element", ExpectedResult = "Element")]
        [TestCase("Bad Element", ExpectedResult = "BadElement")]
        [TestCase("Not_Bad_Element", ExpectedResult = "Not_Bad_Element")]
        [TestCase("<Not_Bad_Element>", ExpectedResult = "Not_Bad_Element")]
        [TestCase("Foo&Bar", ExpectedResult = "FooBar")]
        [TestCase("Foo2Bar", ExpectedResult = "Foo2Bar")]
        [TestCase("<", ExpectedResult = "")]
        [TestCase(">", ExpectedResult = "")]
        [TestCase("></", ExpectedResult = "")]
        [TestCase("/>", ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult ="")]
        public string ToTagSafe(string value)
        {
            return value.ToTagSafe();
        }

        [TestCase("Element Attribute=\"value\"", ExpectedResult = "Element")]
        [TestCase("Element Attribute1=\"value1\"  Attribute2=\"value2\"", ExpectedResult = "Element")]
        [TestCase("Element Attribute=\"value with spaces\"", ExpectedResult = "Element")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = "")]
        public string StripAttributes(string value)
        {
            return value.StripAttributes();
        }

        //[TestCase("Element", ExpectedResult = "<Element />")]
        //[TestCase("XML Element", ExpectedResult = "<XMLElement />")]
        //[TestCase("<Element>", ExpectedResult = "<Element>")]
        //[TestCase("</Element>", ExpectedResult = "</Element>")]
        //[TestCase("<Element></Element>", ExpectedResult = "<Element></Element>")]
        //[TestCase("<Element />", ExpectedResult = "<Element />")]
        //[TestCase("<Bad Element>", ExpectedResult = "<BadElement>")]
        //[TestCase("<Element Attribute=\"value\">", ExpectedResult = "<Element Attribute=\"value\">")]
        //[TestCase("<Element Attribute=\"value\" />", ExpectedResult = "<Element />")]
        //[TestCase("<Not_Bad_Element>", ExpectedResult = "<Not_Bad_Element>")]
        //[TestCase("<", ExpectedResult = "")]
        //[TestCase(">", ExpectedResult = "")]
        //[TestCase("></", ExpectedResult = "")]
        //[TestCase("/>", ExpectedResult = "")]
        //[TestCase("", ExpectedResult = "")]
        //[TestCase(" ", ExpectedResult = "")]
        //[TestCase(null, ExpectedResult = "")]
        //public string ToXmlElement(string tableName)
        //{
        //    return tableName.ToXmlElement();
        //}

        //[TestCase("Element", ExpectedResult = "<Element>")]
        //[TestCase("XML Element", ExpectedResult = "<XMLElement>")]
        //[TestCase("<Element>", ExpectedResult = "<Element>")]
        //[TestCase("</Element>", ExpectedResult = "<Element>")]
        //[TestCase("<Element></Element>", ExpectedResult = "<Element></Element>")]
        //[TestCase("<Element />", ExpectedResult = "<Element></Element>")]
        //[TestCase("<Bad Element>", ExpectedResult = "<BadElement>")]
        //[TestCase("<Element Attribute=\"value\">", ExpectedResult = "<Element Attribute=\"value\">")]
        //[TestCase("<Not_Bad_Element>", ExpectedResult = "<Not_Bad_Element>")]
        //[TestCase("<", ExpectedResult = "")]
        //[TestCase(">", ExpectedResult = "")]
        //[TestCase("></", ExpectedResult = "")]
        //[TestCase("/>", ExpectedResult = "")]
        //[TestCase("", ExpectedResult = "")]
        //[TestCase(" ", ExpectedResult = "")]
        //[TestCase(null, ExpectedResult = "")]
        //public string ToXmlElementOpen(string tableName)
        //{
        //    return tableName.ToXmlElementOpen();
        //}


        //[TestCase("Element", ExpectedResult = "</Element>")]
        //[TestCase("XML Element", ExpectedResult = "</XMLElement>")]
        //[TestCase("<Element>", ExpectedResult = "</Element>")]
        //[TestCase("</Element>", ExpectedResult = "</Element>")]
        //[TestCase("<Element", ExpectedResult = "</Element>")]
        //[TestCase("Element>", ExpectedResult = "</Element>")]
        //[TestCase("<Element></Element>", ExpectedResult = "<Element />")]
        //[TestCase("<Element />", ExpectedResult = "<Element />")]
        //[TestCase("<Element/>", ExpectedResult = "<Element />")]
        //[TestCase("<Bad Element />", ExpectedResult = "<BadElement />")]
        //[TestCase("<Bad Element>", ExpectedResult = "</BadElement>")]
        //[TestCase("<Element Attribute=\"value\" />", ExpectedResult = "<Element />")]
        //[TestCase("<Element Attribute=\"value\">", ExpectedResult = "</Element>")]
        //[TestCase("<Not_Bad_Element>", ExpectedResult = "</Not_Bad_Element>")]
        //[TestCase("<", ExpectedResult = "")]
        //[TestCase(">", ExpectedResult = "")]
        //[TestCase("></", ExpectedResult = "")]
        //[TestCase("/>", ExpectedResult = "")]
        //[TestCase("", ExpectedResult = "")]
        //[TestCase(" ", ExpectedResult = "")]
        //[TestCase(null, ExpectedResult = "")]
        //public string ToXmlElementClose(string tableName)
        //{
        //    return tableName.ToXmlElementClose();
        //}



        //[TestCase("Element", ExpectedResult = "Element")]
        //[TestCase("XML Element", ExpectedResult = "XML Element")]
        //[TestCase("<Element>", ExpectedResult = "Element")]
        //[TestCase("</Element>", ExpectedResult = "Element")]
        //[TestCase("<Element></Element>", ExpectedResult = "Element")]
        //[TestCase("<Element />", ExpectedResult = "Element")]
        //[TestCase("<Bad Element>", ExpectedResult = "Bad Element")]
        //[TestCase("<Element Attribute=\"value\">", ExpectedResult = "Element")]
        //[TestCase("<Not_Bad_Element>", ExpectedResult = "Not_Bad_Element")]
        //[TestCase("", ExpectedResult = "")]
        //[TestCase(" ", ExpectedResult = "")]
        //[TestCase(null, ExpectedResult = "")]
        //public string FromXmlElement(string tableName)
        //{
        //    return tableName.FromXmlElement();
        //}
    }
}
