// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-14-2017
//
// Last Modified By : Mark
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="eDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Directions available in the application, of global and local combined.
    /// </summary>
    public enum eDirection
    {
        /// <summary>
        /// The local 1 or global x direction.
        /// </summary>
        Local1GlobalX = 1,

        /// <summary>
        /// The local 2 or global y direction.
        /// </summary>
        Local2GlobalY = 2,

        /// <summary>
        /// The local 3 or global z direction.
        /// </summary>
        Local3GlobalZ = 3
    }
}