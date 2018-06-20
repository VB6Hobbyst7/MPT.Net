// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eLoadType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// The type of load assignable in the application.
    /// </summary>
    public enum eLoadType
    {
        /// <summary>
        /// Load, (i.e. force).
        /// </summary>
        Load = 1,

        /// <summary>
        /// Acceleration.
        /// </summary>
        Accel = 2
    }
}
#endif