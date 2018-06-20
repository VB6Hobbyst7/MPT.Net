// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservablePDeltaForces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Element can retrieve P-Delta force data.
    /// </summary>
    public interface IObservablePDeltaForces
    {

        /// <summary>
        /// This function retrieves the P-Delta force assignments to line elements.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="numberForces">The number of P-Delta forces assigned to the line element.</param>
        /// <param name="pDeltaForces">P-Delta force values assigned to the line element. [F]</param>
        /// <param name="directions">The direction of each P-Delta force assignment.</param>
        /// <param name="coordinateSystems">This is an array that contains the name of the coordinate system in which each projected P-Delta force is defined.
        /// This item is blank when the <paramref name="directions" /> item is <see cref="ePDeltaDirection.Local_1" />, that is, when the P-Delta force is defined in the line element local 1-axis direction.</param>
        void GetPDeltaForce(string name,
            ref int numberForces,
            ref double[] pDeltaForces,
            ref ePDeltaDirection[] directions,
            ref string[] coordinateSystems);
    }
}
#endif