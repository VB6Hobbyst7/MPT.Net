// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eTendonJack.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// How the tendon is jacked.
    /// </summary>
    public enum eTendonJack
    {
        /// <summary>
        /// Tendon jacked from I-End.
        /// </summary>
        FromIEnd = 1,

        /// <summary>
        /// Tendon jacked from J-E.
        /// </summary>
        FromJEnd = 2,

        /// <summary>
        /// Tendon jacked from both ends.
        /// </summary>
        BothEnds = 3
    }
}
#endif