// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="e2DFrameType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// 2D frame template types available in the application.
    /// </summary>
    public enum e2DFrameType
    {
        /// <summary>
        /// Portal frame.
        /// </summary>
        PortalFrame = 0,

        /// <summary>
        /// Concentric braced frame.
        /// </summary>
        ConcentricBraced = 1,

        /// <summary>
        /// Eccentric braced frame.
        /// </summary>
        EccentricBraced = 2
    }
}
#endif