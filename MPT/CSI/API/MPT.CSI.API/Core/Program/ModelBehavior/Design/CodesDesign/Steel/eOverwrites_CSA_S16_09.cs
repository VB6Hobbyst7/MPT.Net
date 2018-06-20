// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="eOverwrites_CSA_S16_09.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="CSA_S16_09" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_CSA_S16_09
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
        /// The unbraced length ratio, lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_LateralTorsionalBuckling = 19,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_K_Major = 20,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_K_Minor = 21,

        /// <summary>
        /// The effective length factor, K lateral-torsional buckling.
        /// </summary>
        EffectiveLengthFactor_K_LTB = 22,

        /// <summary>
        /// The moment coefficient, omega1 minor.
        /// </summary>
        MomentCoefficient_Omega1_Major = 23,

        /// <summary>
        /// The moment coefficient, omega1 minor.
        /// </summary>
        MomentCoefficient_Omega1_Minor = 24,

        /// <summary>
        /// The bending coefficient, omega2.
        /// </summary>
        BendingCoefficient_Omega2 = 25,

        /// <summary>
        /// The non-sway moment factor, U1 major.
        /// </summary>
        NonswayMomentFactor_U1_Major = 26,

        /// <summary>
        /// The non-sway moment factor, U1 minor.
        /// </summary>
        NonswayMomentFactor_U1_Minor = 27,

        /// <summary>
        /// The sway moment factor, U2 major.
        /// </summary>
        SwayMomentFactor_U2_Major = 28,

        /// <summary>
        /// The sway moment factor, U2 minor.
        /// </summary>
        SwayMomentFactor_U2_Minor = 29,

        /// <summary>
        /// Parameter for compressive resistance, n.
        /// </summary>
        ParamtersForCompressiveResistance_n = 30,

        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 31,

        /// <summary>
        /// Expected/Specified Fy ratio, Ry.
        /// </summary>
        ExpectedToSpecifiedFyRatio_Ry = 32,


        /// <summary>
        /// The compressive capacity, Prc.
        /// </summary>
        CompressiveResistance_Cr = 33,


        /// <summary>
        /// The tensile capacity, Prt.
        /// </summary>
        TensileResistance_Tr = 34,


        /// <summary>
        /// The major bending capacity, Mr3.
        /// </summary>
        MajorBendingResistance_Mr3 = 35,


        /// <summary>
        /// The minor bending capacity, Mr2.
        /// </summary>
        MinorBendingResistance_Mr2 = 36,


        /// <summary>
        /// The major shear capacity, Vr2.
        /// </summary>
        MajorShearResistance_Vr2 = 37,


        /// <summary>
        /// The minor shear capacity, Vr3.
        /// </summary>
        MinorShearResistance_Vr3 = 38,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 39
    }
}