// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eLoadControlDisplacement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Displacement load control types available in the application.
    /// </summary>
    public enum eLoadControlDisplacement
    {
        /// <summary>
        /// Conjugate displacement.
        /// </summary>
        Conjugate = 1,

        /// <summary>
        /// Monitored displacement.
        /// </summary>
        Monitored = 2
    }
}