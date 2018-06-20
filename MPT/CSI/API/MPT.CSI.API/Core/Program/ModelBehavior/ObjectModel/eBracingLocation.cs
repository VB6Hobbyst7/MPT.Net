// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-24-2017
// ***********************************************************************
// <copyright file="eBracingLocation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Bracing locations available in the cross section of frames in the application.
    /// </summary>
    public enum eBracingLocation
    {
        /// <summary>
        /// Bracing on the top face/flange only.
        /// </summary>
        Top = 1,

        /// <summary>
        /// Bracing on the botton face/flange only.
        /// </summary>
        Bottom = 2,

        /// <summary>
        /// Bracing on the top and bottom faces/flanges.
        /// </summary>
        All = 3
    }
}