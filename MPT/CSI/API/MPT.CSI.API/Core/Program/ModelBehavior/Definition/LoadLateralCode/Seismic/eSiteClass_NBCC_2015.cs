// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_NBCC_2015.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for NBCC 2015 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_NBCC_2015
    {
        /// <summary>
        /// Site class A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Site class B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Site class C.
        /// </summary>
        C = 3,

        /// <summary>
        /// Site class D.
        /// </summary>
        D = 4,

        /// <summary>
        /// Site class E.
        /// </summary>
        E = 5,

        /// <summary>
        /// Site class F.
        /// </summary>
        F = 6
    }
}
#endif