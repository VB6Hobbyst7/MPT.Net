// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePlotType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Plot types available in the application.
    /// </summary>
    public enum ePlotType
    {
        /// <summary>
        /// Plot scaled to arithmetic type.
        /// </summary>
        Arithmetic = 1,

        /// <summary>
        /// Plot scaled to log type.
        /// </summary>
        Log = 2
    }
}
#endif