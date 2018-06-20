// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="eConfinementType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Confinement rebar types available in the application.
    /// </summary>
    public enum eConfinementType
    {
        /// <summary>
        /// Tie confinement rebar configuration.
        /// </summary>
        Ties = 1,

        /// <summary>
        /// Spiral confinement rebar configuration.
        /// (Spiral only possible when <see cref="eRebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />)
        /// </summary>
        Spiral = 2
    }
}