// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eItemTypeElement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Element type from the analysis model.
    /// </summary>
    public enum eItemTypeElement
    {
        /// <summary>
        /// Object element.
        /// </summary>
        ObjectElement = 0,

        /// <summary>
        /// Element.
        /// </summary>
        Element = 1,

        /// <summary>
        /// Group element.
        /// </summary>
        GroupElement = 2,

        /// <summary>
        /// Selection element.
        /// </summary>
        SelectionElement = 3
    }
}
