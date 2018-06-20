// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_Eurocode_8_2004.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Site class for Eurocode 8-2004 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_Eurocode_8_2004
    {
        /// <summary>
        /// Site ground type class A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Site ground type class B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Site ground type class C.
        /// </summary>
        C = 3,

        /// <summary>
        /// Site ground type class D.
        /// </summary>
        D = 4,

        /// <summary>
        /// Site ground type class E.
        /// </summary>
        E = 5,
    }
}
#endif