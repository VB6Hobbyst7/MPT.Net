// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="StiffnessPendulum.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Stiffness values for each face of a triple pendulum isolator.
    /// </summary>
    public struct StiffnessPendulum
    {
        /// <summary>
        /// Translational stiffness along the outer top sliding surface. [F/L].
        /// </summary>
        /// <value>The outer top sliding surface.</value>
        public double OuterTopSlidingSurface { get; set; }

        /// <summary>
        /// Translational stiffness along outer bottom sliding surface. [F/L].
        /// </summary>
        /// <value>The outer bottom sliding surface.</value>
        public double OuterBottomSlidingSurface { get; set; }


        /// <summary>
        /// Translational stiffness along the inner top sliding surface. [F/L].
        /// </summary>
        /// <value>The inner top sliding surface.</value>
        public double InnerTopSlidingSurface { get; set; }


        /// <summary>
        /// Rotational stiffness along the inner bottom sliding surface. [F/L].
        /// </summary>
        /// <value>The inner bottom sliding surface.</value>
        public double InnerBottomSlidingSurface { get; set; }


        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 4 entries.
        /// </summary>
        /// <param name="stiffnesses">1x6 matrix of stiffness values of the corresponding isolator surface:
        /// Value(0) = <see cref="OuterTopSlidingSurface" />;
        /// Value(1) = <see cref="OuterBottomSlidingSurface" />;
        /// Value(2) = <see cref="InnerTopSlidingSurface" />;
        /// Value(3) = <see cref="InnerBottomSlidingSurface" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + stiffnesses.Length + " elements when 4 elements was expected.</exception>
        public void FromArray(double[] stiffnesses)
        {
            if (stiffnesses.Length != 4) { throw new CSiException("Array has " + stiffnesses.Length + " elements when 4 elements was expected."); }
            OuterTopSlidingSurface = stiffnesses[0];
            OuterBottomSlidingSurface = stiffnesses[1];
            InnerTopSlidingSurface = stiffnesses[2];
            InnerBottomSlidingSurface = stiffnesses[3];
        }

        /// <summary>
        /// Return a 1x4 matrix of stiffness values of the corresponding isolator surface:
        /// Value(0) = <see cref="OuterTopSlidingSurface" />;
        /// Value(1) = <see cref="OuterBottomSlidingSurface" />;
        /// Value(2) = <see cref="InnerTopSlidingSurface" />;
        /// Value(3) = <see cref="InnerBottomSlidingSurface" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] stiffnesses = { OuterTopSlidingSurface, OuterBottomSlidingSurface, InnerTopSlidingSurface, InnerBottomSlidingSurface };
            return stiffnesses;
        }
    }
}

#endif