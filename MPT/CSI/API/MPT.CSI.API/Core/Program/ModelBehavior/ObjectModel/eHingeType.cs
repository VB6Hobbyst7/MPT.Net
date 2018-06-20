// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-28-2017
// ***********************************************************************
// <copyright file="eHingeType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Hinge types available in the program.
    /// </summary>
    public enum eHingeType
    {
        /// <summary>
        /// Axial P.
        /// </summary>
        AxialP = 1,

        /// <summary>
        /// Shear V2.
        /// </summary>
        ShearV2 = 2,

        /// <summary>
        /// Shear V3.
        /// </summary>
        ShearV3 = 3,

        /// <summary>
        /// Torsion T.
        /// </summary>
        TorsionT = 4,

        /// <summary>
        /// Moment M2.
        /// </summary>
        MomentM2 = 5,

        /// <summary>
        /// Moment M3.
        /// </summary>
        MomentM3 = 6,

        /// <summary>
        /// Interacting P-M2.
        /// </summary>
        InteractingPM2 = 7,

        /// <summary>
        /// Interacting P-M3.
        /// </summary>
        InteractingPM3 = 8,

        /// <summary>
        /// Interacting M2-M3.
        /// </summary>
        InteractingM2M3 = 9,

        /// <summary>
        /// Interacting P-M2-M3.
        /// </summary>
        InteractingPM2M3 = 10,

        /// <summary>
        /// Fiber P-M2-M3.
        /// </summary>
        FiberPM2M3 = 11
    }
}
