// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="ICountableLoadDisplacement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object returns the number of ground displacement loads assigned.
    /// </summary>
    public interface ICountableLoadDisplacement
    {
        /// <summary>
        /// If neither the <paramref name="name" /> item nor the <paramref name="loadPattern" /> item is provided, the function returns the total number of ground displacement load assignments to point elements in the model.
        /// If the <paramref name="name" /> item is provided but not the <paramref name="loadPattern" /> item, the function returns the total number of ground displacement load assignments made for the specified point element.
        /// If the <paramref name="name" /> item is not provided but the <paramref name="loadPattern" /> item is specified, the function returns the total number of ground displacement load assignments made to all point elements for the specified load pattern.
        /// If both the <paramref name="name" /> item and the <paramref name="loadPattern" /> item are provided, the function the total number of ground displacement load assignments made to the specified point element for the specified load pattern.
        /// If the <paramref name="name" /> item or the <paramref name="loadPattern" /> item is provided but is not recognized by the program as valid, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="loadPattern">The name of an existing load pattern.</param>
        /// <returns>System.Int32.</returns>
        int CountLoadDisplacements(string name = "",
            string loadPattern = "");
    }
}