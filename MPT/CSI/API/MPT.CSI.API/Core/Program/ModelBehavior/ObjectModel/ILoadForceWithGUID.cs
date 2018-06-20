// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ILoadForceWithGUID.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable force load based on GUID.
    /// </summary>
    public interface ILoadForceWithGUID
    {
        /// <summary>
        /// This function retrieves the joint force load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of force load assignments returned.</param>
        /// <param name="pointNames">The name of the point object to which the specified force load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of the distributed load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="forces">The forces assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadForceWithGUID(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref string[] GUIDs,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemType itemType = eItemType.Object);


        /// <summary>
        /// This function makes point load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group.</param>
        /// <param name="loadPattern">The name of the load pattern.</param>
        /// <param name="force">The force assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="GUID">This is the global unique ID of a load force assigned to the point object or if it is not the global unique id of a load force assigned to the point object and it is not blank, the global unique ID which is assigned to the newly assigned load.
        /// If left blank, a new load is assigned to the point object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any load force assigned to the point object, all previous loads force, if any, assigned to the specified point object, in the specified load pattern, are deleted before making the new assignment.
        /// If the input GUID is the GUID of a load force already assigned to the point object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        void SetLoadForceWithGUID(string name,
            string loadPattern,
            Loads force,
            string GUID,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global);


        /// <summary>
        /// This function deletes the point load assignment with the specified global unique ID for the specified point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="GUID">The global unique ID of one of the point loads on that point object.</param>
        void DeleteLoadForceWithGUID(string name,
            string GUID);
    }
}
#endif