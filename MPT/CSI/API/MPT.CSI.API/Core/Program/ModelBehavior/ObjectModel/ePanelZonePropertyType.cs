// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="ePanelZonePropertyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Method by which properties are determined for panel zones.
    /// </summary>
    public enum ePanelZonePropertyType
    {
        /// <summary>
        /// Properties are elastic from column.
        /// </summary>
        ElasticFromColumn = 0,

        /// <summary>
        /// Properties are elastic from column and doubler plate.
        /// </summary>
        ElasticFromColumnAndDoublerPlate = 1,

        /// <summary>
        /// Properties are from specified spring stiffnesses.
        /// </summary>
        FromSpringStiffness = 2,

        /// <summary>
        /// Properties are from a specified link property.
        /// </summary>
        FromLink = 3
    }
}
