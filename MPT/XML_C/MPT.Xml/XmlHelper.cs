// ***********************************************************************
// Assembly         : MPT.Xml
// Author           : Mark Thomas
// Created          : 12-18-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-18-2017
// ***********************************************************************
// <copyright file="XmlHelper.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Xml.Linq;

namespace MPT.Xml
{
    /// <summary>
    /// Contains helper methods for reading XML.
    /// </summary>
    public static class XmlHelper
    {
        
        /// <summary>
        /// Gets the element value, or returns empty if null.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.String.</returns>
        public static string ElementValueNull(this XElement element)
        {
            return element?.Value ?? string.Empty;
        }
        
        /// <summary>
        /// Gets the attribute value, or returns empty if null.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>System.String.</returns>
        public static string AttributeValueNull(this XElement element, string attributeName)
        {
            if (element == null)
                return string.Empty;

            XAttribute attribute = element.Attribute(attributeName);
            return attribute?.Value ?? string.Empty;
        }
    }
}
