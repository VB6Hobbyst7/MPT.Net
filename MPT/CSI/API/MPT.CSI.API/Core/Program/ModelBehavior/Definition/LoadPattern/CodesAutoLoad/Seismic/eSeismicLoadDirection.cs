// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSeismicLoadDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Directions for which the seismic load can be applied.
    /// </summary>
    public enum eSeismicLoadDirection
    {
        /// <summary>
        /// Global X.
        /// </summary>
        GlobalX = 1,

        /// <summary>
        /// Global Y.
        /// </summary>
        GlobalY = 2,

        /// <summary>
        /// Global Z.
        /// </summary>
        GlobalZ = 3
    }
}