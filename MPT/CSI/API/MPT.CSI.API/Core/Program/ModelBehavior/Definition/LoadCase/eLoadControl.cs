// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eLoadControl.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load control types available in the application.
    /// </summary>
    public enum eLoadControl
    {
        /// <summary>
        /// Full load.
        /// </summary>
        FullLoad = 1,

        /// <summary>
        /// Displacement control.
        /// </summary>
        DisplacementControl = 2
    }
}