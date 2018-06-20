// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_NZS_4203_1992.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for NZS 4203 1992 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_NZS_4203_1992
    {
        /// <summary>
        /// Site subsoil category class A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Site subsoil category class B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Site subsoil category class C.
        /// </summary>
        C = 3,
    }
}
#endif