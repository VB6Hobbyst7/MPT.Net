// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="eTendonModelingOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Tendon modeling option available in the application.
    /// </summary>
    public enum eTendonModelingOption
    {
        /// <summary>
        /// Model tendon as loads.
        /// </summary>
        Loads = 1,

        /// <summary>
        /// Model tendon as elements.
        /// </summary>
        Elements = 2
    }
}
