// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-12-2017
// ***********************************************************************
// <copyright file="eConstraintAxis.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Constraint axes available in the application.
    /// </summary>
    public enum eConstraintAxis
    {
        /// <summary>
        /// X-axis constraint.
        /// </summary>
        X = 1,

        /// <summary>
        /// Y-axis constraint.
        /// </summary>
        Y = 2,

        /// <summary>
        /// Z-axis constraint.
        /// </summary>
        Z = 3,

        /// <summary>
        /// Auto-axis constraint.
        /// Axis of the constraint is automatically determined from the joints assigned to the constraint.
        /// </summary>
        AutoAxis = 4
    }
}
