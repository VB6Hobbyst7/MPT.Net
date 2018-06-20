// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="SteelCode.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Represents the steel frame design codes in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class SteelCode : CSiApiBase
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SteelCode" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected SteelCode(CSiApiSeed seed) : base(seed)
        {}

        // No methods created, as this is meant to be a shared type for all auto wind loads.

        #endregion
    }
}
