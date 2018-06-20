// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eSpectralResponse.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Spectral response types available in the application.
    /// </summary>
    public enum eSpectralResponse
    {
        /// <summary>
        /// Spectral displacement.
        /// </summary>
        SpectralDisplacement = 1,

        /// <summary>
        /// Spectral velo.
        /// </summary>
        SpectralVelocity = 2,

        /// <summary>
        /// Pseudo-spectral velocity.
        /// </summary>
        PseudoSpectralVelocity = 3,

        /// <summary>
        /// Spectral acceleration.
        /// </summary>
        SpectralAcceleration = 4,

        /// <summary>
        /// Pseudo-spectral acceleration.
        /// </summary>
        PseudoSpectralAcceleration = 5
    }
}
#endif
