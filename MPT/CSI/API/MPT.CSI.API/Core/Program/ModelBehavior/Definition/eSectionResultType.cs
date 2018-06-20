// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eSectionResultType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// The result type of the section cut.
    /// </summary>
    public enum eSectionResultType
    {
        /// <summary>
        /// Analysis.
        /// </summary>
        Analysis = 1,

        /// <summary>
        /// Design wall.
        /// </summary>
        DesignWall = 2,

        /// <summary>
        /// Design spandrel.
        /// </summary>
        DesignSpandrel = 3,

        /// <summary>
        /// Design slab.
        /// </summary>
        DesignSlab = 4
    }
}
#endif