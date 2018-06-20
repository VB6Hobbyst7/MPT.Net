// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="ePanelZoneLocalAxis.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Method by which the local axis is defined.
    /// </summary>
    public enum ePanelZoneLocalAxis
    {
        /// <summary>
        /// Panel zone local axis angle is from column.
        /// </summary>
        FromColumn = 0,

        /// <summary>
        /// Panel zone local axis angle is user defined.
        /// </summary>
        UserDefined = 1
    }
}
