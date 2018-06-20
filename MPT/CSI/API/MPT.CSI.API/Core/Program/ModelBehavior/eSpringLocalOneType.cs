// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eSpringLocalOneType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Methods used to specify the spring positive local 1-axis orientation in the application.
    /// </summary>
    public enum eSpringLocalOneType
    {
        /// <summary>
        /// Parallel to object local axis.
        /// </summary>
        Parallel = 1,


        /// <summary>
        /// Normal to specified object face.
        /// </summary>
        Normal = 2,

        /// <summary>
        /// User specified direction vector.
        /// </summary>
        User = 3
    }
}
#endif