// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eAreaOffsetType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Offset types used for areas in the application.
    /// </summary>
    public enum eAreaOffsetType
    {
        /// <summary>
        /// Offset all area edges.
        /// </summary>
        AllEdges = 0,

        /// <summary>
        /// Offset selected area edges only.
        /// </summary>
        SelectedEdges = 1,

        /// <summary>
        /// Offset selected points of selected areas only.
        /// </summary>
        SelectedPoints = 2
    }
}
#endif