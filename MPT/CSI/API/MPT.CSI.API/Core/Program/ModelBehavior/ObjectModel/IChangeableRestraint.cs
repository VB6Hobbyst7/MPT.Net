// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IChangeableRestraint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set the point restraint assignment.
    /// </summary>
    public interface IChangeableRestraint
    {
        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="degreesOfFreedom">These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetRestraint(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            eItemType itemType = eItemType.Object);
    }
}