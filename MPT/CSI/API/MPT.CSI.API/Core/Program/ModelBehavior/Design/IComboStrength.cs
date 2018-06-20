// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 09-29-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IComboStrength.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements an interface for using load combinations for strength-based design criteria.
    /// </summary>
    public interface IComboStrength
    {
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <param name="nameLoadCombinations">The name of the load combinations selected.</param>
        void GetComboStrength(ref string[] nameLoadCombinations);

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="setLoadCombination">True: The specified load combination is selected as a design combination for strength design.
        /// False: The combination is not selected for strength design.</param>
        void SetComboStrength(string nameLoadCombination, bool setLoadCombination);
    }
}
