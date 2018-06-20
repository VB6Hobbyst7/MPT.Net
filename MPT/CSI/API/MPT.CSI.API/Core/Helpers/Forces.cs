// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="Forces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Internal forces or values associated with forces.
    /// </summary>
    public struct Forces
    {
        /// <summary>
        /// Axial force. [F].
        /// </summary>
        /// <value>The p.</value>
        public double P { get; set; }

        /// <summary>
        /// Major axis shear. [F].
        /// </summary>
        /// <value>The v2.</value>
        public double V2 { get; set; }


        /// <summary>
        /// Minor axis shear. [F].
        /// </summary>
        /// <value>The v3.</value>
        public double V3 { get; set; }


        /// <summary>
        /// Torsion [F*L].
        /// </summary>
        /// <value>The t.</value>
        public double T { get; set; }

        /// <summary>
        /// Minor axis bending. [F*L].
        /// </summary>
        /// <value>The m2.</value>
        public double M2 { get; set; }

        /// <summary>
        /// Major axis bending [F*L].
        /// </summary>
        /// <value>The m3.</value>
        public double M3 { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 6 entries.
        /// </summary>
        /// <param name="forces">1x6 matrix of internal force values of the corresponding degree of freedom:
        /// Value(0) = <see cref="P" />;
        /// Value(1) = <see cref="V2" />;
        /// Value(2) = <see cref="V3" />;
        /// Value(3) = <see cref="T" />;
        /// Value(4) = <see cref="M2" />;
        /// Value(5) = <see cref="M3" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + forces.Length + " elements when 6 elements was expected.</exception>
        public void FromArray(double[] forces)
        {
            if (forces.Length != 6) { throw new CSiException("Array has " + forces.Length + " elements when 6 elements was expected."); }
            P = forces[0];
            V2 = forces[1];
            V3 = forces[2];
            T = forces[3];
            M2 = forces[4];
            M3 = forces[5];
        }

        /// <summary>
        /// Return a 1x6 matrix of internal force values of the corresponding degree of freedom:
        /// Value(0) = <see cref="P" />;
        /// Value(1) = <see cref="V2" />;
        /// Value(2) = <see cref="V3" />;
        /// Value(3) = <see cref="T" />;
        /// Value(4) = <see cref="M2" />;
        /// Value(5) = <see cref="M3" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] forces = { P, V2, V3, T, M2, M3};
            return forces;
        }
    }
}
