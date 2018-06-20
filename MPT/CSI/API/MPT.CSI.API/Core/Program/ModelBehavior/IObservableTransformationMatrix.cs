// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservableTransformationMatrix.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return a transformation matrix of direction cosines between the local and present global coordinates.
    /// </summary>
    public interface IObservableTransformationMatrix
    {
        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameObject">The name of an existing object.</param>
        /// <param name="directionCosines">Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.</param>
        void GetTransformationMatrix(string nameObject,
            out double[] directionCosines);
    }
}
