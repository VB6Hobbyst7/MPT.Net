// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="IGeometricNonlinearity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case uses geometric nonlinearity.
    /// </summary>
    public interface IGeometricNonlinearity
    {
        /// <summary>
        /// This function retrieves the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        void GetGeometricNonlinearity(string name,
            ref eGeometricNonlinearity geometricNonlinearityType);

        /// <summary>
        /// This function sets the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        void SetGeometricNonlinearity(string name,
            eGeometricNonlinearity geometricNonlinearityType);

    }
}