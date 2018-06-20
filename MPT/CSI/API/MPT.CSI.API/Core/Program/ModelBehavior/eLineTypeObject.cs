// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eLineTypeObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Line object types available in the application.
    /// </summary>
    public enum eLineTypeObject
    {
        /// <summary>
        /// Straight frame object.
        /// </summary>
        StraightFrame = 0,

        /// <summary>
        /// Curved frame object.
        /// </summary>
        CurvedFrame = 1,

        /// <summary>
        /// Cable object.
        /// </summary>
        Cable = 2,

        /// <summary>
        /// Tendon object.
        /// </summary>
        Tendon = 3
    }
}
