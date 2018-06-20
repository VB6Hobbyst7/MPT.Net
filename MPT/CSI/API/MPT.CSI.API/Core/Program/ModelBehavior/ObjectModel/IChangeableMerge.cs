// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeableMerge.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set the point merge assignment.
    /// </summary>
    public interface IChangeableMerge
    {
        /// <summary>
        /// This function assigns the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="mergeNumber">The merge number assigned to the specified point object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetMergeNumber(string name,
            int mergeNumber,
            eItemType itemType = eItemType.Object);
    }
}
#endif