// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eAxisOrientation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Axis orientations available in the application.
    /// </summary>
    public enum eAxisOrientation
    {
        /// <summary>
        /// Parallel to x axis.
        /// </summary>
        ParallelToXAxis = 1,

        /// <summary>
        /// Parallel to y axis.
        /// </summary>
        ParallelToYAxis = 2,

        /// <summary>
        /// Parallel to z axis.
        /// </summary>
        ParallelToZAxis = 3,

        /// <summary>
        /// 3D line.
        /// </summary>
        Line3D = 4
    }
}
#endif