// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-18-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate2DCartesian.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// 2D-coordinate by Cartesian values.
    /// </summary>
    public struct Coordinate2DCartesian
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
        /// Assigns array values to struct properties.
        /// Array must have 2 entries.
        /// </summary>
        /// <param name="coordinates">1x2 matrix of values of Cartesian coordinates along the corresponding degree of freedom:
        /// Value(0) = <see cref="X" /> [L];
        /// Value(1) = <see cref="Y" /> [L];</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + coordinates.Length + " elements when 2 elements was expected.</exception>
        public void FromArray(double[] coordinates)
        {
            if (coordinates.Length != 2)
            {
                throw new CSiException("Array has " + coordinates.Length + " elements when 2 elements was expected.");
            }
            X = coordinates[0];
            Y = coordinates[1];
        }

        /// <summary>
        /// Return a 1x2 matrix of booleans indicating Cartesian coordinate:
        /// Value(0) = <see cref="X" /> [L];
        /// Value(1) = <see cref="Y" /> [L];
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] coordinates = {X, Y};
            return coordinates;
        }
    }
}
