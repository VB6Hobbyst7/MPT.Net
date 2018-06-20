// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-12-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eAnalysisSteadyStateOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Display  style for results from steady state analyses in the application.
    /// </summary>
    public enum eAnalysisSteadyStateOptions
    {
        /// <summary>
        /// Envelopes.
        /// </summary>
        Envelopes = 1,

        /// <summary>
        /// At frequencies.
        /// </summary>
        Frequencies = 2
    }
#endif
}