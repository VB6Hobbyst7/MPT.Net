// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadPointWithGUID.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable point load with GUID.
    /// </summary>
    public interface ILoadPointWithGUID
    {
        /// <summary>
        /// This function retrieves the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of point loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of each distributed load.</param>
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
        void GetLoadPointWithGUID(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] GUIDs,
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
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the uniform load.</param>
        /// <param name="GUID">This is the global unique ID of a distributed load assigned to the frame object or if it is not the global unique id of a distributed load assigned to the frame object and it is not blank, the global unique ID which is assigned to the newly assigned load.
        /// If left blank, a new load is assigned to the frame object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="forceType">Force type for the point load for the load pattern.</param>
        /// <param name="loadDirection">The direction that the load is applied.</param>
        /// <param name="pointLoadValue">The load value of the point loads.
        /// [F] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the uniform load.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any distributed load assigned to the frame object, all previous distributed loads, if any, assigned to the specified frame object, in the specified load pattern, are deleted before making the new assignment.
        /// If the input GUID is the GUID of a distributed load already assigned to the frame object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        void SetLoadPointWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double pointLoadValue,
            double absoluteDistanceFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true);

        /// <summary>
        /// This function deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The global unique ID of one of the distributed loads on that frame object.</param>
        void DeleteLoadPointWithGUID(string name,
            string GUID);
    }
}
#endif