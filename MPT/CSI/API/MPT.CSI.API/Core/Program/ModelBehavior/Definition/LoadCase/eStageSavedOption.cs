// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eStageSavedOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Options for saving stage results available in the application.
    /// </summary>
    public enum eStageSavedOption
    {
        /// <summary>
        /// End of final stage.
        /// </summary>
        EndOfFinalStage = 0,

        /// <summary>
        /// End of each stage.
        /// </summary>
        EndOfEachStage = 1,

        /// <summary>
        /// Start and end of each stage.
        /// </summary>
        StartAndEndOfEachStage = 2,

        /// <summary>
        /// Two or more times in each stage.
        /// </summary>
        TwoOrMoreTimesPerStage = 3
    }
}