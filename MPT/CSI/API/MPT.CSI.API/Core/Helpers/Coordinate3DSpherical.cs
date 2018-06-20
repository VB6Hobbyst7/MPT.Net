// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate3DSpherical.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// 3D-coordinate by spherical values.
    /// </summary>
    public struct Coordinate3DSpherical
    {
        /// <summary>
        /// The radius coordinate. [L]
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// The plan angle coordinate.
        /// This angle is measured in the XY plane from the positive global X axis.
        /// When looking in the XY plane with the positive Z axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The theta.</value>
        public double Theta { get; set; }


        /// <summary>
        /// The elevation angle coordinate.
        /// This angle is measured in an X'Z plane that is perpendicular to the XY plane with the positive X' axis oriented at angle <see cref="Theta" /> from the positive global X axis.
        /// Angle is measured from the positive global Z axis.
        /// When looking in the X’Z plane with the positive Y' axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The phi.</value>
        public double Phi { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 3 entries.
        /// </summary>
        /// <param name="coordinates">1x3 matrix of values of spherical coordinates along the corresponding degree of freedom:
        /// Value(0) = <see cref="Radius" /> [L];
        /// Value(1) = <see cref="Theta" /> [deg];
        /// Value(2) = <see cref="Phi" /> [deg];</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + coordinates.Length + " elements when 3 elements was expected.</exception>
        public void FromArray(double[] coordinates)
        {
            if (coordinates.Length != 3)
            {
                throw new CSiException("Array has " + coordinates.Length + " elements when 3 elements was expected.");
            }
            Radius = coordinates[0];
            Theta = coordinates[1];
            Phi = coordinates[2];
        }

        /// <summary>
        /// Return a 1x3 matrix of booleans indicating spherical coordinate:
        /// Value(0) = <see cref="Radius" /> [L];
        /// Value(1) = <see cref="Theta" /> [deg];
        /// Value(2) = <see cref="Phi" /> [deg];
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] coordinates = { Radius, Theta, Phi };
            return coordinates;
        }
    }
}
