// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eStation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Orientations relative to a given station.
    /// </summary>
    public enum eStation
    {
        /// <summary>
        /// Before the specified station.
        /// </summary>
        Before = 1,

        /// <summary>
        /// After the specified station.
        /// </summary>
        After = 2
    }
}
#endif