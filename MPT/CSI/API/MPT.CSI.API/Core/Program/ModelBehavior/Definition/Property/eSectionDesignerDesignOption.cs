// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-17-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-17-2017
// ***********************************************************************
// <copyright file="eSectionDesignerDesignOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Deign options available for section designer sections in the application.
    /// </summary>
    public enum eSectionDesignerDesignOption
    {
        /// <summary>
        /// No design.
        /// </summary>
        NoDesign = 0,

        /// <summary>
        /// Design as general steel section.
        /// </summary>
        GeneralSteel = 1,

        /// <summary>
        /// Design as a concrete column; check the reinforcing.
        /// </summary>
        ConcreteColumnCheck = 2,

        /// <summary>
        /// Design as a concrete column; design the reinforcing.
        /// </summary>
        ConcreteColumnDesign = 3
    }
}