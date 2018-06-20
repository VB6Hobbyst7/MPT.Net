// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2017
// ***********************************************************************
// <copyright file="CartesianCoordinate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;


namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Struct CartesianCoordinate
    /// </summary>
    /// <seealso cref="System.IEquatable{CartesianCoordinate}" />
    public struct CartesianCoordinate : IEquatable<CartesianCoordinate>
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
        /// Initializes a new instance of the <see cref="CartesianCoordinate"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tolerance">The tolerance.</param>
        public CartesianCoordinate(double x, double y,
            double tolerance = Numbers.ZeroTolerance)
        {
            X = x;
            Y = y;
            Tolerance = tolerance;
        }


        /// <summary>
        /// To the polar.
        /// </summary>
        /// <returns>PolarCoordinate.</returns>
        public PolarCoordinate ToPolar()
        {
            return new PolarCoordinate(
                radius: Algebra.SRSS(X, Y),
                rotation: new Angle(NMath.Atan(Y/X))
                );
        }

        /// <summary>
        /// To the barycentric.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>BarycentricCoordinate.</returns>
        public BarycentricCoordinate ToBarycentric(Point vertexA, Point vertexB, Point vertexC)
        {
            double determinate = (vertexB.Y - vertexC.Y) * (vertexA.X - vertexC.X) +
                                 (vertexC.X - vertexB.X) * (vertexA.Y - vertexC.Y);

            double alpha = ((vertexB.Y - vertexC.Y) * (X - vertexC.X) +
                            (vertexC.X - vertexB.X) * (Y - vertexC.Y)) / determinate;

            double beta = ((vertexC.Y - vertexA.Y) * (X - vertexC.X) +
                            (vertexA.X - vertexC.X) * (Y - vertexC.Y)) / determinate;

            double gamma = 1 - alpha - beta;

            return new BarycentricCoordinate(alpha, beta, gamma);
        }

        /// <summary>
        /// To the trilinear.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>TrilinearCoordinate.</returns>
        public TrilinearCoordinate ToTrilinear(Point vertexA, Point vertexB, Point vertexC)
        {
            BarycentricCoordinate barycentric = ToBarycentric(vertexA, vertexB, vertexC);
            return barycentric.ToTrilinear(vertexA, vertexB, vertexC);
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CartesianCoordinate other)
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
            if (obj is CartesianCoordinate) { return Equals((CartesianCoordinate)obj); }
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
        public static bool operator ==(CartesianCoordinate a, CartesianCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CartesianCoordinate a, CartesianCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset operator -(CartesianCoordinate point1, CartesianCoordinate point2)
        {
            return new Offset(
                (Point)point1,
                (Point)point2,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator +(CartesianCoordinate point1, CartesianCoordinate point2)
        {
            return new CartesianCoordinate(
                point1.X + point2.X,
                point1.Y + point2.Y,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(CartesianCoordinate point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static CartesianCoordinate operator *(double scale, CartesianCoordinate point1)
        {
            return new CartesianCoordinate(
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
        public static CartesianCoordinate operator /(CartesianCoordinate point1, double scale)
        {
            return new CartesianCoordinate(
                point1.X / scale,
                point1.Y / scale,
                point1.Tolerance);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="CartesianCoordinate"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Point(CartesianCoordinate a)
        {
            return new Point(a.X, a.Y, a.Tolerance);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="CartesianCoordinate"/>.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CartesianCoordinate(Point b)
        {
            return new CartesianCoordinate(b.X, b.Y, b.Tolerance);
        }
        #endregion
    }
}
