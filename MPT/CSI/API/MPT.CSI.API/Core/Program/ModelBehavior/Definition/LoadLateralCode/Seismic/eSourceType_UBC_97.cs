// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSourceType_UBC_97.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Seismic source type for UBC 97 seismic lateral code forces.
    /// </summary>
    public enum eSourceType_UBC_97
    {
        /// <summary>
        /// Seismic source type A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Seismic source type B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Seismic source type C.
        /// </summary>
        C = 3,
    }
}
#endif