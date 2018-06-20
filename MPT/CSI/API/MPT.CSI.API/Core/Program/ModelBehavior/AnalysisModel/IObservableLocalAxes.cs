// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 06-15-2017
// ***********************************************************************
// <copyright file="IObservableLocalAxes.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return local axes rotational offset.
    /// </summary>
    public interface IObservableLocalAxes
    {
        /// <summary>
        /// This function retrieves the local axis angle assignment for the element.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.</param>
        void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset);
    }
}
