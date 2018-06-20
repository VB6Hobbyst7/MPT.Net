// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="ePointAlignOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Method by which a point object is aligned.
    /// </summary>
    public enum ePointAlignOption
    {
        /// <summary>
        /// Align points to X-ordinate in present coordinate system.
        /// </summary>
        XOrdinate = 1,

        /// <summary>
        /// Align points to Y-ordinate in present coordinate system.
        /// </summary>
        YOrdinate = 2,

        /// <summary>
        /// Align points to Z-ordinate in present coordinate system.
        /// </summary>
        ZOrdinate = 3,

        /// <summary>
        /// Align points to nearest selected line object, area object edge or solid object edge.
        /// </summary>
        NearestLineOrEdge = 4
    }
}

#endif