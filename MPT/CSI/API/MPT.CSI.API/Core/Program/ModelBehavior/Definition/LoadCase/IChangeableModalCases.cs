// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IChangeableModalCases.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case can set modal cases to be used.
    /// </summary>
    public interface IChangeableModalCase
    {
        /// <summary>
        /// This function sets the modal case for the specified analysis case.
        /// If the specified modal case is not actually a modal case, the program automatically replaces it with the first modal case it can find.
        /// If no modal load cases exist, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCase">This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.</param>
        void SetModalCase(string name,
            string modalCase);
    }
}