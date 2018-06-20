// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="Designer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents structural design in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDesigner" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Designer : CSiApiBase, IDesigner
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The design concrete
        /// </summary>
        private DesignConcrete _designConcrete;
        /// <summary>
        /// The design steel
        /// </summary>
        private DesignSteel _designSteel;
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
        /// <summary>
        /// The design aluminum
        /// </summary>
        private DesignAluminum _designAluminum;
        /// <summary>
        /// The design cold formed
        /// </summary>
        private DesignColdFormed _designColdFormed;
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        private DesignCompositeBeam _designCompositeBeam;
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        private DesignDetailing _designDetailing;
#endif
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the design concrete.
        /// </summary>
        /// <value>The design concrete.</value>
        public DesignConcrete DesignConcrete => _designConcrete ?? (_designConcrete = new DesignConcrete(_seed));

        /// <summary>
        /// Gets the design steel.
        /// </summary>
        /// <value>The design steel.</value>
        public DesignSteel DesignSteel => _designSteel ?? (_designSteel = new DesignSteel(_seed));

#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
        /// <summary>
        /// Gets the design aluminum.
        /// </summary>
        /// <value>The design aluminum.</value>
        public DesignAluminum DesignAluminum => _designAluminum ?? (_designAluminum = new DesignAluminum(_seed));

        /// <summary>
        /// Gets the design cold formed.
        /// </summary>
        /// <value>The design cold formed.</value>
        public DesignColdFormed DesignColdFormed => _designColdFormed ?? (_designColdFormed = new DesignColdFormed(_seed));
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the design composite beam.
        /// </summary>
        /// <value>The design composite beam.</value>
        public DesignCompositeBeam DesignCompositeBeam => _designCompositeBeam ?? (_designCompositeBeam = new DesignCompositeBeam(_seed));
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the design detailing.
        /// </summary>
        /// <value>The design detailing.</value>
        public DesignDetailing DesignDetailing => _designDetailing ?? (_designDetailing = new DesignDetailing(_seed));
#endif
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Designer" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Designer(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


#endregion
    }
}
