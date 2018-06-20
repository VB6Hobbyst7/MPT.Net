// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-10-2017
// ***********************************************************************
// <copyright file="eLoadType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Load type to set in load cases.
    /// </summary>
    public enum eLoadType
    {
        /// <summary>
        /// Load is applied as a force.
        /// </summary>
        Load = 1,

        /// <summary>
        /// Load is applied as an acceleration to the mass.
        /// </summary>
        Accel = 2
    }
}
