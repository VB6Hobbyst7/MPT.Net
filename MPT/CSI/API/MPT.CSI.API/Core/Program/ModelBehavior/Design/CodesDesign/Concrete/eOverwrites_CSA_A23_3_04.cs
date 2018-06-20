// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_CSA_A23_3_04.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Overwrites available for <see cref="CSA_A23_3_04" /> concrete design in the application.
    /// </summary>
    public enum eOverwrites_CSA_A23_3_04
    {
        /// <summary>
        /// Framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 2,

        /// <summary>
        /// The unbraced length ratio, major.
        /// </summary>
        UnbracedLengthRatio_Major = 3,

        /// <summary>
        /// The unbraced length ratio, minor.
        /// </summary>
        UnbracedLengthRatio_Minor = 4,

        /// <summary>
        /// The effective length factor, K major.
        /// </summary>
        EffectiveLengthFactor_K_Major = 5,

        /// <summary>
        /// The effective length factor, K minor.
        /// </summary>
        EffectiveLengthFactor_K_Minor = 6,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_Cm_Major = 7,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_Cm_Minor = 8,

        /// <summary>
        /// The non-sway moment factor, Db major.
        /// </summary>
        NonswayMomentFactor_Db_Major = 9,

        /// <summary>
        /// The non-sway moment factor, Db minor.
        /// </summary>
        NonswayMomentFactor_Db_Minor = 10,

        /// <summary>
        /// The sway moment factor, Ds major.
        /// </summary>
        SwayMomentFactor_Ds_Major = 11,

        /// <summary>
        /// The sway moment factor, Ds minor.
        /// </summary>
        SwayMomentFactor_Ds_Minor = 12,

        /// <summary>
        /// Force modification factor, Rd.
        /// </summary>
        ForceModificationFactor_Rd = 13,

        /// <summary>
        /// Force modification factor, Ro.
        /// </summary>
        ForceModificationFactor_Ro = 14,

        /// <summary>
        /// Maximum aggregate size.
        /// </summary>
        MaxAggregateSize = 15,
    }
}
#endif