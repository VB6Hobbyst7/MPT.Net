using System;
using NMath = System.Math;

using MPT.Math;

namespace MPT.GIS
{
    public class CoordinateOffset : IEquatable<CoordinateOffset>
    {

        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets or sets the first coordinate value of this Coordinate structure.
        /// </summary>
        /// <value>The i.</value>
        public Coordinate I { get; private set; }

        /// <summary>
        /// Gets or sets the second coordinate value of this Coordinate structure.
        /// </summary>
        /// <value>The j.</value>
        public Coordinate J { get; private set; }

        /// <summary>
        /// Longitude_j - Longitude_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Longitude()
        {
            return (J.Longitude - I.Longitude);
        }

        /// <summary>
        /// Latitude_j - Latitude_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Latitude()
        {
            return (J.Latitude - I.Latitude);
        }

        /// <summary>
        /// Altitude_j - Altitude_i.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double? Altitude()
        {
            if (J.Altitude.HasValue && I.Altitude.HasValue)
                return (J.Altitude - I.Altitude);
            return null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset"/> struct.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CoordinateOffset(Coordinate i, Coordinate j,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = i;
            J = j;
            Tolerance = tolerance;
        }

        /// <summary>
        /// To the point.
        /// </summary>
        /// <returns>Point.</returns>
        public Coordinate ToCoordinate()
        {
            if (Altitude().HasValue)
            {
                return new Coordinate(
                Latitude(),
                Longitude(),
                Altitude().GetValueOrDefault());
            }
            return new Coordinate(
                Latitude(),
                Longitude());
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CoordinateOffset other)
        {
            if ((other != null) &&
                ((NMath.Abs(I.Longitude - other.I.Longitude) < Tolerance) &&
                 (NMath.Abs(I.Latitude - other.I.Latitude) < Tolerance) &&
                 (NMath.Abs(J.Longitude - other.J.Longitude) < Tolerance) &&
                 (NMath.Abs(J.Latitude - other.J.Latitude) < Tolerance)))
            {
                return (I.Altitude == other.I.Altitude &&
                        J.Altitude == other.J.Altitude);
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
            if (obj is CoordinateOffset) { return Equals((CoordinateOffset)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return I.GetHashCode() ^ J.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CoordinateOffset a, CoordinateOffset b)
        {
            return ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CoordinateOffset a, CoordinateOffset b)
        {
            return ReferenceEquals(a, null) ? !ReferenceEquals(b, null) : !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator -(CoordinateOffset point1, CoordinateOffset point2)
        {
            return point1.ToCoordinate() - point2.ToCoordinate();
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator +(CoordinateOffset point1, CoordinateOffset point2)
        {
            Coordinate coordinate;
            if (point1.Altitude().HasValue && point2.Altitude().HasValue)
            {
                coordinate = new Coordinate(
                    point1.Longitude() + point2.Longitude(),
                    point1.Latitude() + point2.Latitude(),
                    point1.Altitude().GetValueOrDefault() + point2.Altitude().GetValueOrDefault());
            }
            else
            {
                coordinate = new Coordinate(
                    point1.Longitude() + point2.Longitude(),
                    point1.Latitude() + point2.Latitude());
            }
            return new CoordinateOffset(
                coordinate,
                new Coordinate(),
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator *(CoordinateOffset point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator *(double scale, CoordinateOffset point1)
        {
            return new CoordinateOffset(
                point1.I * scale,
                point1.J * scale,
                point1.Tolerance);
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static CoordinateOffset operator /(CoordinateOffset point1, double scale)
        {
            return new CoordinateOffset(
                point1.I / scale,
                point1.J / scale,
                point1.Tolerance);
        }
        #endregion
    }
}
