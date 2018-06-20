// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_NZS_3404_1997.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="NZS_3404_1997" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_NZS_3404_1997
    {

        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The steel type.
        /// </summary>
        SteelType = 2,

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
        /// The effective length factor, braced, Ke major.
        /// </summary>
        EffectiveLengthFactor_Braced_Ke_Major = 21,

        /// <summary>
        /// The effective length factor, braced, Ke minor.
        /// </summary>
        EffectiveLengthFactor_Braced_Ke_Minor = 22,

        /// <summary>
        /// The effective length factor, sway, Ke major.
        /// </summary>
        EffectiveLengthFactor_Sway_Ke_Major = 23,

        /// <summary>
        /// The effective length factor, sway, Ke minor.
        /// </summary>
        EffectiveLengthFactor_Sway_Ke_Minor = 24,

        /// <summary>
        /// Twist restraint factor for lateral-torsional buckling, kt.
        /// </summary>
        TwistRestraintFactorForLTB_kt = 25,

        /// <summary>
        /// Lateral-rotational restraing factor, kr.
        /// </summary>
        LateralRotationalRestraintFactor_kr = 26,

        /// <summary>
        /// Load height factor for lateral-torsional buckling, kl.
        /// </summary>
        LoadHeightFactorForLTB_kl = 27,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_Cm_Major = 28,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_Cm_Minor = 29,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentModificationFactor_AlphaM = 30,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        SlenderReductionFactor_AlphaS = 31,

        /// <summary>
        /// The non-sway moment factor, Db major.
        /// </summary>
        NonswayMomentFactor_Db_Major = 32,

        /// <summary>
        /// The non-sway moment factor, Db minor.
        /// </summary>
        NonswayMomentFactor_Db_Minor = 33,

        /// <summary>
        /// The sway moment factor, Ds major.
        /// </summary>
        SwayMomentFactor_Ds_Major = 34,

        /// <summary>
        /// The sway moment factor, Ds minor.
        /// </summary>
        SwayMomentFactor_Ds_Minor = 35,

        /// <summary>
        /// Form factor, Kf.
        /// </summary>
        FormFactor_Kf = 36,

        /// <summary>
        /// Axial capacity correction factor, Kt.
        /// </summary>
        AxialCapacityCorrectionFactor_Kt = 37,


        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 38,


        /// <summary>
        /// The compressive capacity, Nc.
        /// </summary>
        CompressiveCapacity_Nc = 39,


        /// <summary>
        /// The tensile capacity, Nt.
        /// </summary>
        TensileCapacity_Nt = 40,


        /// <summary>
        /// The major bending capacity, Ms33.
        /// </summary>
        MajorBendingCapacity_Ms33 = 41,


        /// <summary>
        /// The minor bending capacity, Ms22.
        /// </summary>
        MinorBendingCapacity_Ms22 = 42,


        /// <summary>
        /// The minor bending capacity, Mb33.
        /// </summary>
        MinorBendingCapacity_Mb33 = 43,


        /// <summary>
        /// The major shear capacity, Vu2.
        /// </summary>
        MajorShearCapacity_Vu2 = 44,


        /// <summary>
        /// The minor shear capacity, Vu3.
        /// </summary>
        MinorShearCapacity_Vu3 = 45,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 46
    }
}
#endif