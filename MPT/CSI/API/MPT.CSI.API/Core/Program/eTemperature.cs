// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="eTemperature.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// Temperature unit types available in the application.
    /// </summary>
    public enum eTemperature
    {
        /// <summary>
        /// Not applicable.
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// Fahrenheit.
        /// </summary>
        F = 1,

        /// <summary>
        /// Celcius.
        /// </summary>
        C = 2
    }
}
#endif