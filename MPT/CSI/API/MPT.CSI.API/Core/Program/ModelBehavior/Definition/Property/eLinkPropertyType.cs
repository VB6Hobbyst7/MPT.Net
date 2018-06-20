// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eLinkPropertyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Link types available in the application.
    /// </summary>
    public enum eLinkPropertyType
    {
        /// <summary>
        /// Linear link.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Damper link.
        /// </summary>
        Damper = 2,

        /// <summary>
        /// Gap link.
        /// </summary>
        Gap = 3,

        /// <summary>
        /// Hook link.
        /// </summary>
        Hook = 4,

        /// <summary>
        /// Plastic Wen link.
        /// </summary>
        PlasticWen = 5,

        /// <summary>
        /// Rubber isolator link.
        /// </summary>
        IsolatorRubber = 6,

        /// <summary>
        /// Friction isolator link.
        /// </summary>
        IsolatorFriction = 7,

        /// <summary>
        /// Multi linear elastic link.
        /// </summary>
        MultiLinearElastic = 8,

        /// <summary>
        /// Multi linear plastic link.
        /// </summary>
        MultiLinearPlastic = 9,

        /// <summary>
        /// Tension/Compression friction isolator link.
        /// </summary>
        IsolatorTCFriction = 10
    }
}
