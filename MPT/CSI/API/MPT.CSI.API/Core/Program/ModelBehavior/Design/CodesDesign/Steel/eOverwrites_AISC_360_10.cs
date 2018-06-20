// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_AISC_360_10.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="AISC_360_10" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_AISC_360_10
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// Omega0.
        /// </summary>
        Omega0 = 2,


        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 3,


        /// <summary>
        /// The deflection check type.
        /// </summary>
        DeflectionCheckType = 4,


        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 5,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 6,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 7,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 8,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 9,


        /// <summary>
        /// The deflection limit for dead load.
        /// </summary>
        DeflectionLimit_DeadLoad_Absolute = 10,


        /// <summary>
        /// The deflection limit for combined specified dead and live load.
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Absolute = 11,


        /// <summary>
        /// The deflection limit for live load.
        /// </summary>
        DeflectionLimit_LiveLoad_Absolute = 12,


        /// <summary>
        /// The deflection limit, total.
        /// </summary>
        DeflectionLimit_Total_Absolute = 13,


        /// <summary>
        /// The camber limit, total.
        /// </summary>
        CamberLimit_Total_Absolute = 14,


        /// <summary>
        /// The specified camber.
        /// </summary>
        SpecifiedCamber = 15,


        /// <summary>
        /// The net area/total area ratio.
        /// </summary>
        NetAreaToTotalAreaRatio = 16,


        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 17,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 18,

        /// <summary>
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatio_Minor = 19,

        /// <summary>
        /// The unbraced length ratio, lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_LateralTorsionalBuckling = 20,

        /// <summary>
        /// The effective length factor, K1 major.
        /// </summary>
        EffectiveLengthFactor_K1_Major = 21,

        /// <summary>
        /// The effective length factor, K1 minor.
        /// </summary>
        EffectiveLengthFactor_K1_Minor = 22,

        /// <summary>
        /// The effective length factor, K2 major.
        /// </summary>
        EffectiveLengthFactor_K2_Major = 23,

        /// <summary>
        /// The effective length factor, K2 minor.
        /// </summary>
        EffectiveLengthFactor_K2_Minor = 24,

        /// <summary>
        /// The effective length factor, K lateral-torsional buckling.
        /// </summary>
        EffectiveLengthFactor_K2_LateralTorsionalBuckling = 25,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_Cm_Major = 26,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_Cm_Minor = 27,

        /// <summary>
        /// The bending coefficient, Cb.
        /// </summary>
        BendingCoefficient_Cb = 28,

        /// <summary>
        /// The non-sway moment factor, B1 major.
        /// </summary>
        NonswayMomentFactor_B1_Major = 29,

        /// <summary>
        /// The non-sway moment factor, B1 minor.
        /// </summary>
        NonswayMomentFactor_B1_Minor = 30,

        /// <summary>
        /// The sway moment factor, B2 major.
        /// </summary>
        SwayMomentFactor_B2_Major = 31,

        /// <summary>
        /// The sway moment factor, B2 minor.
        /// </summary>
        SwayMomentFactor_B2_Minor = 32,


        /// <summary>
        /// Reduce HSS thickness?
        /// </summary>
        ReduceHSSThickness = 33,


        /// <summary>
        /// HSS welding type.
        /// </summary>
        HSSWeldingType = 34,


        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 35,

        /// <summary>
        /// Expected/Specified Fy ratio.
        /// </summary>
        ExpectedToSpecifiedFyRatio = 36,


        /// <summary>
        /// The compressive capacity, phi*Pnc.
        /// </summary>
        CompressiveCapacity_Phi_Pnc = 37,


        /// <summary>
        /// The tensile capacity, phi*Pnt.
        /// </summary>
        TensileCapacity_Phi_Pnt = 38,


        /// <summary>
        /// The major bending capacity, phi*Mn3.
        /// </summary>
        MajorBendingCapacity_Phi_Mn3 = 39,


        /// <summary>
        /// The minor bending capacity, phi*Mn2.
        /// </summary>
        MinorBendingCapacity_Phi_Mn2 = 40,


        /// <summary>
        /// The major shear capacity, phi*Vn2.
        /// </summary>
        MajorShearCapacity_Phi_Vn2 = 41,


        /// <summary>
        /// The minor shear capacity, phi*Vn3.
        /// </summary>
        MinorShearCapacity_Phi_Vn3 = 42,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 43
    }
}
#endif