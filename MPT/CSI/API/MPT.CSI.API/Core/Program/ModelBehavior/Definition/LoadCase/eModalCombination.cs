// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eModalCombination.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Modal combination methods available in the application for combining multiple modes in different directions.
    /// </summary>
    public enum eModalCombination
    {
        /// <summary>
        /// The Complete Quadratic Combination method.
        /// </summary>
        CQC = 1,

        /// <summary>
        /// Square-Root of the Sum-of-Squares method.
        /// </summary>
        SRSS = 2,

        /// <summary>
        /// The absolute value of the sums method.
        /// </summary>
        ABS = 3,

        /// <summary>
        /// The General(Gupta) Modal Combination method.
        /// </summary>
        GMC = 4,

        /// <summary>
        /// The Nuclear Regulatory Commision 10 percent method.
        /// </summary>
        NRC10Percent = 5,

        /// <summary>
        /// The double sum method.
        /// </summary>
        DoubleSum = 6
    }
}