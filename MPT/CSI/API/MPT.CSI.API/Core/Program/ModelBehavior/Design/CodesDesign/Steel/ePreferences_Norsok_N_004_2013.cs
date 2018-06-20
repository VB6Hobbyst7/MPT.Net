// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_Norsok_N_004_2013.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="Norsok_N_004_2013" /> steel design in the application.
    /// </summary>
    public enum ePreferences_Norsok_N_004_2013
    {
        /// <summary>
        /// Design code combinations to consider.
        /// These are either generated from Eq. 6.10 or from both Eqs. 6.10a and 6.10b..
        /// </summary>
        CombosEquation = 1,

        /// <summary>
        /// The K factor method.
        /// </summary>
        KFactorMethod = 2,

        /// <summary>
        /// Pressure interaction method.
        /// </summary>
        PressureInteractionMethod = 3,

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
    }
}
#endif