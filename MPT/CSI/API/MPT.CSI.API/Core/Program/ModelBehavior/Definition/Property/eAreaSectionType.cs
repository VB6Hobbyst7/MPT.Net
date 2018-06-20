// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eAreaSectionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Types of areas available in the program.
    /// </summary>
    public enum eAreaSectionType
    {
        /// <summary>
        /// All area types.
        /// </summary>
        All = 0,

        /// <summary>
        /// Shell.
        /// </summary>
        Shell = 1,

        /// <summary>
        /// Plane.
        /// </summary>
        Plane = 2,

        /// <summary>
        /// ASolid.
        /// </summary>
        ASolid = 3
    }
}