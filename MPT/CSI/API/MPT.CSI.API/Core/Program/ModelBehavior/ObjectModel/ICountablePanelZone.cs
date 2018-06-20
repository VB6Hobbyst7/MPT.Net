// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="ICountablePanelZone.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has countable panel zones.
    /// </summary>
    public interface ICountablePanelZone
    {
        /// <summary>
        /// This function returns the total number of panel zone assignments to point objects in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int CountPanelZone();
    }
}