// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AS_3600_09.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="AS_3600_09" /> concrete design in the application.
    /// </summary>
    public enum ePreferences_AS_3600_09
    {
        /// <summary>
        /// The number of interaction curves.
        /// </summary>
        NumberOfInteractionCurves = 1,

        /// <summary>
        /// The number of interaction points.
        /// </summary>
        NumberOfInteractionPoints = 2,

        /// <summary>
        /// Consider minimum eccentricity.
        /// </summary>
        ConsiderMinimumEccentricity = 3,

        /// <summary>
        /// Phi tension controlled.
        /// </summary>
        PhiTensionControlled = 4,

        /// <summary>
        /// Phi compression-controlled.
        /// </summary>
        PhiCompressionControlled = 5,

        /// <summary>
        /// Phi shear and/or torsion.
        /// </summary>
        PhiShearAndOrTorsion = 6,

        /// <summary>
        /// Phi shear, seismic.
        /// </summary>
        PhiShearSeismic = 7,

        /// <summary>
        /// Phi joint shear.
        /// </summary>
        PhiJointShear = 8,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 9,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 10,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 11
    }
}
#endif
