// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eDampingTypeHysteretic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Hysteretic damping types available in the application.
    /// </summary>
    public enum eDampingTypeHysteretic
    {
        /// <summary>
        /// Constant hysteretic damping for all frequencies.
        /// </summary>
        Constant = 1,

        /// <summary>
        /// Interpolated hysteretic damping by frequency.
        /// </summary>
        Interpolated = 2
    }
}
#endif