// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="ePreferences_Eurocode_3_2005.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="Eurocode_3_2005" /> steel design in the application.
    /// </summary>
    public enum ePreferences_Eurocode_3_2005
    {
        /// <summary>
        /// Country specific implementation considering country National Annex.
        /// CEN Default is the general version without an annex..
        /// </summary>
        Country = 1,

        /// <summary>
        /// Design code combinations to consider.
        /// These are either generated from Eq. 6.10 or from both Eqs. 6.10a and 6.10b..
        /// </summary>
        CombosEquation = 2,

        /// <summary>
        /// The K factor method.
        /// </summary>
        KFactorMethod = 3,

        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 4,

        /// <summary>
        /// GammaM0.
        /// </summary>
        GammaM0 = 5,


        /// <summary>
        /// GammaM1.
        /// </summary>
        GammaM1 = 6,

        /// <summary>
        /// GammaM2.
        /// </summary>
        GammaM2 = 7,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 8,

        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 9,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 10,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 11,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 12,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 13,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 14,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 15,

        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 16,

        /// <summary>
        /// Reliability class.
        /// </summary>
        ReliabilityClass = 17,
    }
}