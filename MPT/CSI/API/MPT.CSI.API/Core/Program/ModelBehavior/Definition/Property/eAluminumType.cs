// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eAluminumType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Types of aluminum.
    /// </summary>
    public enum eAluminumType
    {
        /// <summary>
        /// Wrought aluminum.
        /// </summary>
        Wrought = 1,

        /// <summary>
        /// Cast-mold aluminum.
        /// </summary>
        CastMold = 2,

        /// <summary>
        /// Cast-sand aluminum.
        /// </summary>
        CastSand = 3
    }
}
#endif