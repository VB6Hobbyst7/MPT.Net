// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="Abstractions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents abstract groupings in the application model, such as diaphragms.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IAbstraction" />
    public class Abstractions : CSiApiBase, IAbstraction
    {
        #region Fields
        private readonly CSiApiSeed _seed;

        private Diaphragm _diaphragm;
        private Story _story;
        private Tower _tower;
        private PierLabel _pierLabel;
        private SpandrelLabel _spandrelLabel;
        #endregion

        #region Properties          
        /// <summary>
        /// Gets the diaphragm.
        /// </summary>
        /// <value>The diaphragm.</value>
        public Diaphragm Diaphragm => _diaphragm ?? (_diaphragm = new Diaphragm(_seed));

        /// <summary>
        /// Gets the story.
        /// </summary>
        /// <value>The story.</value>
        public Story Story => _story ?? (_story = new Story(_seed));

        /// <summary>
        /// Gets the tower.
        /// </summary>
        /// <value>The tower.</value>
        public Tower Tower => _tower ?? (_tower = new Tower(_seed));

        /// <summary>
        /// Gets the pier label.
        /// </summary>
        /// <value>The pier label.</value>
        public PierLabel PierLabel => _pierLabel ?? (_pierLabel = new PierLabel(_seed));

        /// <summary>
        /// Gets the spandrel label.
        /// </summary>
        /// <value>The spandrel label.</value>
        public SpandrelLabel SpandrelLabel => _spandrelLabel ?? (_spandrelLabel = new SpandrelLabel(_seed));

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Abstractions"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Abstractions(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        #endregion
    }
}
#endif