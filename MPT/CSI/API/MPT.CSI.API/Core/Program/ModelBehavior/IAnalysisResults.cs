// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IAnalysisResults.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements the analysis setup and results in the application.
    /// </summary>
    public interface IAnalysisResults
    {
        #region Properties        
        /// <summary>
        /// Gets the analysis results setup.
        /// </summary>
        /// <value>The analysis results setup.</value>
        AnalysisResultsSetup AnalysisResultsSetup { get; }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <value>The results.</value>
        Results Results { get; }
        #endregion
    }
}