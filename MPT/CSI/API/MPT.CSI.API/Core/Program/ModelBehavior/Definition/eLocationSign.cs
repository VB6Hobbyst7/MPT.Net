// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLocationSign.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Location reference by sign.
    /// Used in SectioCut.Get/SetResultsSide.
    /// </summary>
    public enum eLocationSign
    {
        /// <summary>
        /// Positive 3-axis side of quadrilateral.
        /// </summary>
        Positive3 = 1,

        /// <summary>
        /// Negative 3-axis side of quadrilateral.
        /// </summary>
        Negative3 = 2
    }
}
#endif