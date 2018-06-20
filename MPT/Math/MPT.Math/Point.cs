// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2017
// ***********************************************************************
// <copyright file="Point.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.Math.Coordinates;
using NMath = System.Math;

namespace MPT.Math
{
    /// <summary>
    /// Struct Point
    /// </summary>
    /// <seealso cref="System.IEquatable{Point}" />
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Point(double x, double y,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Returns the cross product/determinant of the points.
        /// x1*y2 - x2*y1
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Double.</returns>
        public double CrossProduct(Point point)
        {
            return VectorLibrary.CrossProduct(X, Y, point.X, point.Y);
        }

        /// <summary>
        /// Returns the dot product of the points.
        /// x1*x2 + y1*y2
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(Point point)
        {
            return VectorLibrary.DotProduct(X, Y, point.X, point.Y);
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Point other)
        {
            return (NMath.Abs(X - other.X) < Tolerance) &&
                   (NMath.Abs(Y - other.Y) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Point) { return Equals((Point)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Point a, Point b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset operator -(Point point1, Point point2)
        {
            return new Offset(
                point1, 
                point2,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }
        // TODO: Finish. Also for 3D & Cartesian, Angle, Polar.
        //public static Point operator -(Point point1, Offset point2)
        //{
        //    return new Point(
        //        point1,
        //        point2,
        //        NMath.Min(point1.Tolerance, point2.Tolerance));
        //}

        //public static Point operator -(Offset point1, Point point2)
        //{
        //    return new Point(
        //        point1,
        //        point2,
        //        NMath.Min(point1.Tolerance, point2.Tolerance));
        //}

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point operator +(Point point1, Point point2)
        {
            return new Point(
                point1.X + point2.X, 
                point1.Y + point2.Y,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }
        // TODO: Finish. Also for 3D & Cartesian, Angle, Polar.
        //public static Point operator +(Point point1, Offset point2)
        //{
        //    return new Point(
        //        point1,
        //        point2,
        //        NMath.Min(point1.Tolerance, point2.Tolerance));
        //}

        //public static Point operator +(Offset point1, Point point2)
        //{
        //    return new Point(
        //        point1,
        //        point2,
        //        NMath.Min(point1.Tolerance, point2.Tolerance));
        //}

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Point operator *(Point point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static Point operator *(double scale, Point point1)
        {
            return new Point(
                point1.X * scale,
                point1.Y * scale,
                point1.Tolerance);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Point operator /(Point point1, double scale)
        {
            return new Point(
                point1.X / scale, 
                point1.Y / scale,
                point1.Tolerance);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator *(Point point1, Point point2)
        {
            return point1.DotProduct(point2);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="CartesianCoordinate"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator CartesianCoordinate(Point a)
        {
            return new CartesianCoordinate(a.X, a.Y, a.Tolerance);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CartesianCoordinate"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(CartesianCoordinate b)
        {
            return new Point(b.X, b.Y, b.Tolerance);
        }
        #endregion
    }
}
