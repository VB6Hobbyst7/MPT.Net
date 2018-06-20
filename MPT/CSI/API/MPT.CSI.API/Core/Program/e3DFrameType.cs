// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="e3DFrameType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// 3D frame template types available in the application.
    /// </summary>
    public enum e3DFrameType
    {
        /// <summary>
        /// Open frame.
        /// </summary>
        OpenFrame = 0,

        /// <summary>
        /// Perimeter frame.
        /// </summary>
        PerimeterFrame = 1,

        /// <summary>
        /// Beam-slab.
        /// </summary>
        BeamSlab = 2,

        /// <summary>
        /// Flat plate..
        /// </summary>
        FlatPlate = 3,
    }
}
#endif