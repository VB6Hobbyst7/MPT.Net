// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IChangeableModifiers.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Cable object can have stiffness, weight, and mass modifiers set.
    /// </summary>
    public interface IChangeableCableModifiers
    {
        /// <summary>
        /// This function defines a modifier assignment.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing element or object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        void SetModifiers(string name,
            CableModifier modifiers);
    }
}