// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-12-2017
//
// Last Modified By : Mark
// Last Modified On : 07-12-2017
// ***********************************************************************
// <copyright file="eAnalysisMultiValuedOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Options for multi-valued analysis.
    /// </summary>
    public enum eAnalysisMultiValuedOptions
    {
        /// <summary>
        /// Envelopes.
        /// </summary>
        Envelopes = 1,

        /// <summary>
        /// Multiple values, if possible.
        /// </summary>
        StepByStep = 2,

        /// <summary>
        /// Correspondence.
        /// </summary>
        LastStep = 3
    }
}
