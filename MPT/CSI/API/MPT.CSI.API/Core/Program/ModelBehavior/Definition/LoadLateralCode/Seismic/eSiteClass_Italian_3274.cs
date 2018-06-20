// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSiteClass_Italian_3274.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Site class for Italian 3274 seismic lateral code forces.
    /// </summary>
    public enum eSiteClass_Italian_3274
    {
        /// <summary>
        /// Site soil type seismic intensity class A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Site soil type seismic intensity class B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Site soil type seismic intensity class C.
        /// </summary>
        C = 3,

        /// <summary>
        /// Site soil type seismic intensity class D.
        /// </summary>
        D = 4,
    }
#endif
}