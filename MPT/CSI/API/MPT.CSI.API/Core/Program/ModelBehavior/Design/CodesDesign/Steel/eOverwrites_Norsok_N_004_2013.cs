// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_Norsok_N_004_2013.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="Norsok_N_004_2013" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_Norsok_N_004_2013
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
        /// The moment coefficient, k major.
        /// </summary>
        MomentCoefficient_k_Major = 21,

        /// <summary>
        /// The moment coefficient, k minor.
        /// </summary>
        MomentCoefficient_k_Minor = 22,

        /// <summary>
        /// The bending coefficient, C1.
        /// </summary>
        BendingCoefficient_C1 = 23,

        /// <summary>
        /// The moment coefficient, kzt.
        /// </summary>
        MomentCoefficient_kzy = 24,

        /// <summary>
        /// The moment coefficient, kyz.
        /// </summary>
        MomentCoefficient_kyz = 25,


        /// <summary>
        /// The equalized pressure.
        /// </summary>
        PressureEqualized = 26,


        /// <summary>
        /// The external pressure.
        /// </summary>
        ExternalPressure = 27,


        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 28,


        /// <summary>
        /// The compressive capacity, NcRd.
        /// </summary>
        CompressiveCapacity_NcRd = 29,


        /// <summary>
        /// The tensile capacity, NtRd.
        /// </summary>
        TensileCapacity_NtRd = 30,


        /// <summary>
        /// The major bending capacity, Mc3Rd.
        /// </summary>
        MajorBendingCapacity_Mc3Rd = 31,


        /// <summary>
        /// The minor bending capacity, Mc2Rd.
        /// </summary>
        MinorBendingCapacity_Mc2Rd = 32,


        /// <summary>
        /// The major shear capacity, MbRd.
        /// </summary>
        BucklingResistanceMoment_MbRd = 33,


        /// <summary>
        /// The major shear capacity, V2Rd.
        /// </summary>
        MajorShearCapacity_V2Rd = 34,


        /// <summary>
        /// The minor shear capacity, V3Rd.
        /// </summary>
        MinorShearCapacity_V3Rd = 35,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 36
    }
}
#endif