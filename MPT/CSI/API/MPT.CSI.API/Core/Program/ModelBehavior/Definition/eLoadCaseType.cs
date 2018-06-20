// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLoadCaseType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Load case type available in the application.
    /// </summary>
    public enum eLoadCaseType
    {
        /// <summary>
        /// Linear Static.
        /// </summary>
        LinearStatic = 1,

        /// <summary>
        /// Nonlinear Static.
        /// </summary>
        NonlinearStatic = 2,

        /// <summary>
        /// Modal.
        /// </summary>
        Modal = 3,

        /// <summary>
        /// Response Spectrum.
        /// </summary>
        ResponseSpectrum = 4,

        /// <summary>
        /// Linear Modal Time History.
        /// </summary>
        LinearModalTimeHistory = 5,

        /// <summary>
        /// Nonlinear Modal Time History.
        /// </summary>
        NonlinearModalTimeHistory = 6,

        /// <summary>
        /// Linear Direct Integration Time History.
        /// </summary>
        LinearDirectIntegrationTimeHistory = 7,

        /// <summary>
        /// Nonlinear Direct Integration Time History.
        /// </summary>
        NonlinearDirectIntegrationTimeHistory = 8,

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Moving Load.
        /// </summary>
        MovingLoad = 9,
#endif

        /// <summary>
        /// Buckling.
        /// </summary>
        Buckling = 10,

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// SteadyState.
        /// </summary>
        SteadyState = 11,

        /// <summary>
        /// Power Spectral Density.
        /// </summary>
        PowerSpectralDensity = 12,

        /// <summary>
        /// Linear Static Multistep.
        /// </summary>
        LinearStaticMultistep = 13,
#endif

        /// <summary>
        /// Hyperstatic.
        /// </summary>
        Hyperstatic = 14
    }
}
