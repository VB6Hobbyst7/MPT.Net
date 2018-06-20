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
    /// Stiffness, weight, and mass modifiers for cables.
    /// The default value for all modifiers is one.
    /// </summary>
    public class CableModifier // Note: This is a class and not a struct because default values must be set to 1.
    {
        /// <summary>
        /// Cross-sectional area modifier.
        /// </summary>
        /// <value></value>
        public double CrossSectionalArea { get; set; } = 1;
        
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
        /// Array must have 3 entries.
        /// </summary>
        /// <param name="modifiers">1x3 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="CrossSectionalArea" />;
        /// Value(1) = <see cref="MassModifier" />;
        /// Value(2) = <see cref="WeightModifier" /></param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + modifiers.Length + " elements when 3 elements was expected.</exception>
        public void FromArray(double[] modifiers)
        {
            if (modifiers.Length != 3) { throw new CSiException("Array has " + modifiers.Length + " elements when 3 elements was expected."); }
            CrossSectionalArea = modifiers[0];
            MassModifier = modifiers[1];
            WeightModifier = modifiers[2];
        }

        /// <summary>
        /// Return a 1x3 matrix of modifier values of the corresponding properties:
        /// Value(0) = <see cref="CrossSectionalArea" />;
        /// Value(1) = <see cref="MassModifier" />;
        /// Value(2) = <see cref="WeightModifier" />
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] modifiers = {  CrossSectionalArea,
                                    MassModifier, WeightModifier};
            return modifiers;
        }
    }
}
