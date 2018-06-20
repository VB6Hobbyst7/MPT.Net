// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eCableGeometryDefinition.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Cable definition used in the program for defining cable geometry.
    /// </summary>
    public enum eCableGeometryDefinition
    {


        /// <summary>
        /// Minimum tension at I-End.
        /// </summary>
        MinTensionIEnd = 1,

        /// <summary>
        /// Minimum tension at J-End.
        /// </summary>
        MinTensionJEnd = 2,

        /// <summary>
        /// Tension at I-End.
        /// </summary>
        TensionIEnd = 3,

        /// <summary>
        /// Tension at J-End.
        /// </summary>
        TensionJEnd = 4,

        /// <summary>
        /// Horizontal tension component.
        /// </summary>
        HorizontalTensionComponent = 5,

        /// <summary>
        /// Maximum vertical sag.
        /// </summary>
        MaximumVerticalSag = 6,

        /// <summary>
        /// Low-point vertical sag.
        /// </summary>
        LowPointVerticalSag = 7,

        /// <summary>
        /// Undeformed length.
        /// </summary>
        UndeformedLength = 8,

        /// <summary>
        /// Relative undeformed length.
        /// </summary>
        RelativeUndeformedLength = 9
    }
}

#endif