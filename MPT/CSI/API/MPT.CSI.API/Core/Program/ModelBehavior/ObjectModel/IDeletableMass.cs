// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IDeletableMass.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a deletable mass.
    /// </summary>
    public interface IDeletableMass
    {
        /// <summary>
        /// This function deletes all mass assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteMass(string name,
            eItemType itemType = eItemType.Object);
    }
}
