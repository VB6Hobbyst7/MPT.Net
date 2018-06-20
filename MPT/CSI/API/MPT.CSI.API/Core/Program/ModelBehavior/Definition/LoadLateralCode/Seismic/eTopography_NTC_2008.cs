// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eTopography_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The topography for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eTopography_NTC_2008
    {
        /// <summary>
        /// Topography type T1.
        /// </summary>
        T1 = 1,

        /// <summary>
        /// Topography type T2.
        /// </summary>
        T2 = 2,

        /// <summary>
        /// Topography type T3.
        /// </summary>
        T3 = 3,

        /// <summary>
        /// Topography type T4.
        /// </summary>
        T4 = 4,
    }
}
#endif