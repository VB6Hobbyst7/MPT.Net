// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IProperties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements object properties.
    /// </summary>
    public interface IProperties
    {
        /// <summary>
        /// Gets the area section.
        /// </summary>
        /// <value>The area section.</value>
        AreaSection AreaSection { get; }

        /// <summary>
        /// Gets the frame section.
        /// </summary>
        /// <value>The frame section.</value>
        FrameSection FrameSection { get; }

        /// <summary>
        /// Gets the tendon section.
        /// </summary>
        /// <value>The tendon section.</value>
        TendonSection TendonSection { get; }

        /// <summary>
        /// Gets the link properties.
        /// </summary>
        /// <value>The link properties.</value>
        LinkProperties LinkProperties { get; }

        /// <summary>
        /// Gets the material properties.
        /// </summary>
        /// <value>The material properties.</value>
        MaterialProperties MaterialProperties { get; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the cable section.
        /// </summary>
        /// <value>The cable section.</value>
        CableSection CableSection { get; }

        /// <summary>
        /// Gets the solid properties.
        /// </summary>
        /// <value>The solid properties.</value>
        SolidProperties SolidProperties { get; }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the point spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        PointSpring PointSpring { get; }

        /// <summary>
        /// Gets the line spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        LineSpring LineSpring { get; }

        /// <summary>
        /// Gets the area spring object.
        /// </summary>
        /// <value>The tendon section.</value>
        AreaSpring AreaSpring { get; }
#endif
    }
}