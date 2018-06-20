// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-12-2017
//
// Last Modified By : Mark
// Last Modified On : 07-12-2017
// ***********************************************************************
// <copyright file="eAnalysisMultiStepOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Options for multi-step analysis.
    /// </summary>
    public enum eAnalysisMultiStepOptions
    {
        /// <summary>
        /// Envelopes.
        /// </summary>
        Envelopes = 1,

        /// <summary>
        /// Step by step.
        /// </summary>
        StepByStep = 2,

        /// <summary>
        /// Last step.
        /// </summary>
        LastStep = 3
    }
}
