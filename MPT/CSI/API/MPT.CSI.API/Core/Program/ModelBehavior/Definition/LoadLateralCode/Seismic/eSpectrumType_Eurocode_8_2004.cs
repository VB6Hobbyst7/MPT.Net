// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSpectrumType_Eurocode_8_2004.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The spectrum type for Eurocode 8-2004 seismic lateral code forces.
    /// </summary>
    public enum eSpectrumType_Eurocode_8_2004
    {
        /// <summary>
        /// Type 1 response spectrum.
        /// </summary>
        Type1 = 1,

        /// <summary>
        /// Type 2 response spectrum.
        /// </summary>
        Type2 = 2,
    }
}
#endif