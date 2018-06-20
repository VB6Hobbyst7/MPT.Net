// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eMotionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Time history motion types available in the application.
    /// </summary>
    public enum eMotionType
    {
        /// <summary>
        /// Transient motion type.
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Periodic motion type.
        /// </summary>
        Periodic = 2
    }
}
#endif