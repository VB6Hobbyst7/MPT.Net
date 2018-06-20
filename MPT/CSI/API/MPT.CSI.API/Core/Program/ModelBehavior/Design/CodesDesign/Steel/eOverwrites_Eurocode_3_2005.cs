// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="eOverwrites_Eurocode_3_2005.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="Eurocode_3_2005" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_Eurocode_3_2005
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
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatio_Minor = 18,

        /// <summary>
        /// The effective length factor, sway, K2 major.
        /// </summary>
        EffectiveLengthFactor_Sway_K2_Major = 19,

        /// <summary>
        /// The effective length factor, sway, K2 minor.
        /// </summary>
        EffectiveLengthFactor_Sway_K2_Minor = 20,

        /// <summary>
        /// The moment coefficient, kyy major.
        /// </summary>
        MomentCoefficient_kyy_Major = 21,

        /// <summary>
        /// The moment coefficient, kzz minor.
        /// </summary>
        MomentCoefficient_kzz_Minor = 22,

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
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 26,


        /// <summary>
        /// The compressive capacity, NcRd.
        /// </summary>
        CompressiveCapacity_NcRd = 27,


        /// <summary>
        /// The tensile capacity, NtRd.
        /// </summary>
        TensileCapacity_NtRd = 28,


        /// <summary>
        /// The major bending capacity, Mc3Rd.
        /// </summary>
        MajorBendingCapacity_Mc3Rd = 29,


        /// <summary>
        /// The minor bending capacity, Mc2Rd.
        /// </summary>
        MinorBendingCapacity_Mc2Rd = 30,


        /// <summary>
        /// The major shear capacity, MbRd.
        /// </summary>
        BucklingResistanceMoment_MbRd = 31,


        /// <summary>
        /// The major shear capacity, V2Rd.
        /// </summary>
        MajorShearCapacity_V2Rd = 32,


        /// <summary>
        /// The minor shear capacity, V3Rd.
        /// </summary>
        MinorShearCapacity_V3Rd = 33,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 34,

        /// <summary>
        /// Section class.
        /// </summary>
        SectionClass = 35,

        /// <summary>
        /// Column bucking curve, y-y.
        /// </summary>
        ColumnBucklingCurveYY = 36,

        /// <summary>
        /// Column bucking curve, z-z.
        /// </summary>
        ColumnBucklingCurveZZ = 37,

        /// <summary>
        /// Buckling curve for lateral-torsional buckling.
        /// </summary>
        BucklingCurveForLTB = 38,

        /// <summary>
        /// System overstrength factor, omega.
        /// </summary>
        SystemOverStrengthFactor_Omega = 39,

        /// <summary>
        /// Is the section rolled?
        /// </summary>
        IsRolledSection = 40,

        /// <summary>
        /// The unbraced length ratio, lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_LTB = 41,

        /// <summary>
        /// Effective length factor, braced, K1 major.
        /// </summary>
        EffectiveLengthFactor_Braced_K1_Major = 42,

        /// <summary>
        /// Effective length factor, braced, K1 minor.
        /// </summary>
        EffectiveLengthFactor_Braced_K1_Minor = 43,

        /// <summary>
        /// Effective length factor, lateral-torsional buckling, KLTB.
        /// </summary>
        EffectiveLengthFactor_K_LTB = 44,

        /// <summary>
        /// Material overstrength factor, GammaOV.
        /// </summary>
        MaterialOverstrengthFactor_GammaOv = 45,

        /// <summary>
        /// The warping constant, Iw.
        /// </summary>
        WarpingConstant_Iw = 46,

        /// <summary>
        /// Elastic torsional buckling force, NcrT.
        /// </summary>
        ElasticTorsionalBuckingForce_NcrT = 47,

        /// <summary>
        /// Elastic torsional-flexural buckling force, NcrTF.
        /// </summary>
        ElasticTorsionalFlexuralBucklingForce_NcrTF = 48,
    }
}