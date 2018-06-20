// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="eReferenceVectorDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// The axis/plane reference vector primary and secondary coordinate directions, PlDir(0) and PlDir(1) respectively.
    /// These are taken at the object center in the specified coordinate system and used to determine the plane reference vector.
    /// </summary>
    public enum eReferenceVectorDirection
    {
        /// <summary>
        /// Positive X.
        /// </summary>
        PositiveX = 1,

        /// <summary>
        /// Negative X.
        /// </summary>
        NegativeX = -1,

        /// <summary>
        /// Positive Y.
        /// </summary>
        PositiveY = 2,

        /// <summary>
        /// Negative Y.
        /// </summary>
        NegativeY = -2,

        /// <summary>
        /// Positive Z.
        /// </summary>
        PositiveZ = 3,

        /// <summary>
        /// Negative Z.
        /// </summary>
        NegativeZ = -3,


        /// <summary>
        /// Positive CR.
        /// </summary>
        PositiveCR = 4,

        /// <summary>
        /// Negative CRX.
        /// </summary>
        NegativeCR = -4,

        /// <summary>
        /// Positive CA.
        /// </summary>
        PositiveCA = 5,

        /// <summary>
        /// Negative CA.
        /// </summary>
        NegativeCA = -5,

        /// <summary>
        /// Positive CZ.
        /// </summary>
        PositiveCZ = 6,

        /// <summary>
        /// Negative CZ.
        /// </summary>
        NegativeCZ = -6,


        /// <summary>
        /// Positive SR.
        /// </summary>
        PositiveSR = 7,

        /// <summary>
        /// Negative SR.
        /// </summary>
        NegativeSR = -7,

        /// <summary>
        /// Positive SA.
        /// </summary>
        PositiveSA = 8,

        /// <summary>
        /// Negative SA.
        /// </summary>
        NegativeSA = -8,

        /// <summary>
        /// Positive SB.
        /// </summary>
        PositiveSB = 9,

        /// <summary>
        /// Negative SB.
        /// </summary>
        NegativeSB = -9,

    }
}
