// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadTargetForce.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable target force load.
    /// </summary>
    public interface ILoadTargetForce
    {
        /// <summary>
        /// This function retrieves the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each target force.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forcesValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocations">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Forces[] forcesValues,
            ref Forces[] relativeForcesLocations,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forceValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocation">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadTargetForce(string name,
            string loadPattern,
            ForcesActive forcesActive,
            Forces forceValues,
            Forces relativeForcesLocation,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the target force load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadTargetForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif