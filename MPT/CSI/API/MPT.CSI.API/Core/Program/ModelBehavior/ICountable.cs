// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="ICountable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can be counted.
    /// </summary>
    public interface ICountable
    {
        /// <summary>
        /// Returns the total number of items without regard for type.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Count();
    }
}
