// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="eSolverType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Solver types available for analysis in the application.
    /// </summary>
    public enum eSolverType
    {
        /// <summary>
        /// Standard solver.
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Advanced solver.
        /// </summary>
        Advanced = 1,

        /// <summary>
        /// Multi-threaded solver.
        /// </summary>
        MultiThreaded = 2
    }
}
