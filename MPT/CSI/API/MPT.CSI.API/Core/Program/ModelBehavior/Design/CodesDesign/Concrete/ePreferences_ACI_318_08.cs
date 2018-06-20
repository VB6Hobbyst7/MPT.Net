// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-25-2017
// ***********************************************************************
// <copyright file="ePreferences_ACI_318_08.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="ACI_318_08_IBC_2009" /> concrete design in the application.
    /// </summary>
    public enum ePreferences_ACI_318_08
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
        /// The seismic design category.
        /// </summary>
        SeismicDesignCategory = 4,

        /// <summary>
        /// Phi tension controlled.
        /// </summary>
        PhiTensionControlled = 5,

        /// <summary>
        /// Phi compression-controlled tied.
        /// </summary>
        PhiCompressionControlledTied = 6,

        /// <summary>
        /// Phi compression-controlled spiral.
        /// </summary>
        PhiCompressionControlledSpiral = 7,

        /// <summary>
        /// Phi shear and/or torsion.
        /// </summary>
        PhiShearAndOrTorsion = 8,

        /// <summary>
        /// Phi shear, seismic.
        /// </summary>
        PhiShearSeismic = 9,

        /// <summary>
        /// Phi joint shear.
        /// </summary>
        PhiJointShear = 10,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 11,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 12,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 13
    }
}
