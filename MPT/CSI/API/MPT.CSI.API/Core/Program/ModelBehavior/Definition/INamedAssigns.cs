// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="INamedAssigns.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements modifiers and releases to basic objects in the application.
    /// </summary>
    public interface INamedAssigns
    {
        #region Properties                        
        /// <summary>
        /// Gets the area modifiers.
        /// </summary>
        /// <value>The area modifiers.</value>
        AreaModifiers AreaModifiers { get; }

        /// <summary>
        /// Gets the cable modifiers.
        /// </summary>
        /// <value>The cable modifiers.</value>
        CableModifiers CableModifiers { get; }

        /// <summary>
        /// Gets the frame modifiers.
        /// </summary>
        /// <value>The frame modifiers.</value>
        FrameModifiers FrameModifiers { get; }

        /// <summary>
        /// Gets the frame releases.
        /// </summary>
        /// <value>The frame releases.</value>
        FrameReleases FrameReleases { get; }

        #endregion
    }
}
#endif