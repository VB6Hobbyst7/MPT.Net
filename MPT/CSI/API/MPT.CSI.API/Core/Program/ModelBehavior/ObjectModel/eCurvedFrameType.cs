// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eCurvedFrameType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Curved frame types available in the application.
    /// </summary>
    public enum eCurvedFrameType
    {
        /// <summary>
        /// Circular Arc Specified by a Third Point Name.
        /// The three points are the two end point of the frame object and a third point defined by naming an existing point object or specifying point coordinates.
        /// </summary>
        CircleThirdPointName = 1,

        /// <summary>
        /// Circular Arc Specified by Third Point Coordinates.
        /// The three points are the two end point of the frame object and a third point defined by naming an existing point object or specifying point coordinates.
        /// </summary>
        CircleThirdPointCoordinates = 2,

        /// <summary>
        /// Circular Arc Specified by Planar Point Coordinates and Radius.
        /// A circular curved frame defined by its end points, the coordinates of another point that lies in the plane of the curve but not necessarily on the curved frame, and a curve radius.
        /// </summary>
        CirclePlanarCoordinatesAndRadius = 3,

        /// <summary>
        /// Parabolic Arc Specified by a Third Point Name.
        /// The three points are the two end point of the frame object and a third point defined by naming an existing point object or specifying point coordinates.
        /// </summary>
        ParabolaThirdPointName = 4,

        /// <summary>
        /// Parabolic Arc Specified by Third Point Coordinates.
        /// The three points are the two end point of the frame object and a third point defined by naming an existing point object or specifying point coordinates.
        /// </summary>
        ParabolaThirdPointCoordinates = 5
    }
}

#endif