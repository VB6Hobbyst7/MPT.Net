// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AS_4100_1998.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="AS_4100_1998" /> steel design in the application.
    /// </summary>
    public enum ePreferences_AS_4100_1998
    {
        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 1,

        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 2,

        /// <summary>
        /// Structural analysis method.
        /// </summary>
        StructuralAnalysisMethod = 3,

        /// <summary>
        /// Steel type.
        /// </summary>
        SteelType = 4,

        /// <summary>
        /// Capacity factor Phi for bending.
        /// </summary>
        CapacityFactorPhiBending = 5,

        /// <summary>
        /// Capacity factor Phi for compression.
        /// </summary>
        CapacityFactorPhiCompression = 6,

        /// <summary>
        /// Capacity factor Phi for tension yielding.
        /// </summary>
        CapacityFactorPhiTensionYielding = 7,

        /// <summary>
        /// Capacity factor Phi for tension fracture.
        /// </summary>
        CapacityFactorPhiTensionFracture = 8,

        /// <summary>
        /// Capacity factor Phi for shear.
        /// </summary>
        CapacityFactorPhiShear = 9,

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

    }
}
#endif