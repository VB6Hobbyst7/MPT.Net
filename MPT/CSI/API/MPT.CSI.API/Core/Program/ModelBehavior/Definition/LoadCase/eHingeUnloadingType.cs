// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eHingeUnloadingType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Hinge unloading types available in the application.
    /// </summary>
    public enum eHingeUnloadingType
    {
        /// <summary>
        /// Unload entire structure.
        /// </summary>
        UnloadEntireStructure = 1,

        /// <summary>
        /// Apply local redistribution.
        /// </summary>
        ApplyLocalRedistribution = 2,

        /// <summary>
        /// Restart using secant stif.
        /// </summary>
        RestartUsingSecantStiffness = 3
    }
}