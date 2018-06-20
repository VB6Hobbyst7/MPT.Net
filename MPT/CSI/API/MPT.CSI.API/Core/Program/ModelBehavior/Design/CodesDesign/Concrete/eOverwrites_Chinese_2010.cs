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

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Overwrites available for <see cref="Chinese_2010" /> concrete design in the application.
    /// </summary>
    public enum eOverwrites_Chinese_2010
    {
        /// <summary>
        /// Seismic design grade.
        /// </summary>
        SeismicDesignGrade = 1,

        /// <summary>
        /// Dual system SMF.
        /// </summary>
        DualSystemSMF = 2,

        /// <summary>
        /// MMF.
        /// </summary>
        MMF = 3,

        /// <summary>
        /// SMF.
        /// </summary>
        SMF = 4,

        /// <summary>
        /// AFMF.
        /// </summary>
        AFMF = 5,

        /// <summary>
        /// The column location.
        /// </summary>
        ColumnLocation = 6,

        /// <summary>
        /// Transfer beam of column.
        /// </summary>
        TransferBeamOfColumn = 7,

        /// <summary>
        /// Corner column seismic modification.
        /// </summary>
        CornerColumnSeismicModification = 8,

        /// <summary>
        /// Beam gravity negative moment reduction factor.
        /// </summary>
        BeamGravityNegativeMomentReductionFactor = 9,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 10,

        /// <summary>
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatio_Minor = 11,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_K_Major = 12,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_K_Minor = 13,

        /// <summary>
        /// Torsion modification factor.
        /// </summary>
        TorsionModificationFactor = 14,

        /// <summary>
        /// Torsion design factor, zeta.
        /// </summary>
        TorsionDesignFactor_Zeta = 15,

        /// <summary>
        /// Concrete cover for closed stirrup.
        /// </summary>
        ConcreteCoverForClosedStirrup = 16,

        /// <summary>
        /// Effective length factor for gravity, K Major.
        /// </summary>
        EffectiveLengthFactorForGravity_K_Major = 17,

        /// <summary>
        /// Effective length factor for gravity, K Minor.
        /// </summary>
        EffectiveLengthFactorForGravity_K_Minor = 18,
    }
}
#endif