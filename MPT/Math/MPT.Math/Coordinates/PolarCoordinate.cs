// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="PolarCoordinate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// Struct PolarCoordinate
    /// </summary>
    /// <seealso cref="System.IEquatable{PolarCoordinate}" />
    public struct PolarCoordinate : IEquatable<PolarCoordinate>
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; private set; }


        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public Angle Rotation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarCoordinate"/> struct.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="tolerance">The tolerance.</param>
        public PolarCoordinate(
            double radius,
            Angle rotation,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Rotation = rotation;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarCoordinate"/> struct.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="tolerance">The tolerance.</param>
        public PolarCoordinate(
            double radius,
            double rotation,
            double tolerance = Numbers.ZeroTolerance)
        {
            Radius = radius;
            Rotation = rotation;
            Tolerance = tolerance;
        }

        /// <summary>
        /// To the cartesian.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate ToCartesian()
        {
            return new CartesianCoordinate(Radius*NMath.Cos(Rotation.Radians), Radius*NMath.Sin(Rotation.Radians), Tolerance);
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(PolarCoordinate other)
        {
            return (NMath.Abs(Radius - other.Radius) < Tolerance) &&
                   (NMath.Abs((Rotation - other.Rotation).Radians) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is PolarCoordinate) { return Equals((PolarCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Radius.GetHashCode() ^ Rotation.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PolarCoordinate a, PolarCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PolarCoordinate a, PolarCoordinate b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator -(PolarCoordinate point1, PolarCoordinate point2)
        {
            return new PolarCoordinate(
                point1.Radius - point2.Radius,
                point1.Rotation - point2.Rotation,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator +(PolarCoordinate point1, PolarCoordinate point2)
        {
            return new PolarCoordinate(
                point1.Radius + point2.Radius,
                point1.Rotation + point2.Rotation,
                NMath.Min(point1.Tolerance, point2.Tolerance));
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator *(PolarCoordinate point1, double scale)
        {
            return scale * point1;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="point1">The point1.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator *(double scale, PolarCoordinate point1)
        {
            return new PolarCoordinate(
                point1.Radius * scale,
                point1.Rotation * scale,
                point1.Tolerance);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The result of the operator.</returns>
        public static PolarCoordinate operator /(PolarCoordinate point1, double scale)
        {
            return new PolarCoordinate(
                point1.Radius / scale,
                point1.Rotation / scale,
                point1.Tolerance);
        }

        #endregion
    }
}
