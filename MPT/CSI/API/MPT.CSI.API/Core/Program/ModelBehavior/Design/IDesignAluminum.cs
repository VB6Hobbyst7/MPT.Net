// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IDesignAluminum.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design interface for all aluminum frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignMetal" />
    public interface IDesignAluminum : IDesignMetal
    {
        #region Properties        
        /// <summary>
        /// Gets the aluminum design code.
        /// </summary>
        /// <value>The aluminum design code.</value>
        AluminumCode Code { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        void SetCode(AluminumCode code);

        #endregion
    }
}
#endif