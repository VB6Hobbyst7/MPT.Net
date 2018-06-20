// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-17-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-17-2017
// ***********************************************************************
// <copyright file="eSectionDesignerSectionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Section designer section types available in the application.
    /// </summary>
    public enum eSectionDesignerSectionType
    {
        /// <summary>
        /// I-section.
        /// </summary>
        ISection = 1,

        /// <summary>
        /// Channel section.
        /// </summary>
        Channel = 2,

        /// <summary>
        /// T-section.
        /// </summary>
        TSection = 3,

        /// <summary>
        /// Angle section.
        /// </summary>
        Angle = 4,

        /// <summary>
        /// Double angle section.
        /// </summary>
        DoubleAngle = 5,

        /// <summary>
        /// Box section.
        /// </summary>
        Box = 6,

        /// <summary>
        /// Pipe section.
        /// </summary>
        Pipe = 7,

        /// <summary>
        /// Rectangular plate section.
        /// </summary>
        Plate = 8,

        /// <summary>
        /// Solid rectanglar section.
        /// </summary>
        SolidRectangle = 101,

        /// <summary>
        /// Solid circular section.
        /// </summary>
        SolidCircle = 102,

        /// <summary>
        /// Solid segment section.
        /// </summary>
        SolidSegment = 103,

        /// <summary>
        /// Solid sector section.
        /// </summary>
        SolidSector = 104,

        /// <summary>
        /// Polygon section.
        /// </summary>
        Polygon = 201,

        /// <summary>
        /// Single reinforcing bar section.
        /// </summary>
        ReinforcingSingle = 301,

        /// <summary>
        /// Line of reinforcement.
        /// </summary>
        ReinforcingLine = 302,

        /// <summary>
        /// Rectangular line of reinforcement.
        /// </summary>
        ReinforcingRectangle = 303,

        /// <summary>
        /// Circular line of reinforcement.
        /// </summary>
        ReinforcingCircle = 304,

        /// <summary>
        /// Reference line.
        /// </summary>
        ReferenceLine = 401,

        /// <summary>
        /// Reference circle.
        /// </summary>
        ReferenceCircle = 402,

        /// <summary>
        /// Caltrans square section.
        /// </summary>
        CaltransSquare = 501,

        /// <summary>
        /// Caltrans circular section.
        /// </summary>
        CaltransCircle = 502,

        /// <summary>
        /// Caltrans hexagonal section.
        /// </summary>
        CaltransHexagon = 503,

        /// <summary>
        /// Caltrans octagonal section.
        /// </summary>
        CaltransOctagon = 504
    }
}