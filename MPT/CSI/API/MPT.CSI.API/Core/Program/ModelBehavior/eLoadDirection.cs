// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eLoadDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Load directions available in the application.
    /// </summary>
    public enum eLoadDirection
    {
        /// <summary>
        /// Local 1 axis direction.
        /// Applies only when corresponding coordinate system is set to Local.
        /// </summary>
        Local1 = 1,

        /// <summary>
        /// Local 2 axis direction.
        /// Applies only when corresponding coordinate system is set to Local.
        /// </summary>
        Local2 = 2,

        /// <summary>
        /// Local 3 axis direction.
        /// Applies only when corresponding coordinate system is set to Local.
        /// </summary>
        Local3 = 3,

        /// <summary>
        /// Global X direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        XDirection = 4,

        /// <summary>
        /// Global Y direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        YDirection = 5,

        /// <summary>
        /// Global Z direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        ZDirection = 6,

        /// <summary>
        /// Projected X direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        ProjectedXDirection = 7,

        /// <summary>
        /// Projected Y direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        ProjectedYDirection = 8,

        /// <summary>
        /// Projected Z direction.
        /// Applies only when corresponding coordinate system is not set to Local.
        /// </summary>
        ProjectedZDirection = 9,

        /// <summary>
        /// Gravity direction.
        /// Applies only when corresponding coordinate system is set to Global.
        /// </summary>
        GravityDirection = 10,

        /// <summary>
        /// Projected gravity direction.
        /// Applies only when corresponding coordinate system is set to Global.
        /// </summary>
        ProjectedGravityDirection = 11
    }
}
