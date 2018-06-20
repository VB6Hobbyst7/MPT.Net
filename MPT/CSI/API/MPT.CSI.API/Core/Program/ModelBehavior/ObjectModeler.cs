// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ObjectModeler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents the various objects in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObjectModeler" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ObjectModeler : CSiApiBase, IObjectModeler
    {

        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The area object
        /// </summary>
        private AreaObject _areaObject;
        /// <summary>
        /// The frame object
        /// </summary>
        private FrameObject _frameObject;
        /// <summary>
        /// The link object
        /// </summary>
        private LinkObject _linkObject;
        /// <summary>
        /// The point object
        /// </summary>
        private PointObject _pointObject;
        /// <summary>
        /// The tendon object
        /// </summary>
        private TendonObject _tendonObject;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The cable object
        /// </summary>
        private CableObject _cableObject;
        /// <summary>
        /// The solid object
        /// </summary>
        private SolidObject _solidObject;
#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// The external analysis results
        /// </summary>
        private ExternalAnalysisResults _externalAnalysisResults;
#endif
#endif
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the area object.
        /// </summary>
        /// <value>The area object.</value>
        public AreaObject AreaObject => _areaObject ?? (_areaObject = new AreaObject(_seed));

        /// <summary>
        /// Gets the frame object.
        /// </summary>
        /// <value>The frame object.</value>
        public FrameObject FrameObject => _frameObject ?? (_frameObject = new FrameObject(_seed));

        /// <summary>
        /// Gets the link object.
        /// </summary>
        /// <value>The link object.</value>
        public LinkObject LinkObject => _linkObject ?? (_linkObject = new LinkObject(_seed));

        /// <summary>
        /// Gets the point object.
        /// </summary>
        /// <value>The point object.</value>
        public PointObject PointObject => _pointObject ?? (_pointObject = new PointObject(_seed));

        /// <summary>
        /// Gets the tendon object.
        /// </summary>
        /// <value>The tendon object.</value>
        public TendonObject TendonObject => _tendonObject ?? (_tendonObject = new TendonObject(_seed));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the cable object.
        /// </summary>
        /// <value>The cable object.</value>
        public CableObject CableObject => _cableObject ?? (_cableObject = new CableObject(_seed));

        /// <summary>
        /// Gets the solid object.
        /// </summary>
        /// <value>The solid object.</value>
        public SolidObject SolidObject => _solidObject ?? (_solidObject = new SolidObject(_seed));

#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// Gets the external analysis results.
        /// </summary>
        /// <value>The external analysis results.</value>
        public ExternalAnalysisResults ExternalAnalysisResults => _externalAnalysisResults ?? (_externalAnalysisResults = new ExternalAnalysisResults(_seed));
#endif
#endif
#endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectModeler" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ObjectModeler(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        #endregion
    }
}
