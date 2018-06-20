// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="IDesignRun.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design interface for running and checking the status of design operations.
    /// </summary>
    public interface IDesignRun
    {

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        void StartDesign();
#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        bool ResultsAreAvailable();
#endif
    }
}