// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_Chinese_2010.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Overwrites available for <see cref="Chinese_2010" /> steel design in the application.
    /// </summary>
    public enum eOverwrites_Chinese_2010
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The element type.
        /// </summary>
        ElementType = 2,

        /// <summary>
        /// Is the frame a transfer column?
        /// </summary>
        IsTransferColumn = 3,

        /// <summary>
        /// Seismic magnification factor.
        /// </summary>
        SeismicMagnificationFactor = 4,

        /// <summary>
        /// Is the section rolled?
        /// </summary>
        IsRolledSection = 5,

        /// <summary>
        /// Is the flange cut by gas?
        /// </summary>
        IsFlangeCutByGas = 6,

        /// <summary>
        /// Are both ends pinned?
        /// </summary>
        AreBothEndsPinned = 7,

        /// <summary>
        /// Ignore b/t slenderness check?
        /// </summary>
        Ignore_bt_Check = 8,

        /// <summary>
        /// Is beam classified as a flexo-compression member?
        /// </summary>
        ClassifyBeamAsFlexoCompressionMember = 9,

        /// <summary>
        /// Is the beam top loaded?
        /// </summary>
        IsBeamTopLoaded = 10,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 11,


        /// <summary>
        /// The deflection check type.
        /// </summary>
        DeflectionCheckType = 12,


        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 13,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 14,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 15,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 16,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 17,


        /// <summary>
        /// The deflection limit for dead load.
        /// </summary>
        DeflectionLimit_DeadLoad_Absolute = 18,


        /// <summary>
        /// The deflection limit for combined specified dead and live load.
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Absolute = 19,


        /// <summary>
        /// The deflection limit for live load.
        /// </summary>
        DeflectionLimit_LiveLoad_Absolute = 20,


        /// <summary>
        /// The deflection limit, total.
        /// </summary>
        DeflectionLimit_Total_Absolute = 21,


        /// <summary>
        /// The camber limit, total.
        /// </summary>
        CamberLimit_Total_Absolute = 22,


        /// <summary>
        /// The specified camber.
        /// </summary>
        SpecifiedCamber = 23,


        /// <summary>
        /// The net area/total area ratio.
        /// </summary>
        NetAreaToTotalAreaRatio = 24,


        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 25,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 26,

        /// <summary>
        /// The unbraced length ratio, minor &amp; lateral-torsional buckling.
        /// </summary>
        UnbracedLengthRatio_Minor_LateralTorsionalBuckling = 27,

        /// <summary>
        /// The effective length factor, Mue major.
        /// </summary>
        EffectiveLengthFactor_Mue_Major = 28,

        /// <summary>
        /// The effective length factor, Mue minor.
        /// </summary>
        EffectiveLengthFactor_Mue_Minor = 29,

        /// <summary>
        /// The moment coefficient, Beta_m major.
        /// </summary>
        MomentCoefficient_Beta_m_Major = 30,

        /// <summary>
        /// The moment coefficient, Beta_m minor.
        /// </summary>
        MomentCoefficient_Beta_m_Minor = 31,

        /// <summary>
        /// The moment coefficient, Beta_t major.
        /// </summary>
        MomentCoefficient_Beta_t_Major = 32,

        /// <summary>
        /// The moment coefficient, Beta_t minor.
        /// </summary>
        MomentCoefficient_Beta_t_Minor = 33,

        /// <summary>
        /// Axial stability coefficient, phi*Major.
        /// </summary>
        AxialStabilityCoefficient_Phi_Major = 34,

        /// <summary>
        /// Axial stability coefficient, phi*Minor.
        /// </summary>
        AxialStabilityCoefficient_Phi_Minor = 35,

        /// <summary>
        /// Flexural stability coefficient, phi*bMajor.
        /// </summary>
        FlexuralStabiityCoefficient_Phi_bMajor = 36,

        /// <summary>
        /// Flexural stability coefficient, phi*bMinor.
        /// </summary>
        FlexuralStabiityCoefficient_Phi_bMinor = 37,

        /// <summary>
        /// Plasticity factor, Gamma Major.
        /// </summary>
        PlasticityFactor_Gamma_Major = 38,

        /// <summary>
        /// Plasticity factor, Gamma Minor.
        /// </summary>
        PlasticityFactor_Gamma_Minor = 39,

        /// <summary>
        /// Section influence coefficient, Eta.
        /// </summary>
        SectionInfluenceCoefficient_Eta = 40,

        /// <summary>
        /// B/C capacity factor, Eta.
        /// </summary>
        BOverCCapacityFactor_eta = 41,

        /// <summary>
        /// The euler moment factor, Delta Major.
        /// </summary>
        EulerMomentFactor_Delta_Major = 42,

        /// <summary>
        /// The euler moment factor, Delta Minor.
        /// </summary>
        EulerMomentFactor_Delta_Minor = 43,

        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 44,

        /// <summary>
        /// Allowable norma stress, f.
        /// </summary>
        AllowableNormalStress_f = 45,

        /// <summary>
        /// Allowable shear stress, fv.
        /// </summary>
        AllowableShearStress_fv = 46,

        /// <summary>
        /// Consider fictitious shear?
        /// </summary>
        ConsiderFictitiousShear = 47,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 48,

        /// <summary>
        /// Dual system magnification factor.
        /// </summary>
        DualSystemMagnificationFactor = 49,

        /// <summary>
        /// Lo/r limit in compression.
        /// </summary>
        LoOverRLimitInCompression = 50,

        /// <summary>
        /// L/r limit in tension.
        /// </summary>
        LOverRLimitInTension = 51,
    }
}
#endif