// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eItemType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Item type available for selection filtering.
    /// </summary>
    public enum eItemType
    {
        /// <summary>
        /// Object specified by GUID or name.
        /// </summary>
        Object = 0,

        /// <summary>
        /// Group specified by name.
        /// </summary>
        Group = 1,

        /// <summary>
        /// Objects currently selected in the program.
        /// </summary>
        SelectedObjects = 2
    }
}
