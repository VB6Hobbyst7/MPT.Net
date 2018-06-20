// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2017
// ***********************************************************************
// <copyright file="Offset3D.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math
{
    /// <summary>
    /// Represents the difference between 3D points I (first) and J (second) in three-dimensional space.
    /// </summary>
    /// <seealso cref="System.IEquatable{Offset3D}" />
    public struct Offset3D : IEquatable<Offset3D>
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets or sets the first coordinate value of this Point structure.
        /// </summary>
        /// <value>The i.</value>
        public Point3D I { get; private set; }

        /// <summary>
        /// Gets or sets the second coordinate value of this Point structure.
        /// </summary>
        /// <value>The j.</value>
        public Point3D J { get; private set; }

        /// <summary>
        /// Xj - Xi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double X()
        {
            return (J.X - I.X);
        }

        /// <summary>
        /// Yj - Yi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Y()
        {
            return (J.Y - I.Y);
        }

        /// <summary>
        /// Zj - Zi.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Z()
        {
            return (J.Z - I.Z);
        }


        /// <summary>
        /// The total straight length of the offset.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return Algebra.SRSS(X(), Y(), Z());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Offset3D(Point3D i, Point3D j,
            double tolerance = Numbers.ZeroTolerance)
        {
            I = i;
            J = j;
            Tolerance = tolerance;
        }

        /// <summary>
        /// To the point.
        /// </summary>
        /// <returns>Point3D.</returns>
        public Point3D ToPoint()
        {
            return new Point3D(
                J.X - I.X,
                J.Y - I.Y,
                J.Z - I.Z,
                Tolerance);
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Offset3D other)
        {
            return ((NMath.Abs(I.X - other.I.X) < Tolerance) &&
                    (NMath.Abs(I.Y - other.I.Y) < Tolerance) &&
                    (NMath.Abs(J.X - other.J.X) < Tolerance) &&
                    (NMath.Abs(J.Y - other.J.Y) < Tolerance));
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Offset3D) { return Equals((Offset3D)obj); }
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
        public static bool operator ==(Offset3D a, Offset3D b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Offset3D a, Offset3D b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset3D operator -(Offset3D point1, Offset3D point2)
        {
            return point1.ToPoint() - point2.ToPoint();
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator -(Point3D point1, Offset3D point2)
        {
            return point2 - point1;
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator -(Offset3D point1, Point3D point2)
        {
            return new Point3D(
                point1.X() - point2.X,
                point1.Y() - point2.Y,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset3D operator +(Offset3D point1, Offset3D point2)
        {
            return new Offset3D(
                new Point3D(
                    point1.X() + point2.X(),
                    point1.Y() + point2.Y(),
                    point1.Z() + point2.Z()),
                new Point3D(),
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator +(Point3D point1, Offset3D point2)
        {
            return point2 + point1;
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static Point3D operator +(Offset3D point1, Point3D point2)
        {
            return new Point3D(
                point1.X() + point2.X,
                point1.Y() + point2.Y,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset3D operator *(Offset3D point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static Offset3D operator *(double scale, Offset3D point1)
        {
            return new Offset3D(
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
        public static Offset3D operator /(Offset3D point1, double scale)
        {
            return new Offset3D(
                point1.I / scale,
                point1.J / scale,
                point1.Tolerance);
        }
        #endregion
    }
}
