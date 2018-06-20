// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadDistributedConstant.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable constant distributed load.
    /// </summary>
    public interface ILoadDistributedConstant
    {
        /// <summary>
        /// This function retrieves the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of distributed loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="loadValues">The load values across the cable for each load pattern.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] loadValues,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the distributed load.</param>
        /// <param name="forceType">Force type for the distributed load for the load pattern.</param>
        /// <param name="loadDirection">Direction that the load is applied in for the load pattern.</param>
        /// <param name="loadValue">The load value across the cable.
        /// [F/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" /> and [F*L/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="coordinateSystem">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="replace">True: All previous distributed loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadDistributed(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double loadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadDistributed(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif