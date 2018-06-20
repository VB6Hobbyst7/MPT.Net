// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eFunctionSteadyStateType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Types of Steady State functions available in the application.
    /// </summary>
    public enum eFunctionSteadyStateType
    {
        /// <summary>
        /// Function is read from an external file.
        /// </summary>
        FromFile = 0,

        /// <summary>
        /// User-defined function.
        /// </summary>
        User = 1
    }
}
#endif
