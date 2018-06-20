// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-02-2017
// ***********************************************************************
// <copyright file="IChangeableMaterialOverwrite.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set material overwrites.
    /// </summary>
    public interface IChangeableMaterialOverwrite
    {
        /// <summary>
        /// This function sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="propertyName">This is None or a blank string, indicating that no material overwrite exists for the specified element, or it is the name of an existing material property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetMaterialOverwrite(string name,
            string propertyName,
            eItemType itemType = eItemType.Object);
    }
}