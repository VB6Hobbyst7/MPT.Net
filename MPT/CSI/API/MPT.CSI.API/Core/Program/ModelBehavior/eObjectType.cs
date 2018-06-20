// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eObjectType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object types available in the application.
    /// </summary>
    public enum eObjectType
    {
        /// <summary>
        /// Point Object.
        /// </summary>
        [Description("Point Object")]
        PointObject = 1,

        /// <summary>
        /// Frame Object.
        /// </summary>
        [Description("Frame Object")]
        FrameObject = 2,

        /// <summary>
        /// Cable Object.
        /// </summary>
        [Description("Cable Object")]
        CableObject = 3,

        /// <summary>
        /// Tendon Object.
        /// </summary>
        [Description("Tendon Object")]
        TendonObject = 4,

        /// <summary>
        /// Area Object.
        /// </summary>
        [Description("Area Object")]
        AreaObject = 5,

        /// <summary>
        /// Solid bject.
        /// </summary>
        [Description("Solid Object")]
        SolidObject = 6,

        /// <summary>
        /// Link Object.
        /// </summary>
        [Description("Link Object")]
        LinkObject = 7
    }
}
