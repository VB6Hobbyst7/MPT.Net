// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="ILoadForce.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable force load.
    /// </summary>
    public interface ILoadForce
    {

        /// <summary>
        /// This function returns a list of force load assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of force load assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified force load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="forces">The forces assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadForce(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemType itemType = eItemType.Object);


        /// <summary>
        /// This function makes point load assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of the load pattern.</param>
        /// <param name="force">The force assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="replace">True: All previous point loads, if any, assigned to the specified point object(s) in the specified load pattern are deleted before making the new assignment.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadForce(string name,
            string loadPattern,
            Loads force,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object);


        /// <summary>
        /// This function deletes all point load assignments, for the specified load pattern, from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object, or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are deleted for the objects corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are deleted for objects corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}