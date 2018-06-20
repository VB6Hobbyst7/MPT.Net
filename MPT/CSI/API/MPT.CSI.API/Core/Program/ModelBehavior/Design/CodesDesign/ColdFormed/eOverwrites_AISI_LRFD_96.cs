// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_AISI_LRFD_96.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    /// <summary>
    /// Overwrites available for <see cref="AISI_LRFD_96" /> cold-formed steel design in the application.
    /// </summary>
    public enum eOverwrites_AISI_LRFD_96
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 2,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatioMajor = 3,

        /// <summary>
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatioMinor = 4,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_KMajor = 5,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_KMinor = 6,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_CmMajor = 7,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_CmMinor = 8,

        /// <summary>
        /// The moment coefficient, Ctf major.
        /// </summary>
        MomentCoefficient_CtfMajor = 9,

        /// <summary>
        /// The moment coefficient, Ctf minor.
        /// </summary>
        MomentCoefficient_CtfMinor = 10,

        /// <summary>
        /// The bending coefficient, Cb.
        /// </summary>
        BendingCoefficient_Cb = 11,

        /// <summary>
        /// The moment factor, alpha major.
        /// </summary>
        MomentFactor_AlphaMajor = 12,

        /// <summary>
        /// The moment factor, alpha minor.
        /// </summary>
        MomentFactor_AlphaMinor = 13,

        /// <summary>
        /// Rhrough-fastened to deck.
        /// </summary>
        ThroughFastenedToDeck = 14,

        /// <summary>
        /// The fastener eccentricity, a/b.
        /// </summary>
        FastenerEccentricity_a_b = 15,

        /// <summary>
        /// The hole diameter at top flange.
        /// </summary>
        HoleDiameterAtTopFlange = 16,

        /// <summary>
        /// The hole diameter at bottom flange.
        /// </summary>
        HoleDiameterAtBottomFlange = 17,

        /// <summary>
        /// The hole diameter on web.
        /// </summary>
        HoleDiameterOnWeb = 18,

        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 19,

        /// <summary>
        /// The nominal compressive capacity, PnC.
        /// </summary>
        NominalCompressiveCapacity_Pnc = 20,

        /// <summary>
        /// The nominal tensile capacity, PNT.
        /// </summary>
        NominalTensileCapacity_Pnt = 21,

        /// <summary>
        /// The nominal bending capacity for yielding, Mn33.
        /// </summary>
        NominalBendingCapacityForYielding_Mn33 = 22,

        /// <summary>
        /// The nominal bending capacity for yielding, Mn22.
        /// </summary>
        NominalBendingCapacityForYielding_Mn22 = 23,

        /// <summary>
        /// The nominal bending capacity for lateral torsional buckling, Mn33.
        /// </summary>
        NominalBendingCapacityForLateralTorsionalBuckling_Mn33 = 24,

        /// <summary>
        /// The nominal bending capacity for lateral torsional buckling, Mn22.
        /// </summary>
        NominalBendingCapacityForLateralTorsionalBuckling_Mn22 = 25,

        /// <summary>
        /// The nominal shear capacity, Vn2.
        /// </summary>
        NominalShearCapacity_Vn2 = 26,

        /// <summary>
        /// The nominal shear capacity, Vn3.
        /// </summary>
        NominalShearCapacity_Vn3 = 27
    }
}
#endif