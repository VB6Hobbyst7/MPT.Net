// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eCtType_NBCC_1995.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The structure type for NBCC 1995 seismic lateral code forces..
    /// </summary>
    public enum eCtType_NBCC_1995
    {
        /// <summary>
        /// Moment frame structure.
        /// </summary>
        MomentFrame = 1,

        /// <summary>
        /// All other structures.
        /// </summary>
        Other = 2
    }
}
#endif