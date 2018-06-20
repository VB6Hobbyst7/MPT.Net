// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="eTendonStressStrainCurveType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Stress-strain curve types available for tendons in the application.
    /// </summary>
    public enum eTendonStressStrainCurveType
    {
        /// <summary>
        /// User defined.
        /// </summary>
        UserDefined = 0,

        /// <summary>
        /// Parametric – 250 ksi strand.
        /// </summary>
        Parametric250ksiStrand = 1,

        /// <summary>
        /// Parametric – 270 ksi strand.
        /// </summary>
        Parametric270ksiStrand = 2
    }
}