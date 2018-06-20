// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.ComponentModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad
{
    /// <summary>
    /// List source to use for auto load method or values.
    /// </summary>
    public enum eSource
    {
        /// <summary>
        /// Per code.
        /// </summary>
        [Description("Per Code")]
        PerCode = 1,

        /// <summary>
        /// User defined.
        /// </summary>
        [Description("User Defined")]
        UserDefined = 2
    }
}
#endif
