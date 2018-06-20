// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="eDampingTypeProportional.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Proportional damping types available in the application.
    /// </summary>
    public enum eDampingTypeProportional
    {
        /// <summary>
        /// Mass and stiffness proportional damping by direct specification.
        /// </summary>
        ProportionalBySpecification = 1,

        /// <summary>
        /// Mass and stiffness proportional damping by period.
        /// </summary>
        ProportionalByPeriod = 2,

        /// <summary>
        /// Mass and stiffness proportional damping by frequency.
        /// </summary>
        ProportionalByFrequency = 3
    }
}