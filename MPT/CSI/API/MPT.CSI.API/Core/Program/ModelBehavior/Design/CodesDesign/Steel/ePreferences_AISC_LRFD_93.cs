// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AISC_LRFD_93.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="AISC_LRFD_93" /> steel design in the application.
    /// </summary>
    public enum ePreferences_AISC_LRFD_93
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// Capacity factor Phi for bending.
        /// </summary>
        CapacityFactorPhiBending = 2,

        /// <summary>
        /// Capacity factor Phi for compression.
        /// </summary>
        CapacityFactorPhiCompression = 3,

        /// <summary>
        /// Capacity factor Phi for tension.
        /// </summary>
        CapacityFactorPhiTension = 4,

        /// <summary>
        /// Capacity factor Phi for shear.
        /// </summary>
        CapacityFactorPhiShear = 5,

        /// <summary>
        /// Capacity factor Phi for compression in angle sections.
        /// </summary>
        CapacityFactorPhiCompression_Angle = 6,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 7,

        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 8,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 9,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 10,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 11,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 12,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 13,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 14,

        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 15,
    }
}
#endif