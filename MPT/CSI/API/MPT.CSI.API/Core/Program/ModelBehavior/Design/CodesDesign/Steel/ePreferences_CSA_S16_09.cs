// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="ePreferences_CSA_S16_09.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="CSA_S16_09" /> steel design in the application.
    /// </summary>
    public enum ePreferences_CSA_S16_09
    {
        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 1,

        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 2,

        /// <summary>
        /// The spectral acceleration ratio, Ie*Fa*Sa(0.2).
        /// </summary>
        SpectralAccelerationRatio = 3,

        /// <summary>
        /// The ductility-related modification factor, Rd.
        /// </summary>
        DuctilityRelatedModificationFactor_Rd = 4,

        /// <summary>
        /// The overstrength-related modification factor, Ro.
        /// </summary>
        OverstrengthRelatedModificationFactor_Ro = 5,

        /// <summary>
        /// Capacity factor Phi for bending.
        /// </summary>
        CapacityFactorPhiBending = 6,

        /// <summary>
        /// Capacity factor Phi for compression.
        /// </summary>
        CapacityFactorPhiCompression = 7,

        /// <summary>
        /// Capacity factor Phi for tension.
        /// </summary>
        CapacityFactorPhiTension = 8,

        /// <summary>
        /// Capacity factor Phi for shear.
        /// </summary>
        CapacityFactorPhiShear = 9,

        /// <summary>
        /// Slender section modification.
        /// </summary>
        SlenderSectionModification = 10,

        /// <summary>
        /// Ignore seismic code?
        /// </summary>
        IgnoreSeismicCode = 11,

        /// <summary>
        /// Ignore special seismic load?
        /// </summary>
        IgnoreSpecialSeismicLoad = 12,

        /// <summary>
        /// Is doubler plate plug welded?
        /// </summary>
        DoublerPlateIsPlugWelded = 13,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 14,

        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 15,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 16,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 17,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 18,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 19,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 20,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 21,

    }
}