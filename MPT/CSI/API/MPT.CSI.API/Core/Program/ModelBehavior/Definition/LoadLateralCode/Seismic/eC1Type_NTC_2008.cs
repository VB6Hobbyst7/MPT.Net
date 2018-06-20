// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eC1Type_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Values of C1 used based on the time period option for NTC 2008 seismic lateral code forces..
    /// </summary>
    public enum eC1Type_NTC_2008
    {
        /// <summary>
        /// C1 = 0.085 (m).
        /// </summary>
        C1_85 = 1,

        /// <summary>
        /// C1 = 0.075 (m).
        /// </summary>
        C1_75 = 2,

        /// <summary>
        /// C1 = 0.050 (m).
        /// </summary>
        C1_50
    }
}
#endif
