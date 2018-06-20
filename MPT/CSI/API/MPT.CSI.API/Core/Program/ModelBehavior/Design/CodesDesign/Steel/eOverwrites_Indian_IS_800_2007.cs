// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_Indian_IS_800_2007.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="Indian_IS_800_2007" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_Indian_IS_800_20078
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// Section class.
        /// </summary>
        SectionClass = 2,

        /// <summary>
        /// Column bucking curve, y-y.
        /// </summary>
        ColumnBucklingCurveYY = 3,

        /// <summary>
        /// Column bucking curve, z-z.
        /// </summary>
        ColumnBucklingCurveZZ = 4,

        /// <summary>
        /// Is the section rolled?
        /// </summary>
        IsRolledSection = 5,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 6,


        /// <summary>
        /// The deflection check type.
        /// </summary>
        DeflectionCheckType = 7,


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
        /// The deflection limit for dead load.
        /// </summary>
        DeflectionLimit_DeadLoad_Absolute = 13,


        /// <summary>
        /// The deflection limit for combined specified dead and live load.
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Absolute = 14,


        /// <summary>
        /// The deflection limit for live load.
        /// </summary>
        DeflectionLimit_LiveLoad_Absolute = 15,


        /// <summary>
        /// The deflection limit, total.
        /// </summary>
        DeflectionLimit_Total_Absolute = 16,


        /// <summary>
        /// The camber limit, total.
        /// </summary>
        CamberLimit_Total_Absolute = 17,


        /// <summary>
        /// The specified camber.
        /// </summary>
        SpecifiedCamber = 18,


        /// <summary>
        /// The net area/total area ratio.
        /// </summary>
        NetAreaToTotalAreaRatio = 19,


        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 20,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 21,

        /// <summary>
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatio_Minor = 22,

        /// <summary>
        /// The unbraced length ratio, lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_LateralTorsionalBuckling = 23,

        /// <summary>
        /// The effective length factor, braced, K1 major.
        /// </summary>
        EffectiveLengthFactor_Braced_K1_Major = 24,

        /// <summary>
        /// The effective length factor, braced, K1 minor.
        /// </summary>
        EffectiveLengthFactor_Braced_K1_Minor = 25,

        /// <summary>
        /// The effective length factor, sway, K2 major.
        /// </summary>
        EffectiveLengthFactor_Sway_K2_Major = 26,

        /// <summary>
        /// The effective length factor, sway, K2 minor.
        /// </summary>
        EffectiveLengthFactor_Sway_K2_Minor = 27,

        /// <summary>
        /// Effective length factor, K laeral-torsional buckling.
        /// </summary>
        EffectiveLengthFactor_K_LateralTorsionalBuckling = 28,

        /// <summary>
        /// Bending coefficient, C1.
        /// </summary>
        BendingCoefficient_C1 = 29,

        /// <summary>
        /// The uniform moment factor, Cz.
        /// </summary>
        UniformMomentFactor_Cmz = 30,

        /// <summary>
        /// The uniform moment factor, Cy.
        /// </summary>
        UniformMomentFactor_Cmy = 31,

        /// <summary>
        /// The uniform moment factor, CmLT.
        /// </summary>
        UniformMomentFactor_CmLT = 32,

        /// <summary>
        /// The moment coefficient, kz.
        /// </summary>
        MomentCoefficient_kz = 33,

        /// <summary>
        /// The moment coefficient, ky.
        /// </summary>
        MomentCoefficient_ky = 34,

        /// <summary>
        /// The moment coefficient, kLT.
        /// </summary>
        MomentCoefficient_kLT = 35,

        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 36,


        /// <summary>
        /// The compressive capacity, Pd.
        /// </summary>
        CompressiveCapacity_Pd = 37,


        /// <summary>
        /// The tensile capacity, Td.
        /// </summary>
        TensileCapacity_Td = 38,


        /// <summary>
        /// The major bending capacity, Mdz.
        /// </summary>
        MajorBendingCapacity_Mdz = 39,


        /// <summary>
        /// The minor bending capacity, Mdy.
        /// </summary>
        MinorBendingCapacity_Mdy = 40,


        /// <summary>
        /// The minor bending capacity, Mcr.
        /// </summary>
        MinorBendingCapacity_Mcr = 41,


        /// <summary>
        /// The major shear capacity, Vdy.
        /// </summary>
        MajorShearCapacity_Vdy = 42,


        /// <summary>
        /// The minor shear capacity, Vdz.
        /// </summary>
        MinorShearCapacity_Vdz = 43,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 44,
    }
}
#endif