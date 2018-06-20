// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eFrequencyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Frequency types used in power spectral density functions.
    /// </summary>
    public enum eFrequencyType
    {
        /// <summary>
        /// Frequency as hertz [cyc/s].
        /// </summary>
        HZ = 1,

        /// <summary>
        /// Frequency as rotations per minute.
        /// </summary>
        RPM = 2
    }
}
#endif