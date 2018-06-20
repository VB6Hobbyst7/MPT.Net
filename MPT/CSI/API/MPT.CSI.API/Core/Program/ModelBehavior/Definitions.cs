// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="Definitions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents various definitions in the application, such as groups, load patterns, constraints, cross-sections, etc.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDefinitions" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Definitions : CSiApiBase, IDefinitions
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        private BridgeObjects _bridgeObjects;
#elif !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The joint patterns
        /// </summary>
        private JointPatterns _jointPatterns;
        /// <summary>
        /// The named assigns
        /// </summary>
        private NamedAssigns _namedAssigns;
        /// <summary>
        /// The section cuts
        /// </summary>
        private SectionCuts _sectionCuts;
#else
        private Abstractions _abstractions;
#endif
        /// <summary>
        /// The constraints
        /// </summary>
        private Constraints _constraints;
        /// <summary>
        /// The coordinate systems
        /// </summary>
        private CoordinateSystems _coordinateSystems;
        /// <summary>
        /// The functions
        /// </summary>
        private Functions _functions;
        /// <summary>
        /// The generalized displacements
        /// </summary>
        private GeneralizedDisplacements _generalizedDisplacements;
        /// <summary>
        /// The general reference lines
        /// </summary>
        private GeneralReferenceLines _generalReferenceLines;
        /// <summary>
        /// The groups
        /// </summary>
        private Groups _groups;

        /// <summary>
        /// The load cases
        /// </summary>
        private LoadCases _loadCases;
        /// <summary>
        /// The load combinations
        /// </summary>
        private LoadCombinations _loadCombinations;
        /// <summary>
        /// The load patterns
        /// </summary>
        private LoadPatterns _loadPatterns;
        /// <summary>
        /// The mass source
        /// </summary>
        private MassSource _massSource;
       
        /// <summary>
        /// The properties
        /// </summary>
        private Properties _properties;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// The named sets
        /// </summary>
        private NamedSets _namedSets;
#endif
        #endregion

        #region Properties    
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// Gets the bridge objects.
        /// </summary>
        /// <value>The bridge objects.</value>
        public BridgeObjects BridgeObjects => _bridgeObjects ?? (_bridgeObjects = new BridgeObjects(_seed));
#elif !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        /// <summary>
        /// Gets the joint patterns.
        /// </summary>
        /// <value>The joint patterns.</value>
        public JointPatterns JointPatterns => _jointPatterns ?? (_jointPatterns = new JointPatterns(_seed));

        /// <summary>
        /// Gets the named assigns.
        /// </summary>
        /// <value>The named assigns.</value>
        public NamedAssigns NamedAssigns => _namedAssigns ?? (_namedAssigns = new NamedAssigns(_seed));

        /// <summary>
        /// Gets the section cuts.
        /// </summary>
        /// <value>The section cuts.</value>
        public SectionCuts SectionCuts => _sectionCuts ?? (_sectionCuts = new SectionCuts(_seed));
#else
        /// <summary>
        /// Gets the abstraction objects.
        /// </summary>
        /// <value>The section cuts.</value>
        public Abstractions Abstractions => _abstractions ?? (_abstractions = new Abstractions(_seed));
#endif
        /// <summary>
        /// Gets the constraints.
        /// </summary>
        /// <value>The constraints.</value>
        public Constraints Constraints => _constraints ?? (_constraints = new Constraints(_seed));

        /// <summary>
        /// Gets the coordinate systems.
        /// </summary>
        /// <value>The coordinate systems.</value>
        public CoordinateSystems CoordinateSystems => _coordinateSystems ?? (_coordinateSystems = new CoordinateSystems(_seed));

        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <value>The functions.</value>
        public Functions Functions => _functions ?? (_functions = new Functions(_seed));

        /// <summary>
        /// Gets the generalized displacements.
        /// </summary>
        /// <value>The generalized displacements.</value>
        public GeneralizedDisplacements GeneralizedDisplacements => _generalizedDisplacements ?? (_generalizedDisplacements = new GeneralizedDisplacements(_seed));

        /// <summary>
        /// Gets the general reference lines.
        /// </summary>
        /// <value>The general reference lines.</value>
        public GeneralReferenceLines GeneralReferenceLines => _generalReferenceLines ?? (_generalReferenceLines = new GeneralReferenceLines(_seed));

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public Groups Groups => _groups ?? (_groups = new Groups(_seed));

        /// <summary>
        /// Gets the load cases.
        /// </summary>
        /// <value>The load cases.</value>
        public LoadCases LoadCases => _loadCases ?? (_loadCases = new LoadCases(_seed));

        /// <summary>
        /// Gets the load combinations.
        /// </summary>
        /// <value>The load combinations.</value>
        public LoadCombinations LoadCombinations => _loadCombinations ?? (_loadCombinations = new LoadCombinations(_seed));

        /// <summary>
        /// Gets the load patterns.
        /// </summary>
        /// <value>The load patterns.</value>
        public LoadPatterns LoadPatterns => _loadPatterns ?? (_loadPatterns = new LoadPatterns(_seed));

        /// <summary>
        /// Gets the mass source.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSource MassSource => _massSource ?? (_massSource = new MassSource(_seed));

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public Properties Properties => _properties ?? (_properties = new Properties(_seed));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// Gets the named sets.
        /// </summary>
        /// <value>The named sets.</value>
        public NamedSets NamedSets => _namedSets ?? (_namedSets = new NamedSets(_seed));
#endif
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Definitions" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Definitions(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


#endregion

    }
}
