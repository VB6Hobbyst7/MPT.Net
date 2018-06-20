// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-14-2017
// ***********************************************************************
// <copyright file="DegreesOfFreedomGlobal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Global degrees of freedom, the inclusion of which is indicated by the corresponding boolean.
    /// </summary>
    public struct DegreesOfFreedomGlobal
    {
        /// <summary>
        /// Translational degree-of-freedom along the X-axis.
        /// </summary>
        /// <value><c>true</c> if ux; otherwise, <c>false</c>.</value>
        public bool UX { get; set; }

        /// <summary>
        /// Translational degree-of-freedom along the Y-axis.
        /// </summary>
        /// <value><c>true</c> if uy; otherwise, <c>false</c>.</value>
        public bool UY { get; set; }


        /// <summary>
        /// Translational degree-of-freedom along the Z-axis.
        /// </summary>
        /// <value><c>true</c> if uz; otherwise, <c>false</c>.</value>
        public bool UZ { get; set; }


        /// <summary>
        /// Rotational degree-of-freedom along the X-axis.
        /// </summary>
        /// <value><c>true</c> if rx; otherwise, <c>false</c>.</value>
        public bool RX { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the Y-axis.
        /// </summary>
        /// <value><c>true</c> if ry; otherwise, <c>false</c>.</value>
        public bool RY { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the Z-axis.
        /// </summary>
        /// <value><c>true</c> if rz; otherwise, <c>false</c>.</value>
        public bool RZ { get; set; }

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 6 entries.
        /// </summary>
        /// <param name="degreesOfFreedom">1x6 matrix of booleans indicating inclusion of the corresponding degree of freedom:
        /// Value(0) = <see cref="UX" />;
        /// Value(1) = <see cref="UY" />;
        /// Value(2) = <see cref="UZ" />;
        /// Value(3) = <see cref="RX" />;
        /// Value(4) = <see cref="RY" />;
        /// Value(5) = <see cref="RZ" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + degreesOfFreedom.Length + " elements when 6 elements was expected.</exception>
        public void FromArray(bool[] degreesOfFreedom)
        {
            if (degreesOfFreedom.Length != 6) { throw new CSiException("Array has " + degreesOfFreedom.Length + " elements when 6 elements was expected."); }
            UX = degreesOfFreedom[0];
            UY = degreesOfFreedom[1];
            UZ = degreesOfFreedom[2];
            RX = degreesOfFreedom[3];
            RY = degreesOfFreedom[4];
            RZ = degreesOfFreedom[5];
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
        /// <returns>System.Boolean[].</returns>
        public bool[] ToArray()
        {
            bool[] degreesOfFreedom = { UX, UY, UZ, RX, RY, RZ};
            return degreesOfFreedom;
        }
    }
}
