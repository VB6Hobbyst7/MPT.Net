// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_IS_1893_2002.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for IS 1893-2002 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_IS_1893_2002
    {
        /// <summary>
        /// Site soil type class I.
        /// </summary>
        I = 1,

        /// <summary>
        /// Site soil type class II.
        /// </summary>
        II = 2,

        /// <summary>
        /// Site soil type class III.
        /// </summary>
        III = 3,
    }
}
#endif