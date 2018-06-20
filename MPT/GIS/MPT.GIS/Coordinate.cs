// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 12-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="Coordinate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

using MPT.Math;

namespace MPT.GIS
{
    /// <summary>
    /// Coordinate composed of latitude, longitude, and possibly elevation.
    /// </summary>
    public class Coordinate : IEquatable<Coordinate>
    {
        /// <summary>
        /// Default zero tolerance for operations.
        /// </summary>
        private const double _zeroTolerance = Numbers.ZeroTolerance;

        private double _latitude;

        /// <summary>
        /// Gets or sets the latitude.
        /// This must be between -90 and +90 or else it will be limited to these values.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = validLatitude(value); }
        }

        private double _longitude;

        /// <summary>
        /// Gets or sets the longitude.
        /// This must be between -180 and +180 or else it will be limited to these values.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = validLongitude(value); }
        }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        public double? Altitude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        public Coordinate()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.
        /// This must be between -90 and +90 or else it will be limited to these values.</param>
        /// <param name="longitude">The longitude.
        /// This must be between -180 and +180 or else it will be limited to these values.</param>
        public Coordinate(
            double latitude, 
            double longitude)
        {
            initialize(latitude, longitude);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.
        /// This must be between -90 and +90 or else it will be limited to these values.</param>
        /// <param name="longitude">The longitude.
        /// This must be between -180 and +180 or else it will be limited to these values.</param>
        /// <param name="altitude">The altitude.</param>
        public Coordinate(
            double latitude, 
            double longitude, 
            double altitude)
        {
            initialize(latitude, longitude, altitude);
        }

        /// <summary>
        /// Initializes the specified latitude.
        /// </summary>
        /// <param name="latitude">The latitude.
        /// This must be between -90 and +90 or else it will be limited to these values.</param>
        /// <param name="longitude">The longitude.
        /// This must be between -180 and +180 or else it will be limited to these values.</param>
        /// <param name="altitude">The altitude.</param>
        private void initialize(
            double latitude, 
            double longitude, 
            double? altitude = null)
        {
            Latitude = validLatitude(latitude);
            Longitude = validLongitude(longitude);
            Altitude = altitude;
        }
        
        /// <summary>
        /// Returns a valid latitude.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <returns>System.Double.</returns>
        private double validLatitude(double latitude)
        {
            if (latitude > 90) return 90;
            if (latitude < -90) return -90;
            return latitude;
        }

        /// <summary>
        /// Returns a valid longitude.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <returns>System.Double.</returns>
        private double validLongitude(double longitude)
        {
            if (longitude > 180) return 180;
            if (longitude < -180) return -180;
            return longitude;
        }

        #region Operators & Equals

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Coordinate other)
        {
            if ((other != null) &&
                ((NMath.Abs(Longitude - other.Longitude) < _zeroTolerance) &&
                 (NMath.Abs(Latitude - other.Latitude) < _zeroTolerance)))
            {
                return Altitude == other.Altitude;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Coordinate) { return Equals((Coordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            if (Altitude != null)
            {
                return Longitude.GetHashCode() ^ Latitude.GetHashCode() ^ Altitude.GetHashCode();
            }
            return Longitude.GetHashCode() ^ Latitude.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return ReferenceEquals(a, null) ? !ReferenceEquals(b, null) : !a.Equals(b);
        }

        // TODO: Complete
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator -(Coordinate point1, Coordinate point2)
        {
            return new CoordinateOffset(
                point1,
                point2);
        }

        public static Coordinate operator -(Coordinate point1, CoordinateOffset point2)
        {
            return new Coordinate(
               point1.Longitude - point2.Longitude(),
               point1.Latitude - point2.Latitude());
        }
        public static Coordinate operator -(CoordinateOffset point1, Coordinate point2)
        {
            return new Coordinate(
                point2.Longitude - point1.Longitude(),
                point2.Latitude - point1.Latitude());
        }

        // TODO: Complete
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Coordinate operator +(Coordinate point1, CoordinateOffset point2)
        {
            return new Coordinate(
                point1.Longitude + point2.Longitude(),
                point1.Latitude + point2.Latitude());
        }

        public static Coordinate operator +(CoordinateOffset point1, Coordinate point2)
        {
            return new Coordinate(
                point2.Longitude + point1.Longitude(),
                point2.Latitude + point1.Latitude());
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Coordinate operator *(Coordinate point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static Coordinate operator *(double scale, Coordinate point1)
        {
            return new Coordinate(
                point1.Longitude * scale,
                point1.Latitude * scale);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Coordinate operator /(Coordinate point1, double scale)
        {
            return new Coordinate(
                point1.Longitude / scale,
                point1.Latitude / scale);
        }


        public static explicit operator Coordinate(Point point)  
        {
            Coordinate coordinate = new Coordinate(point.Y, point.X);  

            return coordinate;
        }

        public static explicit operator Coordinate(Point3D point) 
        {
            Coordinate coordinate = new Coordinate(point.Y, point.X, point.Z); 

            return coordinate;
        }


        public static implicit operator Point(Coordinate point)  
        {
            Point coordinate = new Point(point.Longitude, point.Latitude);  

            return coordinate;
        }

        public static implicit operator Point3D(Coordinate point) 
        {
            Point3D coordinate = new Point3D(point.Longitude, point.Latitude, point.Altitude ?? 0); 

            return coordinate;
        }
        #endregion
    }
}
