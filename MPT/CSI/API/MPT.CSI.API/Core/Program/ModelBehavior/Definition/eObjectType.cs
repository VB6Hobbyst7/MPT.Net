// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="eObjectType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Objects avilable for manipulation in staged construction load cases in the application.
    /// </summary>
    public enum eObjectType
    {
        /// <summary>
        /// Point Object.
        /// </summary>
        [Description("Point")]
        Point = 1,

        /// <summary>
        /// Frame Object.
        /// </summary>
        [Description("Frame")]
        Frame = 2,


        /// <summary>
        /// Cable Object.
        /// </summary>
        [Description("Cable")]
        Cable = 3,

        /// <summary>
        /// Tendon Object.
        /// </summary>
        [Description("Tendon")]
        Tendon = 4,

        /// <summary>
        /// Area Object.
        /// </summary>
        [Description("Area")]
        Area = 5,

        /// <summary>
        /// Solid bject.
        /// </summary>
        [Description("Solid")]
        Solid = 6,

        /// <summary>
        /// Link Object.
        /// </summary>
        [Description("Link")]
        Link = 7,

        /// <summary>
        /// Group Object.
        /// </summary>
        [Description("Group")]
        Group = 8
    }
}