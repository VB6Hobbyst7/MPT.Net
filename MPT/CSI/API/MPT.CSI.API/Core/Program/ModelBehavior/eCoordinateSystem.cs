// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eCoordinateSystem.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Common coordinate system types.
    /// These are used with the program as strings.
    /// </summary>
    public enum eCoordinateSystem
    {
        /// <summary>
        /// Coordinate system is local to the element referenced.
        /// </summary>
        Local = 1,

        /// <summary>
        /// Coordinate system is the default or a user-defined global system.
        /// </summary>
        Global = 2,
    }
}
#endif