// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="eLength.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// Length unit types available in the application.
    /// </summary>
    public enum eLength
    {
        /// <summary>
        /// Not applicable.
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// Inch.
        /// </summary>
        inch = 1,

        /// <summary>
        /// Foot.
        /// </summary>
        ft = 2,

        /// <summary>
        /// Micron.
        /// </summary>
        micron = 3,

        /// <summary>
        /// Millimeter.
        /// </summary>
        mm = 4,

        /// <summary>
        /// Centimeter.
        /// </summary>
        cm = 5,

        /// <summary>
        /// Meter.
        /// </summary>
        m = 6
    }
}
#endif