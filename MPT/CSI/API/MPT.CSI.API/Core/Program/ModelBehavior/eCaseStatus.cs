// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-21-2017
// ***********************************************************************
// <copyright file="eCaseStatus.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Analysis status states of a load case.
    /// </summary>
    public enum eCaseStatus
    {
        /// <summary>
        /// Not run.
        /// </summary>
        [Description("Not Run")]
        NotRun = 1,

        /// <summary>
        /// Could not start.
        /// </summary>
        [Description("Could Not Start")]
        CouldNotStart = 2,

        /// <summary>
        /// Not finished.
        /// </summary>
        [Description("Not Finished")]
        NotFinished = 3,

        /// <summary>
        /// Finished.
        /// </summary>
        [Description("Finished")]
        Finished = 4    
    }
}
