// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-24-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_Eurocode_2_2004.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="Eurocode_2_2004" /> concrete design in the application.
    /// </summary>
    public enum ePreferences_Eurocode_2_2004
    {
        /// <summary>
        /// The number of interaction curves.
        /// </summary>
        NumberOfInteractionCurves = 4,

        /// <summary>
        /// The number of interaction points.
        /// </summary>
        NumberOfInteractionPoints = 5,

        /// <summary>
        /// Consider minimum eccentricity.
        /// </summary>
        ConsiderMinimumEccentricity = 6,

        /// <summary>
        /// Theta 0.
        /// </summary>
        Theta0 = 7,

        /// <summary>
        /// The steel gamma
        /// </summary>
        SteelGamma = 8,

        /// <summary>
        /// The concrete gamma.
        /// </summary>
        ConcreteGamma = 9,

        /// <summary>
        /// Alpha concrete compression.
        /// </summary>
        AlphaCC = 10,

        /// <summary>
        /// Alpha concrete tension.
        /// </summary>
        AlphaCT = 11,

        /// <summary>
        /// Alpha lightweight concrete compression.
        /// </summary>
        AlphaLCC = 12,

        /// <summary>
        /// Alpha lightweight concrete tension.
        /// </summary>
        AlphaLCT = 13,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 14,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 15,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 16,

        /// <summary>
        /// Reliability class.
        /// </summary>
        ReliabilityClass = 17
    }
}
