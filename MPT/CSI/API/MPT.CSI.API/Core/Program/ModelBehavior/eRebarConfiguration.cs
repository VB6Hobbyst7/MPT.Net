// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="eRebarConfiguration.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Configuration patterns available for rebar in the application.
    /// </summary>
    public enum eRebarConfiguration
    {
        /// <summary>
        /// Rectangular pattern of reinforcement.
        /// </summary>
        Rectangular = 1,

        /// <summary>
        /// Circular pattern of reinforcement.
        /// </summary>
        Circular = 2
    }
}