// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSeismicCoefficient_NCHRP_2007.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Seismic coefficient for NCHRP 2007 seismic lateral code forces.
    /// </summary>
    public enum eSeismicCoefficient_NCHRP_2007
    {
        /// <summary>
        /// Ss and S1 are from USGS by latitiude and longitude.
        /// </summary>
        SsAndS1FromUSGSLatLong = 0,

        /// <summary>
        /// Ss and S1 are from USGS by zip code.
        /// </summary>
        SsAndS1FromUSGSZipCode = 1,

        /// <summary>
        /// Ss and S1 are user defined.
        /// </summary>
        SsAndS1UserDefined = 2
    }
}
#endif