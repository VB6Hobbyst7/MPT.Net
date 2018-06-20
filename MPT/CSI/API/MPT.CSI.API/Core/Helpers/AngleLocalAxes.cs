// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="AngleLocalAxes.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Local axes definition by angle rotation from the original local axes.
    /// The order of rotations is cricital to achieving a correct transformation.
    /// </summary>
    public struct AngleLocalAxes
    {
        /// <summary>
        /// Rotate about the 3 axis by <see cref="AngleA" /> [deg].
        /// </summary>
        /// <value>The angle a.</value>
        public double AngleA { get; set; }

        /// <summary>
        /// From <see cref="AngleA" />, rotate about the resulting 2 axis by <see cref="AngleB" /> [deg].
        /// </summary>
        /// <value>The angle b.</value>
        public double AngleB { get; set; }

        /// <summary>
        /// From <see cref="AngleB" />, rotate about the resulting 1 axis by <see cref="AngleC" /> [deg].
        /// </summary>
        /// <value>The angle c.</value>
        public double AngleC { get; set; }
    }
}
