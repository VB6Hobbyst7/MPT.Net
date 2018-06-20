// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSeismicIntensity_SP_14_13330_2014.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Seismic intensity for SP 14.13330.2014 seismic lateral code forces.
    /// </summary>
    public enum eSeismicIntensity_SP_14_13330_2014
    {
        /// <summary>
        /// Intensity 6.
        /// </summary>
        Intensity6 = 1,

        /// <summary>
        /// Intensity 7.
        /// </summary>
        Intensity7 = 2,

        /// <summary>
        /// Intensity 8.
        /// </summary>
        Intensity8 = 3,

        /// <summary>
        /// Intensity 9.
        /// </summary>
        Intensity9 = 4,
    }
}
#endif