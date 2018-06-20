// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IObjectModeler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements the various objects in the application.
    /// </summary>
    public interface IObjectModeler
    {
        #region Properties        
        /// <summary>
        /// Gets the area object.
        /// </summary>
        /// <value>The area object.</value>
        AreaObject AreaObject { get; }

        /// <summary>
        /// Gets the frame object.
        /// </summary>
        /// <value>The frame object.</value>
        FrameObject FrameObject { get; }

        /// <summary>
        /// Gets the link object.
        /// </summary>
        /// <value>The link object.</value>
        LinkObject LinkObject { get; }

        /// <summary>
        /// Gets the point object.
        /// </summary>
        /// <value>The point object.</value>
        PointObject PointObject { get; }

        /// <summary>
        /// Gets the tendon object.
        /// </summary>
        /// <value>The tendon object.</value>
        TendonObject TendonObject { get; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the cable object.
        /// </summary>
        /// <value>The cable object.</value>
        CableObject CableObject { get; }

        /// <summary>
        /// Gets the solid object.
        /// </summary>
        /// <value>The solid object.</value>
        SolidObject SolidObject { get; }

#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        /// <summary>
        /// Gets the external analysis results.
        /// </summary>
        /// <value>The external analysis results.</value>
        ExternalAnalysisResults ExternalAnalysisResults { get; }
#endif
#endif
        #endregion
    }
}