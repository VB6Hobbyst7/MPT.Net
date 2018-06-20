// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eFrameType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Frame type available in the application.
    /// </summary>
    public enum eFrameType
    {
        /// <summary>
        /// Straight frame.
        /// </summary>
        Straight = 1,

        /// <summary>
        /// Curved frame.
        /// </summary>
        Curved = 2
    }
}
#endif