// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_AASHTO_2006.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site soil profile type class for AASHTO 2006 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_AASHTO_2006
    {
        /// <summary>
        /// Site soil profile type class I.
        /// </summary>
        I = 1,

        /// <summary>
        /// Site soil profile type class II.
        /// </summary>
        II = 2,

        /// <summary>
        /// Site soil profile type class III.
        /// </summary>
        III = 3,

        /// <summary>
        /// Site soil profile type class IV.
        /// </summary>
        IV = 4,
    }
}
#endif