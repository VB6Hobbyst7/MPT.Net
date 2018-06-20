// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="Editor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Edit;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{

    /// <summary>
    /// Represents editing actions in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IEditor" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Editor : CSiApiBase, IEditor
    {

        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The area editor
        /// </summary>
        private AreaEditor _areaEditor;
        /// <summary>
        /// The frame editor
        /// </summary>
        private FrameEditor _frameEditor;
        /// <summary>
        /// The general editor
        /// </summary>
        private GeneralEditor _generalEditor;
        /// <summary>
        /// The point editor
        /// </summary>
        private PointEditor _pointEditor;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The solid editor
        /// </summary>
        private SolidEditor _solidEditor;
#endif
        #endregion


        #region Properties        
        /// <summary>
        /// Gets the area editor.
        /// </summary>
        /// <value>The area editor.</value>
        public AreaEditor AreaEditor => _areaEditor ?? (_areaEditor = new AreaEditor(_seed));

        /// <summary>
        /// Gets the frame editor.
        /// </summary>
        /// <value>The frame editor.</value>
        public FrameEditor FrameEditor => _frameEditor ?? (_frameEditor = new FrameEditor(_seed));

        /// <summary>
        /// Gets the general editor.
        /// </summary>
        /// <value>The general editor.</value>
        public GeneralEditor GeneralEditor => _generalEditor ?? (_generalEditor = new GeneralEditor(_seed));

        /// <summary>
        /// Gets the point editor.
        /// </summary>
        /// <value>The point editor.</value>
        public PointEditor PointEditor => _pointEditor ?? (_pointEditor = new PointEditor(_seed));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the solid editor.
        /// </summary>
        /// <value>The solid editor.</value>
        public SolidEditor SolidEditor => _solidEditor ?? (_solidEditor = new SolidEditor(_seed));
#endif
        #endregion


        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Editor(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        #endregion
        
    }
}
