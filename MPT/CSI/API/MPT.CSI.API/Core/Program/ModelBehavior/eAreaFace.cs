// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eAreaFace.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Face designations in the application for areas and solids.
    /// </summary>
    public enum eFace
    {
        /// <summary>
        /// Top face of an area element.
        /// </summary>
        TopFace = -2,

        /// <summary>
        /// Bottom face of an area element.
        /// </summary>
        BottomFace = -1,

        /// <summary>
        /// Edge face of an area element.
        /// Technically any number &gt;= 0 for area elements.
        /// </summary>
        EdgeFace = 0,

        /// <summary>
        /// Face 1 of a solid.
        /// </summary>
        Face1 = 1,

        /// <summary>
        /// Face 2 of a solid.
        /// </summary>
        Face2 = 2,

        /// <summary>
        /// Face 3 of a solid.
        /// </summary>
        Face3 = 3,

        /// <summary>
        /// Face 4 of a solid.
        /// </summary>
        Face4 = 4,

        /// <summary>
        /// Face 5 of a solid.
        /// </summary>
        Face5 = 5,

        /// <summary>
        /// Face 6 of a solid.
        /// </summary>
        Face6 = 6,
    }
}
#endif