// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-12-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eAnalysisPSDOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Options available for output from PSD analysis.
    /// </summary>
    public enum eAnalysisPSDOptions
    {
        /// <summary>
        /// The RMS (Root Mean Square).
        /// </summary>
        RMS = 1,

        /// <summary>
        /// The sqrt(PSD).
        /// </summary>
        SQRTPSD = 2
    }
#endif
}