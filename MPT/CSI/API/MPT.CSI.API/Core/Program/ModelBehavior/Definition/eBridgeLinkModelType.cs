// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eBridgeLinkModelType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Linked bridge model types available in the application.
    /// </summary>
    public enum eBridgeLinkModelType
    {
        /// <summary>
        /// Spine model (frame).
        /// </summary>
        Spine = 1,

        /// <summary>
        /// Area model.
        /// </summary>
        Area = 2,

        /// <summary>
        /// Solid model.
        /// </summary>
        Solid = 3
    }
}
#endif
