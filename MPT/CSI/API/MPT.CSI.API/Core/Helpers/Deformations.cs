// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-14-2017
// ***********************************************************************
// <copyright file="Deformations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Deformations along the local degrees-of-freedom.
    /// </summary>
    public struct Deformations
    {
        /// <summary>
        /// Translational deformation along the 1-axis [L].
        /// </summary>
        /// <value>The u1.</value>
        public double U1 { get; set; }

        /// <summary>
        /// Translational deformation along the 2-axis [L].
        /// </summary>
        /// <value>The u2.</value>
        public double U2 { get; set; }


        /// <summary>
        /// Translational deformation along the 3-axis [L].
        /// </summary>
        /// <value>The u3.</value>
        public double U3 { get; set; }


        /// <summary>
        /// Rotational deformation along the 1-axis [rad].
        /// </summary>
        /// <value>The r1.</value>
        public double R1 { get; set; }

        /// <summary>
        /// Rotational deformation along the 2-axis [rad].
        /// </summary>
        /// <value>The r2.</value>
        public double R2 { get; set; }

        /// <summary>
        /// Rotational deformation along the 3-axis [rad].
        /// </summary>
        /// <value>The r3.</value>
        public double R3 { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 6 entries.
        /// </summary>
        /// <param name="deformations">1x6 matrix of deformation values of the corresponding degree of freedom:
        /// Value(0) = <see cref="U1" />;
        /// Value(1) = <see cref="U2" />;
        /// Value(2) = <see cref="U3" />;
        /// Value(3) = <see cref="R1" />;
        /// Value(4) = <see cref="R2" />;
        /// Value(5) = <see cref="R3" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + deformations.Length + " elements when 6 elements was expected.</exception>
        public void FromArray(double[] deformations)
        {
            if (deformations.Length != 6) { throw new CSiException("Array has " + deformations.Length + " elements when 6 elements was expected."); }
            U1 = deformations[0];
            U2 = deformations[1];
            U3 = deformations[2];
            R1 = deformations[3];
            R2 = deformations[4];
            R3 = deformations[5];
        }

        /// <summary>
        /// Return a 1x6 matrix of deformation values of the corresponding degree of freedom:
        /// Value(0) = <see cref="U1" />;
        /// Value(1) = <see cref="U2" />;
        /// Value(2) = <see cref="U3" />;
        /// Value(3) = <see cref="R1" />;
        /// Value(4) = <see cref="R2" />;
        /// Value(5) = <see cref="R3" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] deformations = { U1, U2, U3, R1, R2, R3};
            return deformations;
        }
    }
}
