// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate3DCylindrical.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// 3D-coordinate by cylindrical values.
    /// </summary>
    public struct Coordinate3DCylindrical
    {
        /// <summary>
        /// The radius coordinate. [L]
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// The angle coordinate for the specified point.
        /// The angle is measured in the XY plane from the positive X axis.
        /// When looking in the XY plane with the positive Z axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The theta.</value>
        public double Theta { get; set; }


        /// <summary>
        /// Coordinate along the Z-axis. [L]
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 3 entries.
        /// </summary>
        /// <param name="coordinates">1x3 matrix of values of cylindrical coordinates along the corresponding degree of freedom:
        /// Value(0) = <see cref="Radius" /> [L];
        /// Value(1) = <see cref="Theta" /> [deg];
        /// Value(2) = <see cref="Z" /> [L];</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + coordinates.Length + " elements when 3 elements was expected.</exception>
        public void FromArray(double[] coordinates)
        {
            if (coordinates.Length != 3)
            {
                throw new CSiException("Array has " + coordinates.Length + " elements when 3 elements was expected.");
            }
            Radius = coordinates[0];
            Theta = coordinates[1];
            Z = coordinates[2];
        }

        /// <summary>
        /// Return a 1x3 matrix of booleans indicating cylindrical coordinate:
        /// Value(0) = <see cref="Radius" /> [L];
        /// Value(1) = <see cref="Theta" /> [deg];
        /// Value(2) = <see cref="Z" /> [L];
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] coordinates = { Radius, Theta, Z };
            return coordinates;
        }
    }
}
