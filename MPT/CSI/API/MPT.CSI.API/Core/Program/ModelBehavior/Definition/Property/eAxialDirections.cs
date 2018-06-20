// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-05-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="eAxialDirections.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Axial directions available in the application.
    /// </summary>
    public enum eAxialDirections
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
    }
}
#endif