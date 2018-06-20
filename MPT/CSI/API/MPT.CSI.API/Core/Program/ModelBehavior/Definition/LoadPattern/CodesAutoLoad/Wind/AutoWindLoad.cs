// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="AutoWindLoad.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represents the auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.AutoLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class AutoWindLoad: AutoLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWindLoad" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AutoWindLoad(CSiApiSeed seed) : base(seed) { }
        #endregion

        // No methods created, as this is meant to be a shared type for all auto wind loads.
    }
}
#endif