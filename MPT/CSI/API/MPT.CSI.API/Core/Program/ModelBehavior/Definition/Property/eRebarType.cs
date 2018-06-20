// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eRebarType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Rebar type used in a concrete frame.
    /// This determines the rebar detailing available, and whether a frame is treated as a beam or a column.
    /// </summary>
    public enum eRebarType
    {
        /// <summary>
        /// None specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Column design.
        /// </summary>
        Column = 1,

        /// <summary>
        /// Beam design.
        /// </summary>
        Beam = 2
    }
}
