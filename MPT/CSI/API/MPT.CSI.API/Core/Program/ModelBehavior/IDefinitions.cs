// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IDefinitions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements representations of various definitions in the application, such as groups, load patterns, constraints, cross-sections, etc.
    /// </summary>
    public interface IDefinitions
    {
        #region Properties    
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// Gets the bridge objects.
        /// </summary>
        /// <value>The bridge objects.</value>
        BridgeObjects BridgeObjects   { get; }
#elif !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        /// <summary>
        /// Gets the joint patterns.
        /// </summary>
        /// <value>The joint patterns.</value>
        JointPatterns JointPatterns  { get; }

        /// <summary>
        /// Gets the named assigns.
        /// </summary>
        /// <value>The named assigns.</value>
        NamedAssigns NamedAssigns  { get; }

        /// <summary>
        /// Gets the section cuts.
        /// </summary>
        /// <value>The section cuts.</value>
        SectionCuts SectionCuts  { get; }
#else
        /// <summary>
        /// Gets the abstraction objects.
        /// </summary>
        /// <value>The section cuts.</value>
        Abstractions Abstractions { get; }
#endif
        /// <summary>
        /// Gets the constraints.
        /// </summary>
        /// <value>The constraints.</value>
        Constraints Constraints { get; }

        /// <summary>
        /// Gets the coordinate systems.
        /// </summary>
        /// <value>The coordinate systems.</value>
        CoordinateSystems CoordinateSystems { get; }

        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <value>The functions.</value>
        Functions Functions { get; }

        /// <summary>
        /// Gets the generalized displacements.
        /// </summary>
        /// <value>The generalized displacements.</value>
        GeneralizedDisplacements GeneralizedDisplacements { get; }

        /// <summary>
        /// Gets the general reference lines.
        /// </summary>
        /// <value>The general reference lines.</value>
        GeneralReferenceLines GeneralReferenceLines { get; }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        Groups Groups { get; }

        /// <summary>
        /// Gets the load cases.
        /// </summary>
        /// <value>The load cases.</value>
        LoadCases LoadCases { get; }

        /// <summary>
        /// Gets the load combinations.
        /// </summary>
        /// <value>The load combinations.</value>
        LoadCombinations LoadCombinations { get; }

        /// <summary>
        /// Gets the load patterns.
        /// </summary>
        /// <value>The load patterns.</value>
        LoadPatterns LoadPatterns { get; }

        /// <summary>
        /// Gets the mass source.
        /// </summary>
        /// <value>The mass source.</value>
        MassSource MassSource { get; }
        
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        Properties Properties { get; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// Gets the named sets.
        /// </summary>
        /// <value>The named sets.</value>
        NamedSets NamedSets { get; }
#endif
        #endregion
    }
}