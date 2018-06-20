// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IDeletablePDeltaForces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Element can delete P-Delta force assignments.
    /// </summary>
    public interface IDeletablePDeltaForces
    {
        /// <summary>
        /// This function deletes the P-Delta force assignments for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeletePDeltaForce(string name,
            eItemType itemType = eItemType.Object);
    }
}
#endif