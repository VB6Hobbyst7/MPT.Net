// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eUsageClass_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The usage class for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eUsageClass_NTC_2008
    {
        /// <summary>
        /// Usage class I.
        /// </summary>
        I = 1,

        /// <summary>
        /// Usage class II.
        /// </summary>
        II = 2,

        /// <summary>
        /// Usage class III.
        /// </summary>
        III = 3,

        /// <summary>
        /// Usage class IV.
        /// </summary>
        IV = 4,
    }
}
#endif