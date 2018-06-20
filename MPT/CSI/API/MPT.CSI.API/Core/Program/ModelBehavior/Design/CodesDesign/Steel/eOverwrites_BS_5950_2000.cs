// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_BS_5950_2000.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="BS_5950_2000" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_BS_5950_20005
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,


        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 2,


        /// <summary>
        /// The deflection check type.
        /// </summary>
        DeflectionCheckType = 3,


        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 4,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 5,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 6,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 7,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 8,


        /// <summary>
        /// The deflection limit for dead load.
        /// </summary>
        DeflectionLimit_DeadLoad_Absolute = 9,


        /// <summary>
        /// The deflection limit for combined specified dead and live load.
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Absolute = 10,


        /// <summary>
        /// The deflection limit for live load.
        /// </summary>
        DeflectionLimit_LiveLoad_Absolute = 11,


        /// <summary>
        /// The deflection limit, total.
        /// </summary>
        DeflectionLimit_Total_Absolute = 12,


        /// <summary>
        /// The camber limit, total.
        /// </summary>
        CamberLimit_Total_Absolute = 13,


        /// <summary>
        /// The specified camber.
        /// </summary>
        SpecifiedCamber = 14,


        /// <summary>
        /// The net area/total area ratio.
        /// </summary>
        NetAreaToTotalAreaRatio = 15,


        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 16,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 17,

        /// <summary>
        /// The unbraced length ratio, minor and lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_MinorAndLateralTorsionalBuckling = 18,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_K_Major = 19,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_K_Minor = 20,

        /// <summary>
        /// The uniform moment factor, M major.
        /// </summary>
        UniformMomentFactor_M_Major = 21,

        /// <summary>
        /// The uniform moment factor, M minor.
        /// </summary>
        UniformMomentFactor_M_Minor = 22,

        /// <summary>
        /// The equivalent uniform moment factor, mLT.
        /// </summary>
        EquivalentUniformMomentFactor_mLT = 23,


        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 24,


        /// <summary>
        /// The compressive capacity, Pc.
        /// </summary>
        CompressiveCapacity_Pc = 25,


        /// <summary>
        /// The tensile capacity, Pt.
        /// </summary>
        TensileCapacity_Pt = 26,


        /// <summary>
        /// The major bending capacity, Mc3.
        /// </summary>
        MajorBendingCapacity_Mc3 = 27,


        /// <summary>
        /// The minor bending capacity, Mc2.
        /// </summary>
        MinorBendingCapacity_Mc2 = 28,


        /// <summary>
        /// The major shear capacity, Mb.
        /// </summary>
        BucklingResistanceMoment_Mb = 29,


        /// <summary>
        /// The major shear capacity, Pv2.
        /// </summary>
        MajorShearCapacity_Pv2 = 30,


        /// <summary>
        /// The minor shear capacity, Pv3.
        /// </summary>
        MinorShearCapacity_Pv3 = 31,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 32
    }
}
#endif