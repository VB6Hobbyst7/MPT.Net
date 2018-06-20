// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eParameters_NTC_2008.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The option for defining the parameters for NTC 2008 seismic lateral code forces..
    /// </summary>
    public enum eParameters_NTC_2008
    {
        /// <summary>
        /// Parameters are by latitiude and longitude.
        /// </summary>
        ByLatLong = 1,

        /// <summary>
        /// Parameters are by island.
        /// </summary>
        ByIsland = 2,

        /// <summary>
        /// Parameters are user defined.
        /// </summary>
        UserDefined = 3
    }
}
#endif