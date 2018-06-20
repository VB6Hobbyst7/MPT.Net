// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-07-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ColdFormedSteelCode.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    /// <summary>
    /// Represents the cold-formed steel frame design codes in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class ColdFormedSteelCode : CSiApiBase
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ColdFormedSteelCode" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected ColdFormedSteelCode(CSiApiSeed seed) : base(seed)
        { }

        // No methods created, as this is meant to be a shared type for all auto wind loads.

        #endregion
    }
}
#endif