// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_UBC_97.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for UBC 97 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_UBC_97
    {
        /// <summary>
        /// Site soil category class SA.
        /// </summary>
        SA = 1,

        /// <summary>
        /// Site soil category class SB.
        /// </summary>
        SB = 2,

        /// <summary>
        /// Site soil category class SC.
        /// </summary>
        SC = 3,

        /// <summary>
        /// Site soil category class SD.
        /// </summary>
        SD = 4,

        /// <summary>
        /// Site soil category class SE.
        /// </summary>
        SE = 5
    }
}
#endif