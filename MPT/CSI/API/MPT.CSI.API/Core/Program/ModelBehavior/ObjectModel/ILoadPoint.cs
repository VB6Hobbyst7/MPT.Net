// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadPoint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable point load.
    /// </summary>
    public interface ILoadPoint
    {
        /// <summary>
        /// This function retrieves the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of point loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="forceTypes">Force type for the point load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="pointLoadValues">The load value of the point loads.
        /// [F] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="relativeDistanceFromI">The relative distance from the I-End of the element to the location of the point load.</param>
        /// <param name="coordinateSystems">Coordinated system used for each point load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadPoint(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] pointLoadValues,
            ref double[] absoluteDistanceFromI,
            ref double[] relativeDistanceFromI,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns point loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the uniform load.</param>
        /// <param name="forceType">Force type for the point load for the load pattern.</param>
        /// <param name="loadDirection">The direction that the load is applied.</param>
        /// <param name="pointLoadValue">The load value of the point loads.
        /// [F] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the uniform load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadPoint(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double pointLoadValue,
            double absoluteDistanceFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadPoint(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
