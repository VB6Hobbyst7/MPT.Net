// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-28-2017
// ***********************************************************************
// <copyright file="eHingeBehavior.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Hinge behavior available in the application.
    /// </summary>
    public enum eHingeBehavior
    {
        /// <summary>
        /// Force controlled.
        /// </summary>
        ForceControlled = 1,

        /// <summary>
        /// Deformation controlled.
        /// </summary>
        DeformationControlled = 2
    }
}
