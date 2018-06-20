// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="Properties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the various object sections/properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IProperties" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Properties : CSiApiBase, IProperties
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The area section
        /// </summary>
        private AreaSection _areaSection;
        /// <summary>
        /// The frame section
        /// </summary>
        private FrameSection _frameSection;
        /// <summary>
        /// The tendon section
        /// </summary>
        private TendonSection _tendonSection;
        /// <summary>
        /// The link properties
        /// </summary>
        private LinkProperties _linkProperties;
        /// <summary>
        /// The material properties
        /// </summary>
        private MaterialProperties _materialProperties;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The cable section
        /// </summary>
        private CableSection _cableSection;
        /// <summary>
        /// The solid properties
        /// </summary>
        private SolidProperties _solidProperties;
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        private PointSpring _pointSpring;
        private LineSpring _lineSpring;
        private AreaSpring _areaSpring;
#endif
        #endregion

        #region Properties                        
        /// <summary>
        /// Gets the area section.
        /// </summary>
        /// <value>The area section.</value>
        public AreaSection AreaSection => _areaSection ?? (_areaSection = new AreaSection(_seed));

        /// <summary>
        /// Gets the frame section.
        /// </summary>
        /// <value>The frame section.</value>
        public FrameSection FrameSection => _frameSection ?? (_frameSection = new FrameSection(_seed));

        /// <summary>
        /// Gets the tendon section.
        /// </summary>
        /// <value>The tendon section.</value>
        public TendonSection TendonSection => _tendonSection ?? (_tendonSection = new TendonSection(_seed));

        /// <summary>
        /// Gets the link properties.
        /// </summary>
        /// <value>The link properties.</value>
        public LinkProperties LinkProperties => _linkProperties ?? (_linkProperties = new LinkProperties(_seed));

        /// <summary>
        /// Gets the material properties.
        /// </summary>
        /// <value>The material properties.</value>
        public MaterialProperties MaterialProperties => _materialProperties ?? (_materialProperties = new MaterialProperties(_seed));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the cable section.
        /// </summary>
        /// <value>The cable section.</value>
        public CableSection CableSection => _cableSection ?? (_cableSection = new CableSection(_seed));

        /// <summary>
        /// Gets the solid properties.
        /// </summary>
        /// <value>The solid properties.</value>
        public SolidProperties SolidProperties => _solidProperties ?? (_solidProperties = new SolidProperties(_seed));
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the point spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        public PointSpring PointSpring => _pointSpring ?? (_pointSpring = new PointSpring(_seed));

        /// <summary>
        /// Gets the line spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        public LineSpring LineSpring => _lineSpring ?? (_lineSpring = new LineSpring(_seed));

        /// <summary>
        /// Gets the area spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        public AreaSpring AreaSpring => _areaSpring ?? (_areaSpring = new AreaSpring(_seed));
#endif
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Properties" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Properties(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }
        #endregion
    }
}