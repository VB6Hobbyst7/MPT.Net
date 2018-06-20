// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeablePattern.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set the point pattern assignment.
    /// </summary>
    public interface IChangeablePattern
    {
        /// <summary>
        /// This function sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = [(z – zpoint) * w] + u
        /// where z(<paramref name="zCoordinateAtZeroPressure" />), w(<paramref name="weightPerUnitVolume" />) and u(<paramref name="uniformForcePerUnitArea" />) are described in the Parameters section and zpoint is the Z coordinate of the considered point object in the present coordinate system.
        /// All appropriate unit conversions are used to calculate the value in the database units, but thereafter it is assumed to be unitless.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="zCoordinateAtZeroPressure">The Z coordinate at zero pressure in the present coordinate system. [L]</param>
        /// <param name="weightPerUnitVolume">A weight per unit volume. [F/L^3]</param>
        /// <param name="uniformForcePerUnitArea">An added uniform force per unit area. [F/L^2]</param>
        /// <param name="restrictionCurrent">This restriction applies before the pattern value has been added to any existing pattern value assigned to the point object.</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetPatternByPressure(string name,
            string patternName,
            double zCoordinateAtZeroPressure,
            double weightPerUnitVolume,
            double uniformForcePerUnitArea,
            ePatternRestriction restrictionCurrent,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object);


        /// <summary>
        /// This function sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = ax + by + cz + d
        /// where a, b, c and d are function input parameters and x, y and z are the coordinates of the considered point object in the present coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="a">The x-coefficient in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="b">The y-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="c">The z-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="d">The constant in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to or replaced any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetPatternByCoordinates(string name,
            string patternName,
            double a,
            double b,
            double c,
            double d,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object);
    }
}
#endif