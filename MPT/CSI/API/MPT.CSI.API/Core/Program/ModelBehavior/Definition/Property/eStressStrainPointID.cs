// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eStressStrainPointID.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Integers which sets the point ID.
    /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.
    /// </summary>
    public enum eStressStrainPointID
    {
        /// <summary>
        /// Point -E.
        /// </summary>
        NegativeE = -5,

        /// <summary>
        /// Point -D.
        /// </summary>
        NegativeD = -4,

        /// <summary>
        /// Point -C.
        /// </summary>
        NegativeC = -3,

        /// <summary>
        /// Point -B.
        /// </summary>
        NegativeB = -2,

        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Point A.
        /// </summary>
        A = 1,

        /// <summary>
        /// Point B.
        /// </summary>
        B = 2,

        /// <summary>
        /// Point C.
        /// </summary>
        C = 3,

        /// <summary>
        /// Point D.
        /// </summary>
        D = 4,

        /// <summary>
        /// Point E.
        /// </summary>
        E = 5
    }
}
