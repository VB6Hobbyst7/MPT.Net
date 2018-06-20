// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AASHTO_LRFD_2012.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="AASHTO_LRFD_2012"/> concrete design in the application.
    /// </summary>
    public enum ePreferences_AASHTO_LRFD_2012
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
        /// The seismic zone.
        /// </summary>
        SeismicZone = 4,
        
        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 5,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 6,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 7
    }
}
#endif