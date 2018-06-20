// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eOverwrites_Singapore_CP_6599.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Overwrites available for <see cref="Singapore_CP_6599" /> concrete design in the application.
    /// </summary>
    public enum eOverwrites_Singapore_CP_6599
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
        /// The effective length factor, beta major.
        /// </summary>
        EffectiveLengthFactor_Beta_Major = 5,

        /// <summary>
        /// The effective length factor, beta minor.
        /// </summary>
        EffectiveLengthFactor_Beta_Minor = 6,

        /// <summary>
        /// The moment coefficient, Cm major.
        /// </summary>
        MomentCoefficient_Cm_Major = 7,

        /// <summary>
        /// The moment coefficient, Cm minor.
        /// </summary>
        MomentCoefficient_Cm_Minor = 8,

        /// <summary>
        /// The non-sway moment factor, Dns major.
        /// </summary>
        NonswayMomentFactor_Dns_Major = 9,

        /// <summary>
        /// The non-sway moment factor, Dns minor.
        /// </summary>
        NonswayMomentFactor_Dns_Minor = 10,

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