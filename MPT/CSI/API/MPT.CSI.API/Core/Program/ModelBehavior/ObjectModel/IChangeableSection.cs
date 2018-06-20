// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="IChangeableSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set the section/property assignment.
    /// </summary>
    public interface IChangeableSection
    {
        /// <summary>
        /// This function sets the section property assigned to an object.
        /// </summary>
        /// <param name="name">The name of a defined object.</param>
        /// <param name="propertyName">The name of the section property assigned to the object.
        /// This item is None if there is no section property assigned to the object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object);
    }
}
