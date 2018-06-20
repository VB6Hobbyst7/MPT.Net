// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-24-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_CSA_A23_3_04.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="CSA_A23_3_04" /> concrete design in the application.
    /// </summary>
    public enum ePreferences_CSA_A23_3_04
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
        /// The steel phi.
        /// </summary>
        SteelPhi = 4,

        /// <summary>
        /// The concrete phi.
        /// </summary>
        ConcretePhi = 5,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 6,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 7,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 8
    }
}
#endif