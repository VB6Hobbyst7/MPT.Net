// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_ASCE_10_97.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="ASCE_10_97" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_ASCE_10_97
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
        /// The unbraced length ratio, minor &amp; lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_Minor_LateralTorsionalBuckling = 18,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_K_Major = 19,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_K_Minor = 20,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_Cm_Major = 21,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_Cm_Minor = 22,

        /// <summary>
        /// The bending coefficient, Cb.
        /// </summary>
        BendingCoefficient_Cb = 23,

        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 24,


        /// <summary>
        /// The compressive capacity, Pac.
        /// </summary>
        CompressiveCapacity_Pac = 25,


        /// <summary>
        /// The tensile capacity, Pat.
        /// </summary>
        TensileCapacity_Pat = 26,


        /// <summary>
        /// The major bending capacity, Ma3.
        /// </summary>
        MajorBendingCapacity_Ma3 = 27,


        /// <summary>
        /// The minor bending capacity, Ma2.
        /// </summary>
        MinorBendingCapacity_Ma2 = 28,


        /// <summary>
        /// The major shear stress, Fv2.
        /// </summary>
        MajorShearStress_Phi_Fv2 = 29,


        /// <summary>
        /// The minor shear stress, Fv3.
        /// </summary>
        MinorShearStress_Fv3 = 30,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 31
    }
}
#endif