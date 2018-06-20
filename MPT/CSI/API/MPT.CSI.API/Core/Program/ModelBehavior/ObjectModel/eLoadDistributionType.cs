// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-02-2017
// ***********************************************************************
// <copyright file="eLoadDistributionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Load distribution types available for uniform loads in the application.
    /// Indicates how the load is tranferred to element edges.
    /// </summary>
    public enum eLoadDistributionType
    {
        /// <summary>
        /// One-way load distribution.
        /// This is parallel to the area object local 1 axis
        /// </summary>
        OneWay = 1,

        /// <summary>
        /// Two-way load distribution.
        /// This is parallel to the area object local 1 and 2 axes.
        /// </summary>
        TwoWay = 2
    }
}
