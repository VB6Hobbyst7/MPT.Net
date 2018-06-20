// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IAnalysisModeler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements the various analysis elements in the application.
    /// </summary>
    public interface IAnalysisModeler
    {
        #region Properties        
        /// <summary>
        /// Gets the area element.
        /// </summary>
        /// <value>The area element.</value>
        AreaElement AreaElement { get; }

        /// <summary>
        /// Gets the line element.
        /// </summary>
        /// <value>The line element.</value>
        LineElement LineElement { get; }

        /// <summary>
        /// Gets the link element.
        /// </summary>
        /// <value>The link element.</value>
        LinkElement LinkElement { get; }

        /// <summary>
        /// Gets the point element.
        /// </summary>
        /// <value>The point element.</value>
        PointElement PointElement { get; }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the plane element.
        /// </summary>
        /// <value>The plane element.</value>
        PlaneElement PlaneElement { get; }

        /// <summary>
        /// Gets the solid element.
        /// </summary>
        /// <value>The solid element.</value>
        SolidElement SolidElement { get; }
#endif
        #endregion
    }
}