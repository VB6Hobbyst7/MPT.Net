// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-15-2017
// ***********************************************************************
// <copyright file="eLoadTemperatureType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Temperature types available in the application.
    /// </summary>
    public enum eLoadTemperatureType
    {
        /// <summary>
        /// Uniform temperature load.
        /// </summary>
        Temperature = 1,

        /// <summary>
        /// Temperature gradient load along the local 2 axis.
        /// </summary>
        TemperatureGradient2Axis = 2,

        /// <summary>
        /// Temperature gradient load along the local 3 axis.
        /// </summary>
        TemperatureGradient3Axis = 3
    }
}
