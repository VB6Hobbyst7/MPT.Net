// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eWallSpandrelRebarLayerType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Wall spandrel rebar layer types available in the application.
    /// </summary>
    public enum eWallSpandrelRebarLayerType
    {
        /// <summary>
        /// Horizontal, total top rebar.
        /// </summary>
        HorizontalTopTotal = 1,

        /// <summary>
        /// Horizontal, total bottom rebar.
        /// </summary>
        HorizontalBottomTotal = 2,

        /// <summary>
        /// Horizontal, rebar distributed across each face.
        /// </summary>
        HorizontalDistributedEachface = 3,

        /// <summary>
        /// Vertical, distributed ties.
        /// </summary>
        VerticalTiesDistributed = 4,

        /// <summary>
        /// Each rebar is diagonal.
        /// </summary>
        DiagonalEach = 5 
    }
}
#endif