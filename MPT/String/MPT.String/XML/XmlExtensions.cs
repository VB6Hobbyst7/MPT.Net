using System.Text;
using System.Text.RegularExpressions;
using MPT.String.Character;

namespace MPT.String.XML
{
    /// <summary>
    /// Extension methods to strings and classes relating to strings and XML files.
    /// </summary>
    public static class XmlExtensions
    {   
        /// <summary>
        /// Parses text to automatically substitute reserved XML characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string ToXmlValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Normalize any existing XML escapes
            value = value.FromXmlValue();

            // Now replace the values
            value = value.Replace("&", "&amp;"); // Must come first as it is both a reserved character and part of the escapes
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");
            value = value.Replace("%", "&#37;");
            
            return value;    
        }

        /// <summary>
        /// Parses text to automatically undo substitutes for reserved XML characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string FromXmlValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            // Normalize any existing XML escapes
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("&#37;", "%");
            value = value.Replace("&amp;", "&"); // Must come last as it is both a reserved character and part of the escapes

            return value;
        }


        /// <summary>
        /// Adjusts the value to be in an appropriate form for tag names.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="underscoresForSpaces">True: Underscores will be inserted where any spaces exist. 
        /// Otherwise, separate words will be merged.</param>
        /// <returns>string.</returns>
        public static string ToTagSafe(this string value, 
            bool underscoresForSpaces = false)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            // Remove attributes
            value = StripAttributes(value);

            // Remove spaces
            string joiner = "";
            if (underscoresForSpaces) joiner = "_";
            value = Regex.Replace(value, @"\s", joiner);

            // Remove all characters that are not a letter or a number or an underscore
            value = Regex.Replace(value, @"[^a-zA-Z0-9 _]", "");      // See: https://stackoverflow.com/questions/1318279/c-sharp-regex-to-match-a-string-that-doesnt-contain-a-certain-string

            return value;
        }

        /// <summary>
        /// Strips the attributes from XML element names, but preserves spaces.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string.</returns>
        public static string StripAttributes(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            
            StringBuilder stringBuilder = new StringBuilder();
            string[] values = value.Split(' ');
            foreach (string word in values)
            {
                if (word.Contains("=")) break; // Stop rording at the presence of the frst attribute
                stringBuilder.Append(word + ' ');
            }
            return stringBuilder.ToString().Trim();
        }

        // TODO: Consider only keeping methods above
        ///// <summary>
        ///// To the XML element in the form of &lt;{noSpaces} /&gt; .
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>string.</returns>
        //public static string ToXmlElement(this string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        //    // If Element
        //    value = value.Trim();
        //    if (value[0] == '<' && value[value.Length-1] == '>')
        //    {
        //        // If close element, return as close
        //        if (value[value.Length - 2] == '/') return value.ToXmlElementClose();
        //        //if (value.Contains("></")) // TODO: Get element name and create brackets.
        //        return value.ToXmlElementOpen();
        //    }
        //    value = value.ToTagSafe();

        //    if (value.Length == 0) return string.Empty;
        //    return '<' + value + '>' + "</" + value + '>';
        //}

        ///// <summary>
        ///// To the XML element open tag in the form of &lt;{noSpaces}&gt;.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>string.</returns>
        //public static string ToXmlElementOpen(this string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        //    string element = value.ToTagSafe();

        //    if (element.Length == 0) return string.Empty;

        //    string attributes = GetAttributes(value);
        //    if (attributes.Length > 0) element += ' ' + attributes;

        //    return '<' + element + '>';
        //}

        ///// <summary>
        ///// To the XML element close tag in the form of &lt;/{noSpaces}&gt;.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>string.</returns>
        //public static string ToXmlElementClose(this string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        //    // Check special cases: For empty elements, just get the first word
        //    string[] values = value.Split("></");   // <Element></Element>
        //    if (values.Length == 0) return string.Empty;
        //    value = values[0];
        //    bool isEmpty = (values.Length > 1 || value.Contains("/>")); // <Element></Element> or <Element />

        //    value = value.ToTagSafe();

        //    if (value.Length == 0) return string.Empty;
        //    if (isEmpty)
        //    {
        //        return "<" + value + " />";
        //    }
        //    return "</" + value + '>';
        //}


        ///// <summary>
        ///// Deconstructs the XML element tag into the element name.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>string.</returns>
        //public static string FromXmlElement(this string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        //    // Split on closing bracket to handle cases of empty elements (grab first one only).
        //    string[] values = value.Split('>');

        //    // Split words on spaces and recombine only those w/o '=' to filter out attributes.
        //    string newValue = StripAttributes(values[0]);

        //    newValue = newValue.TrimPrePost("</", ">");
        //    newValue = newValue.TrimPrePost("<", ">");
        //    newValue = newValue.TrimPrePost("<", " />");
        //    newValue = newValue.TrimPrePost("<", " /");

        //    return newValue;
        //}



        //private static string GetAttributes(string value)
        //{
        //    // TODO: Not implemented yet. Expected to fail.
        //    StringBuilder stringBuilder = new StringBuilder();
        //    string[] values = value.Split(' ');
        //    foreach (string word in values)
        //    {
        //        if (!word.Contains("="))
        //        {
        //            stringBuilder.Append(word + ' ');
        //        }
        //    }
        //    return stringBuilder.ToString().Trim();
        //}
    }
}
