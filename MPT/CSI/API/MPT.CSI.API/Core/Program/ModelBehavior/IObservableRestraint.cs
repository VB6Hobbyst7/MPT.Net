// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IObservableRestraint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return point restraint data.
    /// </summary>
    public interface IObservableRestraint
    {
        /// <summary>
        /// This function returns the total number of point elements in the model with restraint assignments.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int CountRestraint();

        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="degreesOfFreedom">These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.</param>
        void GetRestraint(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom);
    }
}