// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="Stress.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Internal stresses oriented along local axes.
    /// </summary>
    public struct Stress
    {
        /// <summary>
        /// In-plane normal stress along the local 1-1-axis [F/L^2].
        /// </summary>
        /// <value>The S11.</value>
        public double S11 { get; set; }

        /// <summary>
        /// In-plane normal stress along the local 2-2-axis [F/L^2].
        /// </summary>
        /// <value>The S22.</value>
        public double S22 { get; set; }

        /// <summary>
        /// Normal stress along the local 3-3-axis [F/L^2].
        /// </summary>
        /// <value>The S33.</value>
        public double S33 { get; set; }


        /// <summary>
        /// In-plane shear stress along the local 1-2-axis [F/L^2].
        /// </summary>
        /// <value>The S12.</value>
        public double S12 { get; set; }

        /// <summary>
        /// Out-of-plane shear Stress along the local 1-3-axis [F/L^2].
        /// </summary>
        /// <value>The S13.</value>
        public double S13 { get; set; }

        /// <summary>
        /// Out-of-plane shear stress along the local 2-3-axis [F/L^2].
        /// </summary>
        /// <value>The S23.</value>
        public double S23 { get; set; }


        /// <summary>
        /// Maximum principal stress [F/L^2].
        /// </summary>
        /// <value>The s maximum.</value>
        public double SMax { get; set; }

        /// <summary>
        /// Minimum principal stress [F/L^2].
        /// </summary>
        /// <value>The s minimum.</value>
        public double SMin { get; set; }


        /// <summary>
        /// Von Mises stress [F/L^2].
        /// </summary>
        /// <value>The SVM.</value>
        public double SVM { get; set; }


        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal stress. [deg].
        /// </summary>
        /// <value>The angle.</value>
        public double Angle { get; set; }


        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 10 entries.
        /// </summary>
        /// <param name="stresses">1x10 matrix of stress values of the corresponding degree of freedom:
        /// Value(0) = <see cref="S11" />;
        /// Value(1) = <see cref="S22" />;
        /// Value(2) = <see cref="S33" />;
        /// Value(3) = <see cref="S12" />;
        /// Value(4) = <see cref="S13" />;
        /// Value(5) = <see cref="S23" />;
        /// Value(6) = <see cref="SMax" />;
        /// Value(7) = <see cref="SMin" />;
        /// Value(8) = <see cref="SVM" />;
        /// Value(9) = <see cref="Angle" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + stresses.Length + " elements when 10 elements was expected.</exception>
        public void FromArray(double[] stresses)
        {
            if (stresses.Length != 10) { throw new CSiException("Array has " + stresses.Length + " elements when 10 elements was expected."); }
            S11 = stresses[0];
            S22 = stresses[1];
            S33 = stresses[2];
            S12 = stresses[3];
            S13 = stresses[4];
            S23 = stresses[5];
            SMax = stresses[6];
            SMin = stresses[7];
            SVM = stresses[8];
            Angle = stresses[9];
        }

        /// <summary>
        /// Return a 1x10 matrix of stress values of the corresponding degree of freedom:
        /// Value(0) = <see cref="S11" />;
        /// Value(1) = <see cref="S22" />;
        /// Value(2) = <see cref="S33" />;
        /// Value(3) = <see cref="S12" />;
        /// Value(4) = <see cref="S13" />;
        /// Value(5) = <see cref="S23" />;
        /// Value(6) = <see cref="SMax" />;
        /// Value(7) = <see cref="SMin" />;
        /// Value(8) = <see cref="SVM" />;
        /// Value(9) = <see cref="Angle" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] stresses = { S11, S22, S33, S12, S13, S23, SMax, SMin, SVM, Angle };
            return stresses;
        }
    }
}
