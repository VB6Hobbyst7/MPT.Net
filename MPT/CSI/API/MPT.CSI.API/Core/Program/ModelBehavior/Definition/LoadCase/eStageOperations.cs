// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eStageOperations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Stage operations available in the application.
    /// </summary>
    public enum eStageOperations
    {
        /// <summary>
        /// Add structure.
        /// </summary>
        AddStructure = 1,

        /// <summary>
        /// Remove structure.
        /// </summary>
        RemoveStructure = 2,

        /// <summary>
        /// Load objects if new.
        /// </summary>
        LoadObjectsIfNew = 3,

        /// <summary>
        /// Load objects.
        /// </summary>
        LoadObjects = 4,

        /// <summary>
        /// Change section properties.
        /// </summary>
        ChangeSectionProperties = 5,

        /// <summary>
        /// Change section property modifiers.
        /// </summary>
        ChangeSectionPropertyModifiers = 6,

        /// <summary>
        /// Change releases.
        /// </summary>
        ChangeReleases = 7,

        /// <summary>
        /// Change section properties and age.
        /// </summary>
        ChangeSectionPropertiesAndAge = 11
    }
}