// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eFunctionTimeHistoryType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Types of Time History functions available in the application.
    /// </summary>
    public enum eFunctionTimeHistoryType
    {
        /// <summary>
        /// Function is read from an external file.
        /// </summary>
        FromFile = 0,

        /// <summary>
        /// User-defined function.
        /// </summary>
        User = 1,

        /// <summary>
        /// Sine function.
        /// </summary>
        Sine = 2,

        /// <summary>
        /// Cosine function.
        /// </summary>
        Cosine = 3,

        /// <summary>
        /// Ramp function.
        /// </summary>
        Ramp = 4,

        /// <summary>
        /// Sawtooth function.
        /// </summary>
        Sawtooth = 5,

        /// <summary>
        /// Triangular function.
        /// </summary>
        Triangular = 6,

        /// <summary>
        /// User periodic function.
        /// </summary>
        UserPeriodic = 7
    }
}
#endif
