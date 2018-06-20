// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IMassLumped.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has Readable/Writeable mass properties lumped at a point.
    /// </summary>
    public interface IMassLumped
    {
        /// <summary>
        /// This function retrieves the point mass assignment values for a point object. The masses are always returned in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="masses">Mass assignment values.</param>
        void GetMass(string name,
            out Mass masses);

        /// <summary>
        /// This function assigns point mass to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetMass(string name,
            Mass masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="materialProperty">The name of an existing material property.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetMass(string name,
            MassVolume masses,
            string materialProperty,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns point mass to a point object.
        /// The program calculates the mass by dividing the specified values by the acceleration of gravity.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="masses">Mass assignment values.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetMass(string name,
            MassWeight masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object);
    }
}