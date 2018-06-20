// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ePDeltaDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Directions available for P-Delta force assignment.
    /// </summary>
    public enum ePDeltaDirection
    {
        /// <summary>
        /// Frame object local 1-axis direction.
        /// </summary>
        Local_1 = 0,

        /// <summary>
        /// Projected X direction in global coordinate system.
        /// </summary>
        ProjectedX = 1,

        /// <summary>
        /// Projected Y direction in global coordinate system.
        /// </summary>
        ProjectedY = 2,

        /// <summary>
        /// Projected Z direction in global coordinate system.
        /// </summary>
        ProjectedZ = 3,
    }
}
