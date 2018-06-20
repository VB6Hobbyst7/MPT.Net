// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="eMaterialPerVolumeOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// The option of what property by unit volume is used by the program to determine corresponding properties.
    /// </summary>
    public enum eMaterialPerVolumeOption
    {
        /// <summary>
        /// Weight per unit volume is specified.
        /// </summary>
        WeightPerVolume = 1,

        /// <summary>
        /// Mass per unit volume is specified.
        /// </summary>
        MassPerVolume = 2
    }
}