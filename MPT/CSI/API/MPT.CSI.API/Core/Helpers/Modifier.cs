// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Modifier.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Stiffness, weight, and mass modifiers.
    /// The default value for all modifiers is one.
    /// </summary>
    public class Modifier // Note: This is a class and not a struct becuase default values must be set to 1.
    {
        /// <summary>
        /// Membrane stiffness modifier along the 1-1 local plane.
        /// </summary>
        /// <value>The membrane F11.</value>
        public double MembraneF11 { get; set; } = 1;

        /// <summary>
        /// Membrane stiffness modifier along the 2-2 local plane.
        /// </summary>
        /// <value>The membrane F22.</value>
        public double MembraneF22 { get; set; } = 1;

        /// <summary>
        /// Membrane stiffness modifier along the 1-2 local plane.
        /// </summary>
        /// <value>The membrane F12.</value>
        public double MembraneF12 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 1-1 local plane.
        /// </summary>
        /// <value>The bending M11.</value>
        public double BendingM11 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 2-2 local plane.
        /// </summary>
        /// <value>The bending M22.</value>
        public double BendingM22 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 1-2 local plane.
        /// </summary>
        /// <value>The bending M12.</value>
        public double BendingM12 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 1-3 local plane.
        /// </summary>
        /// <value>The shear V13.</value>
        public double ShearV13 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 2-3 local plane.
        /// </summary>
        /// <value>The shear V23.</value>
        public double ShearV23 { get; set; } = 1;

        /// <summary>
        /// Mass modifier.
        /// </summary>
        /// <value>The mass modifier.</value>
        public double MassModifier { get; set; } = 1;

        /// <summary>
        /// Weight modifier.
        /// </summary>
        /// <value>The weight modifier.</value>
        public double WeightModifier { get; set; } = 1;

        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 10 entries.
        /// </summary>
        /// <param name="modifiers">1x10 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="MembraneF11" />;
        /// Value(1) = <see cref="MembraneF22" />;
        /// Value(2) = <see cref="MembraneF12" />;
        /// Value(3) = <see cref="BendingM11" />;
        /// Value(4) = <see cref="BendingM22" />;
        /// Value(5) = <see cref="BendingM12" />;
        /// Value(6) = <see cref="ShearV13" />;
        /// Value(7) = <see cref="ShearV23" />;
        /// Value(8) = <see cref="MassModifier" />;
        /// Value(9) = <see cref="WeightModifier" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + modifiers.Length + " elements when 10 elements was expected.</exception>
        public void FromArray(double[] modifiers)
        {
            if (modifiers.Length != 10) { throw new CSiException("Array has " + modifiers.Length + " elements when 10 elements was expected."); }
            MembraneF11 = modifiers[0];
            MembraneF22 = modifiers[1];
            MembraneF12 = modifiers[2];
            BendingM11 = modifiers[3];
            BendingM22 = modifiers[4];
            BendingM12 = modifiers[5];
            ShearV13 = modifiers[6];
            ShearV23 = modifiers[7];
            MassModifier = modifiers[8];
            WeightModifier = modifiers[9];
        }

        /// <summary>
        /// Return a 1x10 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="MembraneF11" />;
        /// Value(1) = <see cref="MembraneF22" />;
        /// Value(2) = <see cref="MembraneF12" />;
        /// Value(3) = <see cref="BendingM11" />;
        /// Value(4) = <see cref="BendingM22" />;
        /// Value(5) = <see cref="BendingM12" />;
        /// Value(6) = <see cref="ShearV13" />;
        /// Value(7) = <see cref="ShearV23" />;
        /// Value(8) = <see cref="MassModifier" />;
        /// Value(9) = <see cref="WeightModifier" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] modifiers = {  MembraneF11, MembraneF22, MembraneF12,
                                    BendingM11, BendingM22, BendingM12,
                                    ShearV13, ShearV23,
                                    MassModifier, WeightModifier};
            return modifiers;
        }
    }
}
