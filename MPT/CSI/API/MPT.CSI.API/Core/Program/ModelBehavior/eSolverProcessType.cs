// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="eSolverProcessType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Process options available for analysis in the application.
    /// </summary>
    public enum eSolverProcessType
    {
        /// <summary>
        /// Auto (program-determined).
        /// </summary>
        Auto = 0,

        /// <summary>
        /// GUI/Same process.
        /// </summary>
        Same = 1,

        /// <summary>
        /// Separate process.
        /// </summary>
        Separate = 2
    }
}
