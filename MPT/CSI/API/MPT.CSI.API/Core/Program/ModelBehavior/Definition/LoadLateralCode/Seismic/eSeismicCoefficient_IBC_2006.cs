// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSeismicCoefficient_IBC_2006.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// Seismic coefficient for IBC 2006 seismic lateral code forces.
    /// </summary>
    public enum eSeismicCoefficient_IBC_2006
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