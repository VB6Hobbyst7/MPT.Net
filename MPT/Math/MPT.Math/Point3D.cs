// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2017
// ***********************************************************************
// <copyright file="Point3D.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math
{
    /// <summary>
    /// Struct Point3D
    /// </summary>
    /// <seealso cref="System.IEquatable{Point3D}" />
    public struct Point3D : IEquatable<Point3D>
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
        /// Gets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Point3D(double x, double y, double z,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Z = z;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Crosses the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>Point3D.</returns>
        public Point3D CrossProduct(Point3D point)
        {
            double[] matrix = VectorLibrary.CrossProduct(X, Y, Z, point.X, point.Y, point.Z);
            return new Point3D(matrix[0], matrix[1], matrix[2]);
        }

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(Point3D point)
        {
            return VectorLibrary.DotProduct(X, Y, Z, point.X, point.Y, point.Z);
        }


        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Point3D other)
        {
            return (NMath.Abs(X - other.X) < Tolerance) &&
                   (NMath.Abs(Y - other.Y) < Tolerance) &&
                   (NMath.Abs(Z - other.Z) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Point3D) { return Equals((Point3D)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Point3D a, Point3D b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Point3D a, Point3D b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset3D operator -(Point3D point1, Point3D point2)
        {
            return new Offset3D(
                point1, 
                point2,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator +(Point3D point1, Point3D point2)
        {
            return new Point3D(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator *(Point3D point1, double scale)
        {
            return new Point3D(point1.X * scale, point1.Y * scale, point1.Z * scale);
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator /(Point3D point1, double scale)
        {
            return new Point3D(point1.X / scale, point1.Y / scale, point1.Z / scale);
        }
        #endregion
    }
}
