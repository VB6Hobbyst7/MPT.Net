// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eMeshType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Mesh types available for meshing in the application.
    /// </summary>
    public enum eMeshType
    {

        /// <summary>
        /// No automatic meshing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Mesh into a specified number of objects.
        /// </summary>
        SpecifiedNumber = 1,

        /// <summary>
        /// Mesh into objects of a specified maximum size.
        /// </summary>
        SpecifiedMaxSize = 2,

        /// <summary>
        /// Mesh based on points on area edge.
        /// Only applies to area objects.
        /// </summary>
        PointsOnAreaEdges = 3,

        /// <summary>
        /// Cookie cut mesh based on lines intersecting edges.
        /// Only applies to area objects.
        /// </summary>
        CookieCutLinesIntersectingEdges = 4,

        /// <summary>
        /// Cookie cut mesh based on points.
        /// Only applies to area objects.
        /// </summary>
        CookieCutPoints = 5,

        /// <summary>
        /// Mesh using General Divide Tool.
        /// Only applies to area objects.
        /// </summary>
        GeneralDivideTool = 6
    }
}
#endif