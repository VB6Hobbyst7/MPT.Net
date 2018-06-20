// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_AS_1170_2007.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for AS 1170 2007 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_AS_1170_2007
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
    }
}
#endif