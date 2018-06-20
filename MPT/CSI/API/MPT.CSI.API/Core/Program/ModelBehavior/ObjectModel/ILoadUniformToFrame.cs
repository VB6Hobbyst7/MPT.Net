// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="ILoadUniformToFrame.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has Gettable/Settable uniform area load to an adjacent frame.
    /// </summary>
    public interface ILoadUniformToFrame
    {
        /// <summary>
        /// This function retrieves the wind pressure load assignments to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="numberItems">The total number of wind pressure loads retrieved for the specified area objects.</param>
        /// <param name="loadPatterns">The name of a defined load pattern for each load.</param>
        /// <param name="areaNames">The name of the area object associated with each uniform load.</param>
        /// <param name="values">The uniform load value for each load. [F/L^2]</param>
        /// <param name="directions">Indicates the direction of the applied load for each load.</param>
        /// <param name="distributionTypes">Load distribution type for how the load is tranferred to element edges for each load.</param>
        /// <param name="coordinateSystems">This is the name of a defined coordinate system associated with wach uniform load</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadUniformToFrame(string name,
            ref int numberItems,
            ref string[] loadPatterns,
            ref string[] areaNames,
            ref double[] values,
            ref eLoadDirection[] directions,
            ref eLoadDistributionType[] distributionTypes,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <param name="value">The uniform load value. [F/L^2]</param>
        /// <param name="direction">Indicates the direction of the applied load.</param>
        /// <param name="distributionType">Load distribution type for how the load is tranferred to element edges.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified area object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="coordinateSystem">This is <see cref="CoordinateSystems.Local" /> or the name of a defined coordinate system, indicating the coordinate system in which the uniform load is specified.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadUniformToFrame(string name,
            string loadPattern,
            double value,
            eLoadDirection direction,
            eLoadDistributionType distributionType,
            bool replace = true,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object);
    }
}
