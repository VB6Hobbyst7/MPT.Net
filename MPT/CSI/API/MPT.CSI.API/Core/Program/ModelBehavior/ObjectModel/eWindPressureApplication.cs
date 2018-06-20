// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-01-2017
// ***********************************************************************
// <copyright file="eWindPressureApplication.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Application of wind pressure to objects in the application.
    /// </summary>
    public enum eWindPressureApplication
    {
        /// <summary>
        /// Windward, pressure varies over height.
        /// </summary>
        Windward = 1,

        /// <summary>
        /// Other, pressure is constant over height.
        /// </summary>
        Other = 2
    }
}
