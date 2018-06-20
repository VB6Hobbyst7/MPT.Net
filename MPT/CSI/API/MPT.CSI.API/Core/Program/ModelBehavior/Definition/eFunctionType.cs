// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="eFunctionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Function types available in the program for applying dynamic loads.
    /// </summary>
    public enum eFunctionType
    {
        /// <summary>
        /// Response spectrum.
        /// </summary>
        ResponseSpectrum = 1,

        /// <summary>
        /// Time history.
        /// </summary>
        TimeHistory = 2,

        /// <summary>
        /// Power spectral density.
        /// </summary>
        PowerSpectralDensity = 3,

        /// <summary>
        /// Steady state.
        /// </summary>
        SteadyState = 4
    }
}
