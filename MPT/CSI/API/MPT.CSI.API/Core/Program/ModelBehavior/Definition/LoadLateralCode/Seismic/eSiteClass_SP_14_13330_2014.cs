// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_SP_14_13330_2014.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for SP 14.13330.2014 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_SP_14_13330_2014
    {
        /// <summary>
        /// Site soil category class I.
        /// </summary>
        I = 1,

        /// <summary>
        /// Site soil category class II.
        /// </summary>
        II = 2,

        /// <summary>
        /// Site soil category class III.
        /// </summary>
        III = 3,

        /// <summary>
        /// Site soil category class IV.
        /// </summary>
        IV = 4,
    }
}
#endif