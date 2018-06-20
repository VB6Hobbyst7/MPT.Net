// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eLimitState_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The limit state for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eLimitState_NTC_2008
    {
        /// <summary>
        /// SLO.
        /// </summary>
        SLO = 1,

        /// <summary>
        /// SLD.
        /// </summary>
        SLD = 2,

        /// <summary>
        /// SLV.
        /// </summary>
        SLV = 3,

        /// <summary>
        /// SLC.
        /// </summary>
        SLC = 4,
    }
}
#endif