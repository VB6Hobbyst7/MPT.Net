// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="AnalysisModeler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents the various analysis elements in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IAnalysisModeler" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class AnalysisModeler : CSiApiBase, IAnalysisModeler
    {

        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The area element
        /// </summary>
        private AreaElement _areaElement;
        /// <summary>
        /// The line element
        /// </summary>
        private LineElement _lineElement;
        /// <summary>
        /// The link element
        /// </summary>
        private LinkElement _linkElement;
        /// <summary>
        /// The point element
        /// </summary>
        private PointElement _pointElement;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The plane element
        /// </summary>
        private PlaneElement _planeElement;
        /// <summary>
        /// The solid element
        /// </summary>
        private SolidElement _solidElement;
#endif
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the area element.
        /// </summary>
        /// <value>The area element.</value>
        public AreaElement AreaElement => _areaElement ?? (_areaElement = new AreaElement(_seed));

        /// <summary>
        /// Gets the line element.
        /// </summary>
        /// <value>The line element.</value>
        public LineElement LineElement => _lineElement ?? (_lineElement = new LineElement(_seed));

        /// <summary>
        /// Gets the link element.
        /// </summary>
        /// <value>The link element.</value>
        public LinkElement LinkElement => _linkElement ?? (_linkElement = new LinkElement(_seed));

        /// <summary>
        /// Gets the point element.
        /// </summary>
        /// <value>The point element.</value>
        public PointElement PointElement => _pointElement ?? (_pointElement = new PointElement(_seed));
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the plane element.
        /// </summary>
        /// <value>The plane element.</value>
        public PlaneElement PlaneElement => _planeElement ?? (_planeElement = new PlaneElement(_seed));

        /// <summary>
        /// Gets the solid element.
        /// </summary>
        /// <value>The solid element.</value>
        public SolidElement SolidElement => _solidElement ?? (_solidElement = new SolidElement(_seed));
#endif
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisModeler" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AnalysisModeler(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }
        #endregion
    }
}