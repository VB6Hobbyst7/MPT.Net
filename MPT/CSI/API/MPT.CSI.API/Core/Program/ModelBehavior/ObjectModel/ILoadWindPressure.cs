// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="ILoadWindPressure.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can have a wind pressure load assigned and set.
    /// </summary>
    public interface ILoadWindPressure
    {

        /// <summary>
        /// This function retrieves the wind pressure load assignments to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of wind pressure loads retrieved for the specified area objects.</param>
        /// <param name="areaNames">The name of the area object associated with each wind pressure load.</param>
        /// <param name="loadPatterns">The name of a defined load pattern for each load.</param>
        /// <param name="windPressureTypes">Wind pressure type for each load.</param>
        /// <param name="pressureCoefficients">This is the wind pressure coefficient for each load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadWindPressure(string name,
            ref int numberItems,
            ref string[] areaNames,
            ref string[] loadPatterns,
            ref eWindPressureApplication[] windPressureTypes,
            ref double[] pressureCoefficients,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="windPressureType">Wind pressure type.</param>
        /// <param name="pressureCoefficient">This is the wind pressure coefficient.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadWindPressure(string name,
            string loadPattern,
            eWindPressureApplication windPressureType,
            double pressureCoefficient,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadWindPressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
