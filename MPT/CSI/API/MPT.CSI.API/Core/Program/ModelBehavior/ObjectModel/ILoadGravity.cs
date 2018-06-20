// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadGravity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable gravity load.
    /// </summary>
    public interface ILoadGravity
    {
        /// <summary>
        /// This function retrieves the gravity load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of gravity loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadGravity(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] xLoadMultiplier,
            ref double[] yLoadMultiplier,
            ref double[] zLoadMultiplier,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns gravity loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multiplier in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multiplier in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multiplier in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the gravity load.</param>
        /// <param name="replace">True: All previous gravity loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadGravity(string name,
            string loadPattern,
            double xLoadMultiplier,
            double yLoadMultiplier,
            double zLoadMultiplier,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the gravity load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadGravity(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif