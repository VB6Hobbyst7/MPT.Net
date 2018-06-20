// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IListableNames.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Names of the object type can be listed.
    /// </summary>
    public interface IListableNames
    {
        /// <summary>
        /// This function retrieves the names of all items.
        /// </summary>
        /// <param name="names">Names retrieved by the program.</param>
        void GetNameList(out string[] names);
    }
}
