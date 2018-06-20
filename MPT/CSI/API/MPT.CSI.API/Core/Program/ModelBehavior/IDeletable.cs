// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="IDeletable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can be removed from the application.
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// The function deletes the specified item by name.
        /// </summary>
        /// <param name="name">The name of an existing item.</param>
        void Delete(string name);
    }
}
