// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_AA_LRFD_2000.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum
{
    /// <summary>
    /// Overwrites available for <see cref="AA_LRFD_2000" /> aluminum design in the application.
    /// </summary>
    public enum eOverwrites_AA_LRFD_2000
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
        /// The bending coefficient, Cb.
        /// </summary>
        BendingCoefficient_Cb = 9,


        /// <summary>
        /// The buckling constant for compression, k1.
        /// </summary>
        BucklingConstantForCompression_k1 = 10,


        /// <summary>
        /// The buckling constant for compression, k2.
        /// </summary>
        BucklingConstantForCompression_k2 = 11,


        /// <summary>
        /// The buckling constant for bending, k1.
        /// </summary>
        BucklingConstantForBending_k1 = 12,


        /// <summary>
        /// The buckling constant for bending, k2.
        /// </summary>
        BucklingConstantForBending_k2 = 13,


        /// <summary>
        /// The safety coefficient, kt.
        /// </summary>
        SafetyCoefficient_kt = 14,


        /// <summary>
        /// The bending coefficient, C1.
        /// </summary>
        BendingCoefficient_C1 = 15,


        /// <summary>
        /// The bending coefficient, C2.
        /// </summary>
        BendingCoefficient_C2 = 16,


        /// <summary>
        /// The yield stress, Fy.
        /// </summary>
        YieldStress_Fy = 17,


        /// <summary>
        /// The compressive stress, Fa.
        /// </summary>
        CompressiveStress_Fa = 18,


        /// <summary>
        /// The tensile stress, Ft.
        /// </summary>
        TensileStress_Ft = 19,


        /// <summary>
        /// The major bending stress, Fb3.
        /// </summary>
        MajorBendingStress_Fb3 = 20,


        /// <summary>
        /// The minor bending stress, Fb2.
        /// </summary>
        MinorBendingStress_Fb2 = 21,


        /// <summary>
        /// The major shear stress, Fs2.
        /// </summary>
        MajorShearStress_Fs2 = 22,


        /// <summary>
        /// The minor shear stress, Fs3.
        /// </summary>
        MinorShearStress_Fs3 = 23
    }
}
#endif