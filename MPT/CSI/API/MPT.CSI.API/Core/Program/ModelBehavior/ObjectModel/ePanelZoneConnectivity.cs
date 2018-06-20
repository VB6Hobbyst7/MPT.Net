// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="ePanelZoneConnectivity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Panel zone connection types.
    /// </summary>
    public enum ePanelZoneConnectivity
    {
        /// <summary>
        /// Panel zone connects beams to other objects.
        /// </summary>
        ConnectsBeamsToObjects = 0,

        /// <summary>
        /// Panel zone connects braces to other obj.
        /// </summary>
        ConnectsBracesToObjects = 1
    }
}
