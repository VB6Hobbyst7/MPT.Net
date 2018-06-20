// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eSpringType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Spring property types available in the application.
    /// </summary>
    public enum eSpringType
    {
        /// <summary>
        /// Simple spring.
        /// </summary>
        Simple = 1,

        /// <summary>
        /// Link property.
        /// </summary>
        Link = 2
    }
}
#endif