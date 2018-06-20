// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eSpringSimpleType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Simple spring types available in the application.
    /// </summary>
    public enum eSpringSimpleType
    {
        /// <summary>
        /// Spring resists tension and compression.
        /// </summary>
        TensionCompression = 1,

        /// <summary>
        /// Spring resists compression only.
        /// </summary>
        Compression =  2,

        /// <summary>
        /// Spring resists tension only.
        /// </summary>
        Tension = 3
    }
}
#endif