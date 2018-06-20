// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeablePointSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can set the point spring assignment.
    /// </summary>
    public interface IChangeablePointSpring
    {
        /// <summary>
        /// This function assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="stiffnesses">Spring stiffness values for each decoupled degree of freedom.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetSpring(string name,
            Stiffness stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="stiffnesses">Spring stiffness values for each coupled degree of freedom.</param>
        /// <param name="isLocalCoordinateSystem">True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetSpringCoupled(string name,
            StiffnessCoupled stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object);
    }
}
