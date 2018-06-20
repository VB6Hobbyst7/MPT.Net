// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="Loads.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// External loads/forces or values associated with forces oriented along axes.
    /// </summary>
    public struct Loads
    {
        /// <summary>
        /// Translational force in the local 1-axis or coordinate system X-axis direction [F].
        /// </summary>
        /// <value>The f1.</value>
        public double F1 { get; set; }

        /// <summary>
        /// Translational force in the local 2-axis or coordinate system Y-axis direction [F].
        /// </summary>
        /// <value>The f2.</value>
        public double F2 { get; set; }


        /// <summary>
        /// Translational force in the local 3-axis or coordinate system Z-axis direction [F].
        /// </summary>
        /// <value>The f3.</value>
        public double F3 { get; set; }


        /// <summary>
        /// Moment about the local 1-axis or coordinate system X-axis [F*L].
        /// </summary>
        /// <value>The m1.</value>
        public double M1 { get; set; }

        /// <summary>
        /// Moment about the local 2-axis or coordinate system Yaxis [F*L].
        /// </summary>
        /// <value>The m2.</value>
        public double M2 { get; set; }

        /// <summary>
        /// Moment about the local 3-axis or coordinate system Z-axis [F*L].
        /// </summary>
        /// <value>The m3.</value>
        public double M3 { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 6 entries.
        /// </summary>
        /// <param name="forces">1x6 matrix of force values of the corresponding degree of freedom:
        /// Value(0) = <see cref="F1" />;
        /// Value(1) = <see cref="F2" />;
        /// Value(2) = <see cref="F3" />;
        /// Value(3) = <see cref="M1" />;
        /// Value(4) = <see cref="M2" />;
        /// Value(5) = <see cref="M3" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + forces.Length + " elements when 6 elements was expected.</exception>
        public void FromArray(double[] forces)
        {
            if (forces.Length != 6) { throw new CSiException("Array has " + forces.Length + " elements when 6 elements was expected."); }
            F1 = forces[0];
            F2 = forces[1];
            F3 = forces[2];
            M1 = forces[3];
            M2 = forces[4];
            M3 = forces[5];
        }

        /// <summary>
        /// Return a 1x6 matrix of force values of the corresponding degree of freedom:
        /// Value(0) = <see cref="F1" />;
        /// Value(1) = <see cref="F2" />;
        /// Value(2) = <see cref="F3" />;
        /// Value(3) = <see cref="M1" />;
        /// Value(4) = <see cref="M2" />;
        /// Value(5) = <see cref="M3" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] forces = { F1, F2, F3, M1, M2, M3 };
            return forces;
        }
    }
}
