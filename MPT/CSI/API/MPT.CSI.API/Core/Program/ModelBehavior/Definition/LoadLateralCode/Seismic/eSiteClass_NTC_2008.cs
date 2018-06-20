// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for NTC 2008 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_NTC_2008
    {
        /// <summary>
        /// Site subsoil type class A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Site subsoil type class B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Site subsoil type class C.
        /// </summary>
        C = 3,

        /// <summary>
        /// Site subsoil type class D.
        /// </summary>
        D = 4,

        /// <summary>
        /// Site subsoil type class E.
        /// </summary>
        E = 5,
    }
}
#endif