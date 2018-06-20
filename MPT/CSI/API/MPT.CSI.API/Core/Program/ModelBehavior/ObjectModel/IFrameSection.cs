// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IFrameSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable/settable frame section properties.
    /// </summary>
    public interface IFrameSection
    {
        /// <summary>
        /// This function retrieves the section property assigned to a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        /// <param name="autoSelectList">Name of the auto select list assigned to the frame object, if any.
        /// If this item is returned as a blank string, no auto select list is assigned to the frame object.</param>
        void GetSection(string name,
            out string propertyName,
            out string autoSelectList);

        /// <summary>
        /// This function retrieves the nonprismatic frame section property data assigned to a frame object.
        /// TODO: Handle: The function returns an error if the section property assigned to the frame object is not a nonprismatic property.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the nonprismatic frame section property assigned to the frame object.</param>
        /// <param name="totalLength">This is the total assumed length of the nonprismatic section.
        /// Enter 0 for this item to indicate that the section length is the same as the frame object length.</param>
        /// <param name="relativeStartLocation">This is the relative distance along the nonprismatic section to the I-End (start) of the frame object.
        /// This item is ignored when the sVarTotalLengthitem is 0.</param>
        void GetSectionNonPrismatic(string name,
            ref string propertyName,
            ref double totalLength,
            ref double relativeStartLocation);

        /// <summary>
        /// This function assigns the section property to a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        /// <param name="nonPrismaticTotalLength">Total assumed length of the nonprismatic section.
        /// Enter 0 for this item to indicate that the section length is the same as the frame object length.
        /// This item is applicable only when the assigned frame section property is a nonprismatic section.</param>
        /// <param name="nonPrismaticRelativeStartLocation">Relative distance along the nonprismatic section to the I-End (start) of the frame object.
        /// This item is ignored when <paramref name="nonPrismaticTotalLength" /> is 0.
        /// This item is applicable only when the assigned frame section property is a nonprismatic section, and the <paramref name="nonPrismaticTotalLength" /> &gt; 0.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the frame object specified by the Name item.
        /// If this item is Group, the assignment is made to all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected frame objects, and the Name item is ignored.</param>
        void SetSection(string name,
            string propertyName,
            double nonPrismaticTotalLength,
            double nonPrismaticRelativeStartLocation,
            eItemType itemType = eItemType.Object);
    }
}