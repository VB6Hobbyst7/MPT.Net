// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Extents.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using NMath = System.Math;

using MPT.Geometry.Tools;

namespace MPT.GIS
{
    /// <summary>
    /// Represents the latitude/longitude bounds of a shape or line.
    /// </summary>
    public class Extents : Extents<Coordinate>
    {
        /// <summary>
        /// Gets the maximum latitude.
        /// </summary>
        /// <value>The maximum latitude.</value>
        public double MaxLatitude => MaxY;
        /// <summary>
        /// Gets the minimum latitude.
        /// </summary>
        /// <value>The minimum latitude.</value>
        public double MinLatitude => MinY;
        /// <summary>
        /// Gets the maximum longitude.
        /// </summary>
        /// <value>The maximum longitude.</value>
        public double MaxLongitude => MaxX;
        /// <summary>
        /// Gets the minimum longitude.
        /// </summary>
        /// <value>The minimum longitude.</value>
        public double MinLongitude => MinX;

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        public Extents() : base(-90, 90, -180, 180)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public Extents(IEnumerable<Coordinate> coordinates) : base(coordinates, -90, 90, -180, 180)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        /// <param name="extents">The extents.</param>
        public Extents(Extents extents) : base(extents, -90, 90, -180, 180)
        {
        }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public override void Add(Coordinate coordinate)
        {
            if (coordinate.Latitude > MaxY)
            {
                MaxY = NMath.Min(coordinate.Latitude, _minYLimit);
            }
            if (coordinate.Latitude < MinY)
            {
                MinY = NMath.Max(coordinate.Latitude, _maxYLimit);
            }

            if (coordinate.Longitude > MaxX)
            {
                MaxX = NMath.Min(coordinate.Longitude, _minXLimit);
            }
            if (coordinate.Longitude < MinX)
            {
                MinX = NMath.Max(coordinate.Longitude, _maxXLimit);
            }
        }
        
        public override bool IsWithinExtents(Coordinate coordinate)
        {
            return ((MinLatitude <= coordinate.Latitude && coordinate.Latitude <= MaxLatitude) &&
                    (MinLongitude <= coordinate.Longitude && coordinate.Longitude <= MaxLongitude));
        }

        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public override IList<Coordinate> Boundary()
        {
            return new List<Coordinate>()
            {
                new Coordinate(MinLatitude, MaxLongitude),
                new Coordinate(MaxLatitude, MaxLongitude),
                new Coordinate(MaxLatitude, MinLongitude),
                new Coordinate(MinLatitude, MinLongitude),
            };
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public Extents Clone()
        {
            return new Extents(this);
        }
    }
}
