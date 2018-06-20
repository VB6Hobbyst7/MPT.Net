// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-12-2017
// ***********************************************************************
// <copyright file="eMaterialSymmetryType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Material symmetry types available in the application.
    /// </summary>
    public enum eMaterialSymmetryType
    {
        /// <summary>
        /// Isotropic symmetry.
        /// </summary>
        Isotropic = 0,

        /// <summary>
        /// Orthotropic symmetry.
        /// </summary>
        Orthotropic = 1,

        /// <summary>
        /// Anisotropic symmetry.
        /// </summary>
        Anisotropic = 2,

        /// <summary>
        /// Uniaxial symmetry.
        /// </summary>
        Uniaxial = 3
            
    }
}
