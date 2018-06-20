// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IFrameInsertionPoint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a gettable/settable insertion point.
    /// </summary>
    public interface IFrameInsertionPoint
    {
        /// <summary>
        /// This function retrieves frame object insertion point assignments.
        /// The assignments include the cardinal point and end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing frame object</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the line element. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the line element. [L]</param>
        /// <param name="cardinalPoint">The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="isMirroredLocal2">True: Frame object section is assumed to be mirrored (flipped) about its local 2-axis.</param>
        /// <param name="isStiffnessTransformed">True: Frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="coordinateSystem">This is <see cref="CoordinateSystems.Local" /> or the name of a defined coordinate system.
        /// It is the coordinate system in which the <paramref name="offsetDistancesI" /> and <paramref name="offsetDistancesJ" /> items are specified.</param>
        void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ,
            ref eCardinalInsertionPoint cardinalPoint,
            ref bool isMirroredLocal2,
            ref bool isStiffnessTransformed,
            ref string coordinateSystem);

        /// <summary>
        /// This function assigns frame object insertion point data.
        /// The assignments are reported as end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the frame object. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the frame object. [L]</param>
        /// <param name="cardinalPoint">Specifies the cardinal point for the frame object.
        /// The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="isMirroredLocal2">True: The frame object section is assumed to be mirrored (flipped) about its local 2-axis.</param>
        /// <param name="isStiffnessTransformed">True: The frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="coordinateSystem">This is <see cref="ePDeltaDirection.Local_1" /> or the name of a defined coordinate system.
        /// It is the coordinate system in which the <paramref name="offsetDistancesI" /> and <paramref name="offsetDistancesJ" /> items are specified.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetInsertionPoint(string name,
            Displacements offsetDistancesI,
            Displacements offsetDistancesJ,
            eCardinalInsertionPoint cardinalPoint,
            bool isMirroredLocal2,
            bool isStiffnessTransformed,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object);


    }
}
