// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-25-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="ePreferences_AISC_360_05.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Preferences available for <see cref="AISC_360_05_IBC_2006" /> steel design in the application.
    /// </summary>
    public enum ePreferences_AISC_360_05
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// Seismic design category.
        /// </summary>
        SeismicDesignCategory = 2,

        /// <summary>
        /// Design provision.
        /// </summary>
        DesignProvision = 3,

        /// <summary>
        /// Analysis method (Obsolete, replaced by 27, 28, and 29).
        /// </summary>
        AnalysisMethodObsolete = 4,

        /// <summary>
        /// Notional load coefficient.
        /// </summary>
        NotionalLoadCoefficient = 5,

        /// <summary>
        /// Phi for bending.
        /// </summary>
        PhiBending = 6,

        /// <summary>
        /// Phi for compression.
        /// </summary>
        PhiCompression = 7,

        /// <summary>
        /// Phi for tension yielding.
        /// </summary>
        PhiTensionYielding = 8,

        /// <summary>
        /// Phi for tension fracture.
        /// </summary>
        PhiTensionFracture = 9,

        /// <summary>
        /// Phi for shear.
        /// </summary>
        PhiShear = 10,

        /// <summary>
        /// Phi for shear in short-webbed rolled I-sections.
        /// </summary>
        PhiShearShortWebbedRolledI = 11,

        /// <summary>
        /// Phi for torsion.
        /// </summary>
        PhiTorsion = 12,

        /// <summary>
        /// Ignore seismic code?
        /// </summary>
        IgnoreSeismicCode = 13,

        /// <summary>
        /// Ignore special seismic load?
        /// </summary>
        IgnoreSpecialSeismicLoad = 14,

        /// <summary>
        /// Is doubler plate plug welded?
        /// </summary>
        DoublerPlateIsPlugWelded = 15,

        /// <summary>
        /// HSS welding type.
        /// </summary>
        HSSWeldingType = 16,

        /// <summary>
        /// Reduce HSS thickness?
        /// </summary>
        ReduceHSSThickness = 17,

        /// <summary>
        /// Consider deflection?
        /// </summary>
        ConsiderDeflection = 18,

        /// <summary>
        /// The relative deflection limit for dead load. [L/Value].
        /// </summary>
        DeflectionLimit_DeadLoad_Relative = 19,


        /// <summary>
        /// The relative deflection limit for combined specified dead load and live load. [L/Value].
        /// </summary>
        DeflectionLimit_SpecifiedDeadAndLiveLoad_Relative = 20,


        /// <summary>
        /// The relative deflection limit for live load. [L/Value].
        /// </summary>
        DeflectionLimit_LiveLoad_Relative = 21,


        /// <summary>
        /// The relative deflection limit, total. [L/Value].
        /// </summary>
        DeflectionLimit_Total_Relative = 22,


        /// <summary>
        /// The relative camber limit, total. [L/Value].
        /// </summary>
        CamberLimit_Total_Relative = 23,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 24,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 25,

        /// <summary>
        /// Multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 26,

        /// <summary>
        /// Analysis method.
        /// </summary>
        AnalysisMethod = 27,

        /// <summary>
        /// Second-order method.
        /// </summary>
        SecondOrderMethod = 28,

        /// <summary>
        /// Stiffness-reduction method.
        /// </summary>
        StiffnessReductionMethod = 29,

        /// <summary>
        /// Importance factor.
        /// </summary>
        ImportanceFactor = 30,

        /// <summary>
        /// Design systems rho.
        /// </summary>
        DesignSystemsRho = 31,

        /// <summary>
        /// Design systems Sds
        /// </summary>
        DesignSystemsSds = 32,

        /// <summary>
        /// Design systems R.
        /// </summary>
        DesignSystemsR = 33,

        /// <summary>
        /// Design systems Omega0.
        /// </summary>
        DesignSystemsOmega0 = 34,

        /// <summary>
        /// Design systems Cd.
        /// </summary>
        DesignSystemsCd = 35,
    }
}