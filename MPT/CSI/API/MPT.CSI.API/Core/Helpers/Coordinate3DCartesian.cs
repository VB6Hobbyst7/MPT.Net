// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate3DCartesian.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// 3D-coordinate by Cartesian values.
    /// </summary>
    public struct Coordinate3DCartesian
    {
        /// <summary>
        /// Coordinate along the X-axis. [L]
        /// </summary>
        /// <value>The x.</value>
        public double X { get; set; }

        /// <summary>
        /// Coordinate along the Y-axis. [L]
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; set; }


        /// <summary>
        /// Coordinate along the Z-axis. [L]
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 3 entries.
        /// </summary>
        /// <param name="coordinates">1x3 matrix of values of Cartesian coordinates along the corresponding degree of freedom:
        /// Value(0) = <see cref="X" /> [L];
        /// Value(1) = <see cref="Y" /> [L];
        /// Value(2) = <see cref="Z" /> [L];</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + coordinates.Length + " elements when 3 elements was expected.</exception>
        public void FromArray(double[] coordinates)
        {
            if (coordinates.Length != 3)
            {
                throw new CSiException("Array has " + coordinates.Length + " elements when 3 elements was expected.");
            }
            X = coordinates[0];
            Y = coordinates[1];
            Z = coordinates[2];
        }

        /// <summary>
        /// Return a 1x3 matrix of booleans indicating Cartesian coordinate:
        /// Value(0) = <see cref="X" /> [L];
        /// Value(1) = <see cref="Y" /> [L];
        /// Value(2) = <see cref="Z" /> [L];
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] coordinates = {X, Y, Z};
            return coordinates;
        }
    }
}
