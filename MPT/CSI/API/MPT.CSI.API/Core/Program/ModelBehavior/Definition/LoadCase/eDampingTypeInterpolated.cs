// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="eDampingTypeInterpolated.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Interpolated damping types available in the application.
    /// </summary>
    public enum eDampingTypeInterpolated
    {
        /// <summary>
        /// Interpolated damping by period.
        /// </summary>
        InterpolatedByPeriod = 5,

        /// <summary>
        /// Interpolated damping by frequency.
        /// </summary>
        InterpolatedByFrequency = 6
    }
}