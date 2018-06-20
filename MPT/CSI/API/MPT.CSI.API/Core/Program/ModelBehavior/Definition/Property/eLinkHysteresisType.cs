// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="eLinkHysteresisType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Hysteresis types available for links in the application.
    /// </summary>
    public enum eLinkHysteresisType
    {
        /// <summary>
        /// Not applicable.
        /// </summary>
        NA = 0,

        /// <summary>
        /// Kinematic hysteresis type.
        /// </summary>
        Kinematic = 1,

        /// <summary>
        /// Takeda hysteresis type.
        /// </summary>
        Takeda = 2,

        /// <summary>
        /// Pivot hysteresis type.
        /// </summary>
        Pivot = 3
    }
}