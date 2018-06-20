// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="eOutputStationType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Output station types available in the application.
    /// </summary>
    public enum eOutputStationType
    {
        /// <summary>
        /// Maximum segment size, that is, maximum station spacing.
        /// </summary>
        MaxSpacing = 1,

        /// <summary>
        /// Minimum number of stations.
        /// </summary>
        MinStations = 2
    }
}
