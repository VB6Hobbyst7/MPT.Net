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
    /// Stiffness, weight, and mass modifiers for frames.
    /// The default value for all modifiers is one.
    /// </summary>
    public class FrameModifier // Note: This is a class and not a struct because default values must be set to 1.
    {
        /// <summary>
        /// Cross-sectional area modifier.
        /// </summary>
        /// <value></value>
        public double CrossSectionalArea { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 2 local axis.
        /// </summary>
        /// <value>The shear V2.</value>
        public double ShearV2 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 3 local axis.
        /// </summary>
        /// <value>The shear V3.</value>
        public double ShearV3 { get; set; } = 1;

        /// <summary>
        /// Torsion stiffness modifier.
        /// </summary>
        /// <value>The torsion stiffness modifier.</value>
        public double Torsion { get; set; } = 1;
        
        /// <summary>
        /// Bending stiffness modifier along the 2 local axis.
        /// </summary>
        /// <value>The bending M2.</value>
        public double BendingM2 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 3 local axis.
        /// </summary>
        /// <value>The bending M3.</value>
        public double BendingM3 { get; set; } = 1;
        

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
        /// Array must have 8 entries.
        /// </summary>
        /// <param name="modifiers">1x8 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="CrossSectionalArea" />;
        /// Value(1) = <see cref="ShearV2" />;
        /// Value(2) = <see cref="ShearV3" />;
        /// Value(3) = <see cref="Torsion" />;
        /// Value(4) = <see cref="BendingM2" />;
        /// Value(5) = <see cref="BendingM3" />;
        /// Value(6) = <see cref="MassModifier" />;
        /// Value(7) = <see cref="WeightModifier" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + modifiers.Length + " elements when 8 elements was expected.</exception>
        public void FromArray(double[] modifiers)
        {
            if (modifiers.Length != 8) { throw new CSiException("Array has " + modifiers.Length + " elements when 8 elements was expected."); }
            CrossSectionalArea = modifiers[0];
            ShearV2 = modifiers[1];
            ShearV3 = modifiers[2];
            Torsion = modifiers[3];
            BendingM2 = modifiers[4];
            BendingM3 = modifiers[5];
            MassModifier = modifiers[6];
            WeightModifier = modifiers[7];
        }

        /// <summary>
        /// Return a 1x8 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="CrossSectionalArea" />;
        /// Value(1) = <see cref="ShearV2" />;
        /// Value(2) = <see cref="ShearV3" />;
        /// Value(3) = <see cref="Torsion" />;
        /// Value(4) = <see cref="BendingM2" />;
        /// Value(5) = <see cref="BendingM3" />;
        /// Value(6) = <see cref="MassModifier" />;
        /// Value(7) = <see cref="WeightModifier" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] modifiers = {  CrossSectionalArea, ShearV2, ShearV3,
                                    Torsion, BendingM2, BendingM3,
                                    MassModifier, WeightModifier};
            return modifiers;
        }
    }
}
