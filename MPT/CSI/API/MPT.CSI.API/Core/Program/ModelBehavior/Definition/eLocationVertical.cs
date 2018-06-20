// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eLocationVertical.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20 || BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Location reference by vertical position.
    /// Used in SectioCut.Get/SetResultsSide.
    /// </summary>
    public enum eLocationVertical
    {
        /// <summary>
        /// Top.
        /// </summary>
        Top = 1,

        /// <summary>
        /// Bottom.
        /// </summary>
        Bottom = 2
    }
}
#endif