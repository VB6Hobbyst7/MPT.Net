// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_AS_3600_09.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Overwrites available for <see cref="AS_3600_09" /> concrete design in the application.
    /// </summary>
    public enum eOverwrites_AS_3600_09
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
        /// The moment coefficient, km major.
        /// </summary>
        MomentCoefficient_km_Major = 7,

        /// <summary>
        /// The moment coefficient, km minor.
        /// </summary>
        MomentCoefficient_km_Minor = 8,

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
    }
}
#endif