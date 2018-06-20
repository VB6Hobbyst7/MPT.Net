// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eAxisExtrusion.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Axis of extrusion used in extruding objects.
    /// </summary>
    public enum eAxisExtrusion
    {
        /// <summary>
        /// The x axis
        /// </summary>
        XAxis = 0,

        /// <summary>
        /// The y axis
        /// </summary>
        YAxis = 1,

        /// <summary>
        /// The z axis.
        /// </summary>
        ZAxis = 2
    }
}
#endif