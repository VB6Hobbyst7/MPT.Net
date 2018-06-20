// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eDegreeOfFreedom.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Degrees of freedom available in the application.
    /// </summary>
    public enum eDegreeOfFreedom
    {
        /// <summary>
        /// Translation along the 1-axis.
        /// </summary>
        U1 =  1,

        /// <summary>
        /// Translation along the 2-axis.
        /// </summary>
        U2 = 2,

        /// <summary>
        /// Translation along the 3-axis.
        /// </summary>
        U3 = 3,

        /// <summary>
        /// Rotation about the 1-axis.
        /// </summary>
        R1 = 4,

        /// <summary>
        /// Rotation about the 2-axis.
        /// </summary>
        R2 = 5,

        /// <summary>
        /// Rotation about the 3-axis.
        /// </summary>
        R3 = 6
    }
}