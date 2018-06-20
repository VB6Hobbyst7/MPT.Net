// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="AutoSeismicLoad.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.AutoLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public abstract class AutoSeismicLoad : AutoLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSeismicLoad" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AutoSeismicLoad(CSiApiSeed seed) : base(seed) { }
        #endregion

        // No methods created, as this is meant to be a shared type for all auto wind loads.
    }
}
