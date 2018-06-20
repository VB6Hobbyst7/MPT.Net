// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IEditor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Edit;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements editing actions in the application.
    /// </summary>
    public interface IEditor
    {
        #region Properties        
        /// <summary>
        /// Gets the area editor.
        /// </summary>
        /// <value>The area editor.</value>
        AreaEditor AreaEditor { get; }

        /// <summary>
        /// Gets the frame editor.
        /// </summary>
        /// <value>The frame editor.</value>
        FrameEditor FrameEditor { get; }


        /// <summary>
        /// Gets the general editor.
        /// </summary>
        /// <value>The general editor.</value>
        GeneralEditor GeneralEditor { get; }


        /// <summary>
        /// Gets the point editor.
        /// </summary>
        /// <value>The point editor.</value>
        PointEditor PointEditor { get; }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the solid editor.
        /// </summary>
        /// <value>The solid editor.</value>
        SolidEditor SolidEditor { get; }

#endif
        #endregion
    }
}