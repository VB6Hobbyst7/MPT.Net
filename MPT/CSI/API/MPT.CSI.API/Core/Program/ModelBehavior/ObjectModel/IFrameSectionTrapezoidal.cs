// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IFrameSectionTrapezoidal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can have a gettable/settable trapezoidal frame cross-section.
    /// </summary>
    public interface IFrameSectionTrapezoidal
    {
        /// <summary>
        /// This function retrieves frame section property data for a trapezoidal frame section.
        /// </summary>
        /// <param name="name">The name of an existing trapezoidal frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="materialName">The name of the material property for the section.</param>
        /// <param name="sectionDepth">The section depth. [L]</param>
        /// <param name="sectionTopWidth">The section top width. [L]</param>
        /// <param name="sectionBottomWidth">The section bottom width. [L]</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetTrapezoidal(string name,
            ref string fileName,
            ref string materialName,
            ref double sectionDepth,
            ref double sectionTopWidth,
            ref double sectionBottomWidth,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// This function initializes a solid trapezoidal frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="materialName">The name of the material property for the section.</param>
        /// <param name="sectionDepth">The section depth. [L]</param>
        /// <param name="sectionTopWidth">The section top width. [L]</param>
        /// <param name="sectionBottomWidth">The section bottom width. [L]</param>
        /// <param name="color">The display color assigned to the section.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetTrapezoidal(string name,
            string materialName,
            double sectionDepth,
            double sectionTopWidth,
            double sectionBottomWidth,
            int color = -1,
            string notes = "",
            string GUID = "");
    }
}
#endif