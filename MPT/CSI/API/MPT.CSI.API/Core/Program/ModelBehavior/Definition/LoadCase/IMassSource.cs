// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-19-2017
// ***********************************************************************
// <copyright file="IMassSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case can use mass source.
    /// </summary>
    public interface IMassSource
    {
        /// <summary>
        /// This function retrieves the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        void GetMassSource(string name,
            ref string sourceName);

        /// <summary>
        /// This function sets the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        void SetMassSource(string name,
            string sourceName);
    }
}