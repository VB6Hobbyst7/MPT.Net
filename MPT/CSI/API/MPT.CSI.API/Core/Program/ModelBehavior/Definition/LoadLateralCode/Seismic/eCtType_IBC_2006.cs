// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="eCtType_IBC_2006.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic
{
    /// <summary>
    /// The values of Ct and x for IBC 2006 seismic lateral code forces.
    /// </summary>
    public enum eCtType_IBC_2006
    {
        /// <summary>
        /// Ct = 0.028(ft), x = 0.8.
        /// </summary>
        Ct0_028_x_0_8 = 0,

        /// <summary>
        /// Ct = 0.016(ft), x = 0.9.
        /// </summary>
        Ct0_016_x_0_9 = 1,

        /// <summary>
        /// Ct = 0.03(ft), x = 0.75.
        /// </summary>
        Ct0_03_x_0_75 = 2,

        /// <summary>
        /// Ct = 0.02(ft), x = 0.75.
        /// </summary>
        Ct0_02_x_0_75 = 3,
    }
}