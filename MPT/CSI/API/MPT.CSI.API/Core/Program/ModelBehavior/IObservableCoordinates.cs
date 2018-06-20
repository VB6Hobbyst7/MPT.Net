// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IObservableCoordinates.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object returns coordinates data.
    /// </summary>
    public interface IObservableCoordinates
    {
        /// <summary>
        /// Returns the x, y and z coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinate">The cartesian x-, y-, z-coordinate of the specified point element/object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        void GetCoordinate(string name,
            ref Coordinate3DCartesian coordinate,
            string coordinateSystem = CoordinateSystems.Global);
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the r, Theta and z coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinate">The cylindrical r-, theta-, z-coordinate of the specified point element/object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        void GetCoordinate(string name,
            ref Coordinate3DCylindrical coordinate,
            string coordinateSystem = CoordinateSystems.Global);

        /// <summary>
        /// Returns the r, a and b coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinate">The spherical r-, a-, b-coordinate of the specified point element/object in the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        void GetCoordinate(string name,
            ref Coordinate3DSpherical coordinate,
            string coordinateSystem = CoordinateSystems.Global);
#endif
    }
}
