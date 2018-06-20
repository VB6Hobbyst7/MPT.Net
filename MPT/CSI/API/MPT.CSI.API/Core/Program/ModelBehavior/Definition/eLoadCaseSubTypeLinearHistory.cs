// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLoadCaseSubTypeLinearHistory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Load case subtypes available in the program for linear modal time history load cases.
    /// </summary>
    public enum eLoadCaseSubTypeLinearHistory
    {
        /// <summary>
        /// Transient linear modal time history.
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Periodic linear modal time history.
        /// </summary>
        Periodic = 2
    }
}
#endif
