// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="eReferenceVector.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// The axis/plane reference vector option.
    /// </summary>
    public enum eReferenceVector
    {
        /// <summary>
        /// Coordinate direction.
        /// </summary>
        CoordinateDirection = 1,

        /// <summary>
        /// Two joints.
        /// </summary>
        TwoJoints = 2,

        /// <summary>
        /// User vector.
        /// </summary>
        UserVetor = 3
    }
}
