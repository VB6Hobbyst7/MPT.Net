// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-20-2017
// ***********************************************************************
// <copyright file="Displacements.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Displacements or offsets along the Global degrees of freedom.
    /// </summary>
    public struct Displacements
    {
        /// <summary>
        /// Translational displacement/offset along the X-axis.
        /// </summary>
        /// <value>The ux.</value>
        public double UX { get; set; }

        /// <summary>
        /// Translational displacement/offset along the Y-axis.
        /// </summary>
        /// <value>The uy.</value>
        public double UY { get; set; }


        /// <summary>
        /// Translational displacement/offset along the Z-axis.
        /// </summary>
        /// <value>The uz.</value>
        public double UZ { get; set; }


        /// <summary>
        /// Rotational displacement/offset along the X-axis.
        /// </summary>
        /// <value>The rx.</value>
        public double RX { get; set; }

        /// <summary>
        /// Rotational displacement/offset along the Y-axis.
        /// </summary>
        /// <value>The ry.</value>
        public double RY { get; set; }

        /// <summary>
        /// Rotational displacement/offset along the Z-axis.
        /// </summary>
        /// <value>The rz.</value>
        public double RZ { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 6 entries.
        /// </summary>
        /// <param name="displacements">1x6 matrix of values of displacements/offsets along the corresponding degree of freedom:
        /// Value(0) = <see cref="UX" />;
        /// Value(1) = <see cref="UY" />;
        /// Value(2) = <see cref="UZ" />;
        /// Value(3) = <see cref="RX" />;
        /// Value(4) = <see cref="RY" />;
        /// Value(5) = <see cref="RZ" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + displacements.Length + " elements when 6 elements was expected.</exception>
        public void FromArray(double[] displacements)
        {
            if (displacements.Length != 6) { throw new CSiException("Array has " + displacements.Length + " elements when 6 elements was expected."); }
            UX = displacements[0];
            UY = displacements[1];
            UZ = displacements[2];
            RX = displacements[3];
            RY = displacements[4];
            RZ = displacements[5];
        }

        /// <summary>
        /// Return a 1x6 matrix of booleans indicating inclusion of the corresponding degree of freedom:
        /// Value(0) = <see cref="UX" />;
        /// Value(1) = <see cref="UY" />;
        /// Value(2) = <see cref="UZ" />;
        /// Value(3) = <see cref="RX" />;
        /// Value(4) = <see cref="RY" />;
        /// Value(5) = <see cref="RZ" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] displacements = { UX, UY, UZ, RX, RY, RZ};
            return displacements;
        }
    }
}
