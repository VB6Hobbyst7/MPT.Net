// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSeismicGroup_NEHRP_1997.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Seismic group for IBC 2003 seismic lateral code forces.
    /// </summary>
    public enum eSeismicGroup_NEHRP_1997
    {

        /// <summary>
        /// Seismic group I.
        /// </summary>
        I = 1,

        /// <summary>
        /// Seismic group II.
        /// </summary>
        II = 2,

        /// <summary>
        /// Seismic group III.
        /// </summary>
        III = 3
    }
}
#endif