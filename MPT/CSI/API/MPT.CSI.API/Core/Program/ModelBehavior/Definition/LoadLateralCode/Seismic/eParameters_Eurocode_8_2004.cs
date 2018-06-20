// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eParameters_Eurocode_8_2004.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Indicates the country for which the Nationally Determined Parameters (NDPs) are specified for Eurocode 8-2004 seismic lateral code forces.
    /// </summary>
    public enum eParameters_Eurocode_8_2004
    {
        /// <summary>
        /// Other (NDPs) are specified.
        /// </summary>
        Other = 0,

        /// <summary>
        /// CEN NDP defaults.
        /// </summary>
        CENDefault = 1,

        /// <summary>
        /// Norway NDPs.
        /// </summary>
        Norway = 2,

        /// <summary>
        /// Portugal NPDs.
        /// </summary>
        Portugal = 3,
    }
}
#endif