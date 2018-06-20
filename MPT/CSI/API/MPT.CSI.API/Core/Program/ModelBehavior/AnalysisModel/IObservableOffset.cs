// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-06-2017
//
// Last Modified By : Mark
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IObservableOffset.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return line offset properties.
    /// </summary>
    public interface IObservableOffset
    {
        /// <summary>
        /// This function retrieves the line element end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the line element at the I-End of the line element. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the line element at the J-End of the line element. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        void GetEndLengthOffset(string name,
            ref double lengthIEnd,
            ref double lengthJEnd,
            ref double rigidZoneFactor);
    }
}