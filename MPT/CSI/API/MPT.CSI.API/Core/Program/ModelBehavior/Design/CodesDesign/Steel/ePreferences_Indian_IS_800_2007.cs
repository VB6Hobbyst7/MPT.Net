// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_Indian_IS_800_2007.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="Indian_IS_800_2007" /> steel design in the application.
    /// </summary>
    public enum ePreferences_Indian_IS_800_2007
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The importance factor.
        /// </summary>
        ImportanceFactor = 2,

        /// <summary>
        /// The seismic zone.
        /// </summary>
        SeismicZone = 3,

        /// <summary>
        /// Consider P-delta done?
        /// </summary>
        ConsiderPDeltaDone = 4,

        /// <summary>
        /// GammaM0.
        /// </summary>
        GammaM0 = 5,


        /// <summary>
        /// GammaM1.
        /// </summary>
        GammaM1 = 6,

        /// <summary>
        /// Ignore seismic code?
        /// </summary>
        IgnoreSeismicCode = 7,

        /// <summary>
        /// Ignore special seismic load?
        /// </summary>
        IgnoreSpecialSeismicLoad = 8,

        /// <summary>
        /// Is doubler plate plug welded?
        /// </summary>
        DoublerPlateIsPlugWelded = 9,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 10,

        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 11,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 12,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 13,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 14,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 15,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 16,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 17,

        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 18,
    }
}
#endif