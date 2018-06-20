// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="eGeometricNonlinearity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Geometric nonlinearity option to set in the non-linear analysis cases.
    /// </summary>
    public enum eGeometricNonlinearity
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// P-delta.
        /// </summary>
        PDelta = 1,

        /// <summary>
        /// P-delta plus large displacements.
        /// </summary>
        PDeltaLargeDisp = 2
    }
}
