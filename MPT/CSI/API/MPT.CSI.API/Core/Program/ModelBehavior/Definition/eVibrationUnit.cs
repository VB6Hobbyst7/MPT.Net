// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eVibrationUnit.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Units by which vibration may be quantified.
    /// </summary>
    public enum eVibrationUnit
    {
        /// <summary>
        /// Frequency [1/sec].
        /// </summary>
        Frequency = 1,

        /// <summary>
        /// Period [sec].
        /// </summary>
        Period = 2
    }
}
#endif
