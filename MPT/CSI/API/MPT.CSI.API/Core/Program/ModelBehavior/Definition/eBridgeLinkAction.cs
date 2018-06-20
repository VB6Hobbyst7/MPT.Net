// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eBridgeLinkAction.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Actions available for bridge links in the application.
    /// </summary>
    public enum eBridgeLinkAction
    {
        /// <summary>
        /// Update linked model.
        /// </summary>
        Update = 1,

        /// <summary>
        /// Clear all from linked model.
        /// </summary>
        Clear = 2,

        /// <summary>
        /// Convert to unlinked model.
        /// </summary>
        Convert = 3
    }
}
#endif
