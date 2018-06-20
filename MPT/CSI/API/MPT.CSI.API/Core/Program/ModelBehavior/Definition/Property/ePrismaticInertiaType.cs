// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-17-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-17-2017
// ***********************************************************************
// <copyright file="ePrismaticInertiaType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Variations in EI of prismatic sections available in the application.
    /// </summary>
    public enum ePrismaticInertiaType
    {
        /// <summary>
        /// EI varies linearly along the segment length.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// EI varies parabolically along the segment length.
        /// </summary>
        Parabolic = 2,

        /// <summary>
        /// EI varies cubically along the segment length.
        /// </summary>
        Cubic = 3
    }
}