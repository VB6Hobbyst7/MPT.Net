// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-16-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-16-2017
// ***********************************************************************
// <copyright file="eAreaOffsetType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Joint offset types avilable for aera elements in the application.
    /// </summary>
    public enum eAreaOffsetType
    {
        /// <summary>
        /// No joint offsets.
        /// </summary>
        NoOffsets = 0,

        /// <summary>
        /// User defined joint offsets specified by joint patte.
        /// </summary>
        OffsetByJointPattern = 1,

        /// <summary>
        /// User defined joint offsets specified by point.
        /// </summary>
        OffsetByPoint = 2
    }
}
