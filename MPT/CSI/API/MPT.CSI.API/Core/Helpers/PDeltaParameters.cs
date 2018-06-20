// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="PDeltaParameters.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// The P-delta moment parameters at each end of the line element used in the application.
    /// </summary>
    public struct PDeltaParameters
    {
        /// <summary>
        /// M2 P-delta to I-end of link as moment, M2I.
        /// </summary>
        /// <value>The m2 i.</value>
        public double M2I { get; set; }

        /// <summary>
        /// M2 P-delta to J-end of link as moment, M2J.
        /// </summary>
        /// <value>The m2 j.</value>
        public double M2J { get; set; }


        /// <summary>
        /// M3 P-delta to I-end of link as moment, M3I.
        /// </summary>
        /// <value>The m3 i.</value>
        public double M3I { get; set; }


        /// <summary>
        /// M3 P-delta to J-end of link as moment, M3J.
        /// </summary>
        /// <value>The m3 j.</value>
        public double M3J { get; set; }



        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 4 entries.
        /// </summary>
        /// <param name="pDeltaParameters">1x4 matrix of P-delta parameters:
        /// Value(0) = <see cref="M2I" />;
        /// Value(1) = <see cref="M2J" />;
        /// Value(2) = <see cref="M3I" />;
        /// Value(3) = <see cref="M3J" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + pDeltaParameters.Length + " elements when 4 elements was expected.</exception>
        public void FromArray(double[] pDeltaParameters)
        {
            if (pDeltaParameters.Length != 4) { throw new CSiException("Array has " + pDeltaParameters.Length + " elements when 4 elements was expected."); }
            M2I = pDeltaParameters[0];
            M2J = pDeltaParameters[1];
            M3I = pDeltaParameters[2];
            M3J = pDeltaParameters[3];
        }

        /// <summary>
        /// Return a 1x4 matrix of P-delta parameters:
        /// Value(0) = <see cref="M2I" />;
        /// Value(1) = <see cref="M2J" />;
        /// Value(2) = <see cref="M3I" />;
        /// Value(3) = <see cref="M3J" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] pDeltaParameters = { M2I, M2J, M3I, M3J };
            return pDeltaParameters;
        }
    }
}
